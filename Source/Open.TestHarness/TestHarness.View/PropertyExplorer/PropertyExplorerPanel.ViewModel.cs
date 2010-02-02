//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using Open.Core.Common;
using Open.Core.Common.Controls.Editors;
using Open.TestHarness.Model;
using Open.TestHarness.View.Assets;

using T = Open.TestHarness.View.PropertyGrid.PropertyExplorerPanelViewModel;

namespace Open.TestHarness.View.PropertyGrid
{
    /// <summary>The logical model for the 'PropertyExplorerPanel' view.</summary>
    public class PropertyExplorerPanelViewModel : ViewModelBase
    {
        #region Head
        public const string PropSelectedObject = "SelectedObject";
        public const string PropViewOptionSelected = "ViewOptionSelected";
        public const string PropModelOptionSelected = "ModelOptionSelected";

        private static readonly List<string> CommonControlProperties = new List<string>
                                                                  {
                                                                      "IsEnabled", 
                                                                      "Width", 
                                                                      "Height",
                                                                      "Opacity",
                                                                      "Padding",
                                                                  };

        private ReadPropertyDataFrom selectedDataOption;
        private readonly List<PropertyInfo> declaredPropertiesOnSelectedObject = new List<PropertyInfo>();

        private readonly PropertyExplorerViewModel propertyExplorerModel;
        private readonly TestHarnessModel testHarnessModel;
        private readonly DelayedAction delayedAction;
        private readonly PropertyExplorerSettings storedSettings;

        public PropertyExplorerPanelViewModel()
        {
            // Setup initial conditions.
            testHarnessModel = TestHarnessModel.Instance;
            delayedAction = new DelayedAction(0.1, OnDelayTimedOut);
            propertyExplorerModel = new PropertyExplorerViewModel { GetCategory = GetCategory, GetProperties = GetProperties };

            // Load settings.
            storedSettings = testHarnessModel.Settings.PropertyExplorer;
            SelectedDataOption = storedSettings.ReadPropertyDataFrom;
            propertyExplorerModel.IncludeHierarchy = storedSettings.IncludeHierarchy;

            // Wire up events.
            testHarnessModel.PropertyChanged += (sender, e) =>
                                       {
                                           if (e.PropertyName == TestHarnessModel.PropCurrentClass) OnCurrentClassChanged();
                                       };

            propertyExplorerModel.PropertyChanged += (sender, e) =>
                                     {
                                         if (e.PropertyName == PropertyExplorerViewModel.PropSelectedObject) OnPropertyChanged(PropSelectedObject);
                                         if (e.PropertyName == PropertyExplorerViewModel.PropIncludeHierarchy)
                                         {
                                             storedSettings.IncludeHierarchy = propertyExplorerModel.IncludeHierarchy;
                                             SaveSettings();
                                         }
                                     };
        }
        #endregion

        #region Event Handlers
        private void OnCurrentClassChanged()
        {
            delayedAction.Start();
        }

        private void OnDelayTimedOut()
        {
            SelectedObject = GetCurrentObject();
        }

        private string GetCategory(PropertyModel property)
        {
            if (SelectedObject == null || !SelectedObject.GetType().IsA(typeof(UIElement)))
            {
                return PropertyGridViewModel.GetCategoryDefault(property);
            }
            else
            {
                if (CommonControlProperties.Contains(property.Definition.Name)) return "Common";
                if (IsDeclaredOn(property)) return string.Format("Declared on '{0}'", SelectedObject.GetType().Name);
                return "Misc";
            }
        }

