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

using System.Windows;
using System.Windows.Controls;
using Open.Core.UI.Controls;

namespace Open.Core.Common.Controls.Editors
{
    /// <summary>A property-grid editor that displays properties of an object and allows editing of the values.</summary>
    public class PropertyGrid : ContentControl
    {
        #region Head
        public const string PropSelectedObject = "SelectedObject";
        public const string PropIncludeHierarchy = "IncludeHierarchy";
        public const string PropFilterByPropertyName = "FilterByPropertyName";

        public PropertyGrid()
        {
            Templates.Instance.ApplyTemplate(this);
            ViewModel = new PropertyGridViewModel();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public PropertyGridViewModel ViewModel
        {
            get
            {
                if (DataContext == null) DataContext = new PropertyGridViewModel
                                                           {
                                                               IncludeHierarchy = IncludeHierarchy,
                                                               FilterByPropertyName = FilterByPropertyName
                                                           };
                return DataContext as PropertyGridViewModel;
            }
            set { DataContext = value; }
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the selected object that the grid is rendering property values for.</summary>
        public object SelectedObject
        {
            get { return GetValue(SelectedObjectProperty); }
            set { SetValue(SelectedObjectProperty, value); }
        }
        /// <summary>Gets or sets the selected object that the grid is rendering property values for.</summary>
        public static readonly DependencyProperty SelectedObjectProperty =
            DependencyProperty.Register(
                PropSelectedObject,
                typeof (object),
                typeof (PropertyGrid),
                new PropertyMetadata(null, (sender, e) => ((PropertyGrid)sender).ViewModel.SelectedObject = e.NewValue));


        /// <summary>Gets or sets whether the properties from the entire object hierachy should be included.</summary>
        public bool IncludeHierarchy
        {
            get { return (bool)(GetValue(IncludeHierarchyProperty)); }
            set { SetValue(IncludeHierarchyProperty, value); }
        }
        /// <summary>Gets or sets Summary.</summary>
        public static readonly DependencyProperty IncludeHierarchyProperty =
            DependencyProperty.Register(
                PropIncludeHierarchy,
                typeof(bool),
                typeof(PropertyGrid),
                new PropertyMetadata(false, (sender, e) => ((PropertyGrid)sender).ViewModel.IncludeHierarchy = (bool)e.NewValue));


        /// <summary>
        ///    Gets or sets a string value to filter the property names by (only display property if the value 
        ///    appears anywhere within the property name). Null for no filtering.
        /// </summary>
        public string FilterByPropertyName
        {
            get { return (string) (GetValue(FilterByPropertyNameProperty)); }
            set { SetValue(FilterByPropertyNameProperty, value); }
        }
        /// <summary>
        ///    Gets or sets a string value to filter the property names by (only display property if the value 
        ///    appears anywhere within the property name). Null for no filtering.
        /// </summary>
        public static readonly DependencyProperty FilterByPropertyNameProperty =
            DependencyProperty.Register(
                PropFilterByPropertyName,
                typeof (string),
                typeof (PropertyGrid),
                new PropertyMetadata(null, (sender, e) => ((PropertyGrid)sender).ViewModel.FilterByPropertyName = (string)e.NewValue));
        #endregion

        #region Methods
        /// <summary>Causes a refresh of the property grid values.</summary>
        public void Refresh()
        {            
            ViewModel.Refresh();
        }
        #endregion
    }
}
