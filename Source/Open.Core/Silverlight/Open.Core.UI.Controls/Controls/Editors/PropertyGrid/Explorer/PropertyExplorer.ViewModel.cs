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
using System.Reflection;
using System.Windows;
using Open.Core.UI.Controls.Assets;

namespace Open.Core.Common.Controls.Editors
{
    /// <summary>The logical model for the 'PropertyExplorer' view.</summary>
    public class PropertyExplorerViewModel : ViewModelBase
    {
        #region Head
        public const string PropSelectedObject = "SelectedObject";
        public const string PropObjectName = "ObjectName";
        public const string PropObjectNamspace = "ObjectNamspace";
        public const string PropPropertyGridVisibility = "PropertyGridVisibility";
        public const string PropFilterByPropertyName = "FilterByPropertyName";
        public const string PropEmptyPropertyGridMessage = "EmptyPropertyGridMessage";
        public const string PropEmptyPropertyGridMessageVisibility = "EmptyPropertyGridMessageVisibility";
        public const string PropIconVisibilityHierarchy = "IconVisibilityHierarchy";
        public const string PropIconVisibilityNoHierarchy = "IconVisibilityNoHierarchy";
        public const string PropIncludeHierarchy = "IncludeHierarchy";

        private object selectedObject;

        public PropertyExplorerViewModel()
        {
            GridViewModel = new PropertyGridViewModel();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the selected object that the property-explorer is rendering property values for.</summary>
        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                if (value == SelectedObject) return;
                selectedObject = value;
                GridViewModel.SelectedObject = value;
                OnPropertyChanged(
                                PropSelectedObject,
                                PropObjectName,
                                PropObjectNamspace,
                                PropPropertyGridVisibility,
                                PropEmptyPropertyGridMessageVisibility);
            }
        }

        /// <summary>Gets the view-model of the embedded grid.</summary>
        public PropertyGridViewModel GridViewModel { get; private set; }

        /// <summary>Gets the display name of the currently selected object.</summary>
        public string ObjectName
        {
            get { return SelectedObject == null ? null : SelectedObject.GetType().Name; }
        }

        /// <summary>Gets the name and namespace of the currently selected object.</summary>
        public string ObjectNamspace
        {
            get { return SelectedObject == null ? null : SelectedObject.GetType().Namespace; }
        }
        #endregion

        #region Properties - Delegates
        /// <summary>Gets or sets a function that retrieves the set of properties from the selected object.</summary>
        /// <remarks>Use this to perform special filter operations.</remarks>
        public Func<object, PropertyInfo[]> GetProperties
        {
            get{ return GridViewModel.GetProperties;}
            set { GridViewModel.GetProperties = value; }
        }

        /// <summary>Gets or sets a function that determines what category (if any) a property resides within.</summary>
        public Func<PropertyModel, string> GetCategory
        {
            get { return GridViewModel.GetCategory; }
            set { GridViewModel.GetCategory = value; }
        }
        #endregion

        #region Properties - Filters
        /// <summary>
        ///    Gets or sets a string value to filter the property names by (only display property if the value 
        ///    appears anywhere within the property name). Null for no filtering.
        /// </summary>
        public string FilterByPropertyName
        {
            get { return GridViewModel.FilterByPropertyName; }
            set
            {
                value = value.AsNullWhenEmpty();
                if (value == FilterByPropertyName) return;
                GridViewModel.FilterByPropertyName = value;
                OnPropertyChanged(PropFilterByPropertyName, PropEmptyPropertyGridMessageVisibility);
            }
        }

        /// <summary>Gets or sets whether the properties from the entire object hierachy should be included.</summary>
        public bool IncludeHierarchy
        {
            get { return GridViewModel.IncludeHierarchy; }
            set
            {
                if (value == IncludeHierarchy) return;
                GridViewModel.IncludeHierarchy = value;
                OnPropertyChanged(PropIncludeHierarchy, PropIconVisibilityHierarchy, PropIconVisibilityNoHierarchy);
            }
        }
        #endregion

        #region Properties - Visibility
        /// <summary>Gets the visibility of the property-grid (based on whether there is a selected object or not).</summary>
        public Visibility PropertyGridVisibility
        {
            get { return SelectedObject == null ? Visibility.Collapsed : Visibility.Visible; }
        }

        public Visibility IconVisibilityHierarchy
        {
            get { return IncludeHierarchy ? Visibility.Visible : Visibility.Collapsed; }
        }

        public Visibility IconVisibilityNoHierarchy
        {
            get { return IncludeHierarchy ? Visibility.Collapsed : Visibility.Visible; }
        }
        #endregion

        #region Properties - Labels
        public string IncludeHierarchyTooltip { get { return StringLibrary.PropertyExplorer_Tooltip_IncludeHierarchy; } }
        #endregion
    }
}
