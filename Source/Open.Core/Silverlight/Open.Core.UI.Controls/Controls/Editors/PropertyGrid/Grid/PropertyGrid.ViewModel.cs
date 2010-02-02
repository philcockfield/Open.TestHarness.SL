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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using Open.Core.Common.Controls.Editors.PropertyGridStructure;

namespace Open.Core.Common.Controls.Editors
{
    /// <summary>The logical model for the 'PropertyGrid' view.</summary>
    public class PropertyGridViewModel : ViewModelBase
    {
        #region Head
        public const string PropSelectedObject = "SelectedObject";
        public const string PropIncludeHierarchy = "IncludeHierarchy";
        public const string PropFilterByPropertyName = "FilterByPropertyName";
        public const string PropGetProperties = "GetProperties";
        public const string PropGetCategory = "GetCategory";
        public const string PropCategoriesVisibility = "CategoriesVisibility";
        public const string PropSingleCategoryVisibility = "SingleCategoryVisibility";

        private const string LabelMiscellaneous = "Miscellaneous";
        private object selectedObject;
        private ObservableCollection<CategoryProperties> categoryProperties;
        private readonly ObservableCollection<PropertyModel> singleCategoryProperties = new ObservableCollection<PropertyModel>();

        private bool includeHierarchy;
        private string filterByPropertyName;

        private Func<object, PropertyInfo[]> getProperties;
        private Func<PropertyModel, string> getCategory;

        public PropertyGridViewModel()
        {
            SingleCategoryPropertiesViewModel = new PropertyGridPrimitiveViewModel(singleCategoryProperties);
        }

        #endregion

        #region Properties
        /// <summary>Gets or sets the selected object that the grid is rendering property values for.</summary>
        public object SelectedObject
        {
            get { return selectedObject; }
            set
            {
                // Setup initial conditions.
                if (value == SelectedObject) return;
                selectedObject = value;

                // Update corresponding collection(s).
                ResetSelectedObject();

                // Finish up.
                OnPropertyChanged(PropSelectedObject);
            }
        }

        /// <summary>Gets the collection of categories.</summary>
        public ObservableCollection<CategoryProperties> Categories
        {
            get
            {
                if (categoryProperties == null) categoryProperties = new ObservableCollection<CategoryProperties>();
                return categoryProperties;
            }
        }

        /// <summary>Gets the collection that is displayed when only a single category is present.</summary>
        public PropertyGridPrimitiveViewModel SingleCategoryPropertiesViewModel { get; private set; }
        #endregion

        #region Properties - Visibility
        public Visibility CategoriesVisibility { get { return Categories.Count > 1 ? Visibility.Visible : Visibility.Collapsed; } }
        public Visibility SingleCategoryVisibility { get { return singleCategoryProperties.Count > 0 ? Visibility.Visible : Visibility.Collapsed; } }
        #endregion

        #region Properties - Delegates
        /// <summary>Gets or sets a function that retrieves the set of properties from the selected object.</summary>
        /// <remarks>Use this to perform special filter operations.</remarks>
        public Func<object, PropertyInfo[]> GetProperties
        {
            get
            {
                if (getProperties == null) getProperties = GetPropertiesDefault;
                return getProperties;
            }
            set
            {
                if (value == GetProperties) return;
                getProperties = value;
                ResetSelectedObject();
                OnPropertyChanged(PropGetProperties);
            }
        }

        /// <summary>Gets or sets a function that determines what category (if any) a property resides within.</summary>
        public Func<PropertyModel, string> GetCategory
        {
            get
            {
                if (getCategory == null) getCategory = GetCategoryDefault;
                return getCategory;
            }
            set
            {
                if (value == GetCategory) return;
                getCategory = value;
                ResetSelectedObject();
                OnPropertyChanged(PropGetCategory);
            }
        }
        #endregion

        #region Properties - Filters
        /// <summary>Gets or sets whether the properties from the entire object hierachy should be included.</summary>
        public bool IncludeHierarchy
        {
            get { return includeHierarchy; }
            set
            {
                if (value == IncludeHierarchy) return;
                includeHierarchy = value;
                ResetSelectedObject();
                OnPropertyChanged(PropIncludeHierarchy);
            }
        }

        /// <summary>
        ///    Gets or sets a string value to filter the property names by (only display property if the value 
        ///    appears anywhere within the property name). Null for no filtering.
        /// </summary>
        public string FilterByPropertyName
        {
            get { return filterByPropertyName; }
            set
            {
                value = value.AsNullWhenEmpty();
                if (value == FilterByPropertyName) return;
                filterByPropertyName = value;
                ResetSelectedObject();
                OnPropertyChanged(PropFilterByPropertyName);
            }
        }
        #endregion

