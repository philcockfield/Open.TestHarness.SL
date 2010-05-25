using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.TableStorage.CodeGeneration;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [TestClass]
    public class TableEntitiesTemplateTest
    {
        #region Head
        private TableEntitiesTemplate generator;

        [TestInitialize]
        public void TestSetup()
        {
            generator = new TableEntitiesTemplate();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void WriteToFile()
        {
            generator.Add<MockEntityA>();
            generator.Add<MockEntityB>();
            OutputFileWriter.Write("TableEntities.g.cs", generator.TransformText());
        }

        [TestMethod]
        public void ShouldAddModelType()
        {
            generator.ModelTypes.Count().ShouldBe(0);
            generator.Add<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInsertSameModelTypeTwice()
        {
            generator.Add<MockEntityA>();
            generator.Add<MockEntityA>();
            generator.Add<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldAllowDifferentModelTypesToBeAdded()
        {
            generator.Add<MockEntityA>();
            generator.Add<MockEntityB>();
            generator.ModelTypes.Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldRemoveModelType()
        {
            generator.Add<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(1);

            generator.Remove<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(0);

            generator.Remove<MockEntityA>();
            generator.Remove<MockEntityA>();
        }

        [TestMethod]
        public void ShouldAddMultipleClassesFromAssembly()
        {
            var types = GetType().Assembly.GetTableModelTypes();
            generator.Add(GetType().Assembly);
            generator.Add(GetType().Assembly); // Should not add types more than once.

            generator.ModelTypes.Count().ShouldBe(types.Count());
            foreach (var type in types)
            {
                generator.ModelTypes.Contains(type).ShouldBe(true);
            }
        }
        #endregion
    }
}
