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
using System.Text;
using Open.Core.Common;

namespace Open.Core.Cloud.TableStorage.CodeGeneration
{
    /// <summary>Generates code for a single TableStorage model.</summary>
    public partial class TableStorageModelEntityTemplate
    {
        #region Head
        private Type modelType;
        private IEnumerable<PropertyInfo> properties;
        #endregion

        #region Properties
        /// <summary>Gets or sets the type of the model.</summary>
        /// <exception cref="ArgumentOutOfRangeException">If the type is not derived from [TableEntityBase].</exception>
        public Type ModelType
        {
            get { return modelType; }
            set
            {
                if (value != null && !value.IsA<TableEntityBase>())
                {
                    throw new ArgumentOutOfRangeException(
                        "value", 
                        string.Format("The type '{0}' does not derive for '{1}'.", value.Name, typeof(TableEntityBase).Name));
                }
                modelType = value;
            }
        }

        /// <summary>Gets the namespace of the entity.</summary>
        public string Namespace { get { return GetNamespace(ModelType); } }

        /// <summary>Gets the name of the entity class.</summary>
        public string ClassName { get { return GetClassName(ModelType); } }

        /// <summary>Gets the name of the entity interface.</summary>
        public string InterfaceName { get { return GetInterfaceName(ModelType); } }

        /// <summary>Gets the name of the entity's table Context.</summary>
        public string ContextName { get { return GetContextName(ModelType); } }

        /// <summary>Gets the collection of properties that require persisting.</summary>
        public IEnumerable<PropertyInfo> Properties { get { return properties ?? (properties = GetProperties()); } }
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
            return modelType
                        .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => m.HasAttribute<PersistAttribute>());
        }
        #endregion
    }
}