        #region Methods
        /// <summary>The default category selector.</summary>
        /// <param name="property">The property to determine the category for.</param>
        /// <returns>The properties categories.</returns>
        /// <remarks>See the 'GetCategory' delegate property, of which this method is the default value</remarks>
        public static string GetCategoryDefault(PropertyModel property)
        {
            var attrCategory = property.CategoryAttribute;
            return attrCategory == null ? null : attrCategory.Category;
        }

        /// <summary>Causes a refresh of the property grid values.</summary>
        public void Refresh()
        {
            ResetSelectedObject();
        }
        #endregion

        #region Internal
        private void ResetSelectedObject()
        {
            ClearCollection();
            if (SelectedObject != null) LoadCollection();
            OnPropertyChanged(PropSingleCategoryVisibility, PropCategoriesVisibility);
        }

        private void ClearCollection( )
        {
            // Dispose of the collections contents.
            foreach (var item in Categories) item.GridViewModel.Dispose();

            // Clear values.
            Categories.RemoveAll();
            singleCategoryProperties.RemoveAll();
        }

        private void LoadCollection()
        {
            // Setup initial conditions.
            if (SelectedObject == null) return;
            var list = new List<CategoryProperties>();

            // Load new categories.
            foreach (var propInfo in GetPropertiesInternal())
            {
                var propertyModel = new PropertyModel(SelectedObject, propInfo);
                var category = GetCategoryProperties(propertyModel, list);
                category.Properties.Add(propertyModel);
            }

            // Update the collection.
            ClearCollection();
            if (list.Count == 1)
            {
                singleCategoryProperties.AddRange(list[0].Properties);
            }
            else
            {
                Categories.AddRange(list.OrderBy(item => item.CategoryName));
            }
        }

        private CategoryProperties GetCategoryProperties(PropertyModel property, ICollection<CategoryProperties> list)
        {
            // Setup initial conditions.
            var categoryName = GetCategory(property);
            if (categoryName == null) categoryName = LabelMiscellaneous;

            // Retrieve the category object if it exists, and if not found create it.
            var category = list.FirstOrDefault(item => item.CategoryName == categoryName);
            if (category == null)
            {
                category = new CategoryProperties {CategoryName = categoryName};
                list.Add(category);
            }

            // Finish up.
            return category;
        }
        #endregion

        #region Internal - Default's for Delegate Properties
        private PropertyInfo[] GetPropertiesInternal()
        {
            var properties = GetProperties(SelectedObject);
            properties = FilterPropertyNames(properties);
            return properties;
        }

        private PropertyInfo[] GetPropertiesDefault(object obj)
        {
            // Retrieve the properties.
            var flags = BindingFlags.Public | BindingFlags.Instance;
            if (!IncludeHierarchy) flags = flags | BindingFlags.DeclaredOnly;
            var properties = obj.GetType().GetProperties(flags);

            // Finish up.
            return properties;
        }

        private PropertyInfo[] FilterPropertyNames(PropertyInfo[] properties)
        {
            // Setup initial conditions.
            var filter = FilterByPropertyName;
            if (filter == null) return properties;

            // Enuermate the collection looking for properties that names match the filter.
            var list = new List<PropertyInfo>();
            foreach (var property in properties)
            {
                if (property.Name.ContainsAll(filter, " ")) list.Add(property);
            }

            // Finish up.
            return list.ToArray();
        }
        #endregion

        /// <summary>Represents a set of properties within a category.</summary>
        public class CategoryProperties : ViewModelBase
        {
            #region Head
            public const string PropCategoryName = "CategoryName";
            public const string PropProperties = "Properties";

            private string categoryName;
            private readonly ObservableCollection<PropertyModel> properties = new ObservableCollection<PropertyModel>();

            public CategoryProperties()
            {
                GridViewModel = new PropertyGridPrimitiveViewModel(Properties);
            }
            #endregion

            #region Properties
            /// <summary>Gets or sets the name of the category of properties.</summary>
            public string CategoryName
            {
                get { return categoryName; }
                set { categoryName = value; OnPropertyChanged(PropCategoryName); }
            }

            /// <summary>Gets the collection of properties.</summary>
            public ObservableCollection<PropertyModel> Properties{get { return properties; }}

            /// <summary>Gets the view-model for the corresponding property grid.</summary>
            public PropertyGridPrimitiveViewModel GridViewModel { get; private set; }
            #endregion
        }
    }
}
