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
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Open.Core.Common;

[assembly: InternalsVisibleTo("Open.Core.Cloud.Test.Clr")]

namespace Open.Core.Cloud.TableStorage
{
    /// <summary>Handles caching of property values.</summary>
    internal class PropertyMapCache<TBackingEntity> where TBackingEntity : ITableServiceEntity
    {
        #region Head
        private readonly List<PropertyMetadata> propertySets = new List<PropertyMetadata>();
        private static readonly string PropRowKey = LinqExtensions.GetPropertyName<ITableServiceEntity>(m => m.RowKey);
        private const BindingFlags PropertyPublicInstance = BindingFlags.Public | BindingFlags.Instance;

        /// <summary>Constructor.</summary>
        public PropertyMapCache()
        {
            BackingType = typeof (TBackingEntity);
        }
        #endregion

        #region Properties
        /// <summary>Gets the type of the backing entity.</summary>
        public Type BackingType { get; private set; }
        #endregion

        #region Methods
        /// <summary>Gets the corresponding property on the backing entity.</summary>
        /// <param name="modelProperty">The model property to look up.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the model-property is not decorated with the [PersistProperty] attribute.</exception>
        /// <exception cref="NotFoundException">Thrown if the model-property does not map to a corresponding property on the backing entity.</exception>
        public PropertyMetadata GetPropertyMetadata(PropertyInfo modelProperty)
        {
            return GetOrCreatePropertySet(modelProperty);
        }
        #endregion

        #region Internal
        private PropertyMetadata GetOrCreatePropertySet(PropertyInfo modelProperty)
        {
            // Setup initial conditions.
            var propertySet = propertySets.FirstOrDefault(m => m.ModelProperty == modelProperty);
            if (propertySet != null) return propertySet;
            if (modelProperty == null) throw new ArgumentNullException("modelProperty");

            // Retrieve the [PersistProperty] attribute.
            var attribute = modelProperty.GetPersistAttribute();
            if (attribute == null) throw new ArgumentOutOfRangeException(
                string.Format("The property '{0}' on type '{1}' is not decorated with [{2}].",
                              modelProperty.Name,
                              modelProperty.DeclaringType.Name,
                              typeof(PersistPropertyAttribute).Name));

            // Retrieve the backing property.
            var backingProperty = GetBackingProperty(modelProperty, attribute);

            // Create the new property set.
            propertySet = new PropertyMetadata(modelProperty, backingProperty, attribute);
            propertySets.Add(propertySet);

            // Finish up.
            return propertySet;
        }

        private PropertyInfo GetBackingProperty(PropertyInfo modelProperty, PersistPropertyAttribute attribute)
        {
            // Retrieve the backing property.
            var name = GetBackingPropertyName(modelProperty, attribute);
            var backingProperty = BackingType.GetProperty(name, PropertyPublicInstance);

            // Ensure the property exists.
            if (backingProperty == null) throw new NotFoundException(
                string.Format("The property '{0}' on type '{1}' does not map to a corresponding property on the backing entity '{2}'.",
                              modelProperty.Name,
                              modelProperty.DeclaringType.Name,
                              BackingType.Name));

            // Ensure the property can be written to.
            if (!backingProperty.CanWrite) throw new ArgumentOutOfRangeException(
                string.Format("The property '{0}' on type '{1}' is read-only and cannot be written to.",
                              modelProperty.Name,
                              modelProperty.DeclaringType.Name));

            // Finish up.
            return backingProperty;
        }

        private static string GetBackingPropertyName(PropertyInfo modelProperty, PersistPropertyAttribute attribute)
        {
            if (IsRowKey(modelProperty, attribute)) return PropRowKey;
            if (!attribute.MapTo.IsNullOrEmpty(true)) return attribute.MapTo;
            return modelProperty.Name;
        }

        private static bool IsRowKey(PropertyInfo modelProperty, PersistPropertyAttribute attribute)
        {
            // Setup initial conditions.
            if (!attribute.IsRowKey) return false;

            // Ensure the row-key is set only once on the class.
            var modelType = modelProperty.DeclaringType;
            var keyPropertyTotal = modelType.GetProperties(PropertyPublicInstance)
                .Where(m => m.GetCustomAttributes(typeof(PersistPropertyAttribute), true)
                                .Where(attr => ((PersistPropertyAttribute)attr).IsRowKey).Count() > 0).Count();
            if (keyPropertyTotal > 1) throw new ArgumentOutOfRangeException(
                string.Format("There are {0} {1} mapped to the RowKey via the [{2}] attribute. There can be only one.", 
                              keyPropertyTotal,
                              "property".ToPlural(keyPropertyTotal, "properties"), 
                              typeof(PersistPropertyAttribute).Name));
            // Finish up.
            return true;
        }
        #endregion
    }
}
