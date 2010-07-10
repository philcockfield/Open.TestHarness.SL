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
using Open.Core.Common;

namespace Open.Core.Cloud.TableStorage.CodeGeneration
{
    /// <summary>Generates code for a single backing entity for a TableStorageModel.</summary>
    public partial class TableEntityTemplate
    {
        #region Head
        private Type modelType;
        private IEnumerable<PropertyInfo> properties;

        /// <summary>Constructor.</summary>
        public TableEntityTemplate()
        {
            IncludeHeaderDirectives = true;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the type of the model.</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the type is not decorated with the [PersistClass] attribute.</exception>
        public Type ModelType
        {
            get { return modelType; }
            set
            {
                if (value != null && value.GetPersistAttribute() == null)
                {
                    throw new ArgumentOutOfRangeException(
                                                    string.Format("The type '{0}' is not decorated with [{1}].",
                                                    value.Name,
                                                    typeof(PersistClassAttribute).Name));
                }
                modelType = value;
            }
        }

        /// <summary>Gets or sets whether the header (using and comments) are generated.</summary>
        public bool IncludeHeaderDirectives { get; set; }

        /// <summary>Gets the namespace of the entity.</summary>
        public string Namespace { get { return GetNamespace(ModelType); } }

        /// <summary>Gets the name of the entity class.</summary>
        public string ClassName { get { return GetClassName(ModelType); } }

        /// <summary>Gets the name of the entity interface.</summary>
        public string InterfaceName { get { return GetInterfaceName(ModelType); } }

        /// <summary>Gets the collection of properties that require persisting.</summary>
        public IEnumerable<PropertyInfo> Properties { get { return properties ?? (properties = GetProperties()); } }

        /// <summary>Gets the name of the entity's TableServiceContext.</summary>
        public string ContextName { get { return GetContextName(ModelType); } }
        #endregion

        #region Methods
        /// <summary>Retrieves the namespace to put a generated entity within.</summary>
        /// <param name="modelType">The type of the model.</param>
        public static string GetNamespace(Type modelType)
        {
            return modelType == null ? null : string.Format("{0}.Generated", modelType.Namespace);
        }

        /// <summary>Retrieves the class name for the generated entity.</summary>
        /// <param name="modelType">The type of the model.</param>
        public static string GetClassName(Type modelType)
        {
            return modelType == null ? null : string.Format("{0}TableEntity", modelType.Name);
        }

        /// <summary>Retrieves the interface name for the generated entity.</summary>
        /// <param name="modelType">The type of the model.</param>
        public static string GetInterfaceName(Type modelType)
        {
            return modelType == null ? null : "I" + GetClassName(modelType);
        }

        /// <summary>Retrieves the context name for the generated entity.</summary>
        /// <param name="modelType">The type of the model.</param>
        public static string GetContextName(Type modelType)
        {
            return modelType == null ? null : string.Format("{0}Context", modelType.Name);
        }
        #endregion

        #region Internal
        private IEnumerable<PropertyInfo> GetProperties()
        {
            if (ModelType == null) return new List<PropertyInfo>();
            return ModelType
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(EmitProperty);
        }

        private static bool EmitProperty(PropertyInfo property)
        {
            // Setup initial conditions.
            var attribute = property.GetPersistAttribute();
            if (attribute == null) return false;

            // Don't emit if this is marked as mapping to the RowKey [property already available].
            if (attribute.IsRowKey) return false;

            // Don't emit if this is named 'RowKey' or 'PartitionKey' [properties already available].
            if (property.Name == LinqExtensions.GetPropertyName<ITableServiceEntity>(m => m.RowKey)) return false;
            if (property.Name == LinqExtensions.GetPropertyName<ITableServiceEntity>(m => m.PartitionKey)) return false;

            // Finish up.
            return true;
        }
        #endregion
    }
}
