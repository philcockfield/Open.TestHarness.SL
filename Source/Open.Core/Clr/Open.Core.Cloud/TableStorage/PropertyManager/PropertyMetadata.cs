using System;
using System.Reflection;
using Open.Core.Common;

namespace Open.Core.Cloud.TableStorage
{
    /// <summary>Cached property results.</summary>
    internal class PropertyMetadata
    {
        #region Head
        private IConverter converter;
        private bool? hasConverter;

        public PropertyMetadata(PropertyInfo modelProperty, PropertyInfo backingProperty, PersistPropertyAttribute persistAttribute)
        {
            // Store values.
            ModelProperty = modelProperty;
            BackingProperty = backingProperty;
            PersistAttribute = persistAttribute;

            // Store whether there is a converter (persisted property value for future fast reads).
            hasConverter = HasConverter; 

            // Ensure the convert is an IConverter.
            if (HasConverter && !IsModelPropertyEnum && !PersistAttribute.Converter.IsA<IConverter>()) throw new ArgumentOutOfRangeException(
                                    string.Format("The property '{0}' on type '{1}' declares a converter of type '{2}' which is not an '{3}'.", 
                                    modelProperty.Name,
                                    modelProperty.DeclaringType.Name,
                                    PersistAttribute.Converter.FullName,
                                    typeof(IConverter).Name));
        }
        #endregion

        #region Properties
        public PropertyInfo ModelProperty { get; private set; }
        public PropertyInfo BackingProperty { get; private set; }
        public PersistPropertyAttribute PersistAttribute { get; set; }
        public bool HasConverter
        {
            get
            {
                if (hasConverter != null) return hasConverter.Value;
                if (PersistAttribute.Converter != null) return true;
                if (IsModelPropertyEnum) return true;
                return false;
            }
        }
        public IConverter Converter
        {
            get
            {
                if (!HasConverter) return null;
                return converter ?? (converter = CreateConverter());
            }
        }

        private bool IsModelPropertyEnum { get { return ModelProperty.PropertyType.IsEnum; } }
        #endregion

        #region Internal
        private IConverter CreateConverter()
        {
            var converterType = PersistAttribute.Converter;
            if (converterType == null && IsModelPropertyEnum) converterType = typeof(EnumToIntConverter);
            if (converterType == null) return null;
            return Activator.CreateInstance(converterType) as IConverter;
        }
        #endregion
    }
}