        private PropertyInfo[] GetProperties(object obj)
        {
            // Setup initial conditions.
            var includeHierarchy = propertyExplorerModel.IncludeHierarchy;
            var type = obj.GetType();
            var isUIElement = type.IsA(typeof (UIElement));
            declaredPropertiesOnSelectedObject.Clear();

            // If this is a model object do a simple read of the properties.
            if (!isUIElement) return GetProperties(obj, includeHierarchy).ToArray();

            // Store common properties reference list.
            declaredPropertiesOnSelectedObject.AddRange(GetProperties(obj, false));

            // Build master list of properties.
            var list = new List<PropertyInfo>();
            list.AddRange(GetProperties(obj, includeHierarchy));
            list.AddRange(GetCommonControlProperties(obj));

            // Finish up.
            return list.ToArray();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the selected object that the property-explorer is rendering property values for.</summary>
        public object SelectedObject
        {
            get { return PropertyExplorerModel.SelectedObject; }
            set
            {
                PropertyExplorerModel.SelectedObject = value;
                OnPropertyChanged<PropertyExplorerPanelViewModel>(m => m.SelectedObject, m => m.IsVisible);
            }
        }

        /// <summary>Gets the view-model for the property explorer.</summary>
        public PropertyExplorerViewModel PropertyExplorerModel
        {
            get { return propertyExplorerModel; }
        }

        /// <summary>Gets whether the property explorer is visible.</summary>
        public bool IsVisible
        {
            get
            {
                var classAttribute = testHarnessModel.CurrentClass == null ? null : testHarnessModel.CurrentClass.Attribute;
                return classAttribute == null ? true : classAttribute.IsPropertyExplorerVisible;
            }
        }
        #endregion

        #region Properties - Data Option
        public ReadPropertyDataFrom SelectedDataOption
        {
            get { return selectedDataOption; }
            set
            {
                // Setup initial conditions.
                if (value == SelectedDataOption) return;

                // Store value (persisting the settings isolated storage).
                selectedDataOption = value;
                testHarnessModel.Settings.PropertyExplorer.ReadPropertyDataFrom = value;
                SaveSettings();

                // Finish up.
                OnPropertyChanged(PropViewOptionSelected, PropModelOptionSelected);
                delayedAction.Start();
            }
        }

        public bool ViewOptionSelected
        {
            get { return selectedDataOption == ReadPropertyDataFrom.View; }
            set
            {
                if (value == ViewOptionSelected) return;
                SelectedDataOption = value ? ReadPropertyDataFrom.View : ReadPropertyDataFrom.Model;
            }
        }

        public bool ModelOptionSelected
        {
            get { return selectedDataOption == ReadPropertyDataFrom.Model; }
            set
            {
                if (value == ModelOptionSelected) return;
                SelectedDataOption = value ? ReadPropertyDataFrom.Model : ReadPropertyDataFrom.View;
            }
        }
        #endregion

        #region Properties - Labels
        public string PropertiesLabel { get { return StringLibrary.PropertyExplorer_Title; } }

        public string ViewOptionLabel { get { return StringLibrary.PropertyExplorer_ViewOption; } }
        public string ModelOptionLabel { get { return StringLibrary.PropertyExplorer_ModelOption; } }

        public string ViewOptionToolTip { get { return StringLibrary.PropertyExplorer_ViewOption_ToolTip; } }
        public string ModelOptionToolTip { get { return StringLibrary.PropertyExplorer_ModelOption_ToolTip; } }
        #endregion

        #region Internal
        private object GetCurrentObject()
        {
            // Setup initial conditions.
            var current = testHarnessModel.CurrentClass;
            if (current == null || current.CurrentControls.Count == 0) return null;

            // If the 'View' option is selected return the control.
            var obj = current.CurrentControls[0];
            if (ViewOptionSelected) return obj;

            // The 'Model' option is selected, return the DataContext.
            var element = obj as FrameworkElement;
            return element == null ? null : element.DataContext;
        }

        private static List<PropertyInfo> GetProperties(object obj, bool includeHierarchy)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            if (!includeHierarchy) flags = flags | BindingFlags.DeclaredOnly;
            var properties = obj.GetType().GetProperties(flags);
            return new List<PropertyInfo>(properties);
        }

        private static List<PropertyInfo> GetCommonControlProperties(object obj)
        {
            var list = new List<PropertyInfo>();
            var type = obj.GetType();
            if (! type.IsA(typeof(UIElement))) return list;

            foreach (var name in CommonControlProperties)
            {
                var property = type.GetProperty(name);
                if (property != null) list.Add(property);
            }
            return list;
        }

        private bool IsDeclaredOn(PropertyModel property)
        {
            return declaredPropertiesOnSelectedObject.FirstOrDefault(item => item.Name == property.Definition.Name) != null;
        }

        private void SaveSettings()
        {
            testHarnessModel.Settings.Save();
        }
        #endregion
    }
}
