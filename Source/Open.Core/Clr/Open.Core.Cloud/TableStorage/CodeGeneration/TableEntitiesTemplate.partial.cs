﻿//------------------------------------------------------
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

namespace Open.Core.Cloud.TableStorage.CodeGeneration
{
    /// <summary>Generates code for a collection of backing entities that support TableStorageModel's.</summary>
    public partial class TableEntitiesTemplate
    {
        #region Head
        /// <summary>Constructor.</summary>
        public TableEntitiesTemplate()
        {
            ModelTypes = new ModelTypes();
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of models the template is building code for.</summary>
        public ModelTypes ModelTypes { get; private set; }
        #endregion

        #region Internal
        private IEnumerable<EntityGenerator> GetGenerators()
        {
            return from n in ModelTypes.Types
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
