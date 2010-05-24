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
using System.Text;

namespace Open.Core.Cloud.TableStorage.CodeGeneration
{
    /// <summary>Generates code for a collection of backing entities taht support TableStorageModel's.</summary>
    public partial class TableEntitiesTemplate : TableEntitiesTemplateBase
    {
        #region Head
        private readonly List<Type> modelTypes = new List<Type>();

        /// <summary>Constructor.</summary>
        public TableEntitiesTemplate()
        {

        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of models the template is building code for.</summary>
        public IEnumerable<Type> ModelTypes { get { return modelTypes; } }
        #endregion

        #region Methods
        /// <summary>Adds a model type to include in code generation.</summary>
        /// <typeparam name="T">The type of the model</typeparam>
        public void AddModelType<T>() where T : TableModelBase
        {
            AddModelType(typeof(T));
        }
        private void AddModelType(Type type)
        {
            if (!modelTypes.Contains(type)) modelTypes.Add(type);
        }

        /// <summary>Adds all TableModelBase types from the given assembly.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        public void AddModelTypes(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            var types = assembly.GetTableModelTypes();
            foreach (var type in types)
            {
                AddModelType(type);
            }
        }

        /// <summary>Removes a model type from code generation.</summary>
        /// <typeparam name="T">The type of the model</typeparam>
        public void RemoveModelType<T>() where T : TableModelBase
        {
            var type = typeof(T);
            modelTypes.Remove(type);
        }
        #endregion

        #region Internal
        private IEnumerable<EntityGenerator> GetGenerators()
        {
            return from n in ModelTypes
                   select new EntityGenerator(n);
        }
        #endregion

        private class EntityGenerator
        {
            #region Head
            public EntityGenerator(Type modelType)
            {
                ModelType = modelType;
                var generator = new TableEntityTemplate
                                    {
                                        ModelType = modelType,
                                        IncludeHeaderDirectives = false,
                                    };
                Code = generator.TransformText();
            }
            #endregion

            #region Properties
            public Type ModelType { get; private set; }
            public string Code { get; private set; }
            #endregion
        }
    }
}
