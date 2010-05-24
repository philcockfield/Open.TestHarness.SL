using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            generator.AddModelType<MockEntityA>();
            generator.AddModelType<MockEntityB>();
            OutputFileWriter.Write("TableEntities.g.cs", generator.TransformText());
        }

        [TestMethod]
        public void ShouldAddModelType()
        {
            generator.ModelTypes.Count().ShouldBe(0);
            generator.AddModelType<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInsertSameModelTypeTwice()
        {
            generator.AddModelType<MockEntityA>();
            generator.AddModelType<MockEntityA>();
            generator.AddModelType<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldRemoveModelType()
        {
            generator.AddModelType<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(1);

            generator.RemoveModelType<MockEntityA>();
            generator.ModelTypes.Count().ShouldBe(0);

            generator.RemoveModelType<MockEntityA>();
            generator.RemoveModelType<MockEntityA>();
        }
        #endregion
    }
}
