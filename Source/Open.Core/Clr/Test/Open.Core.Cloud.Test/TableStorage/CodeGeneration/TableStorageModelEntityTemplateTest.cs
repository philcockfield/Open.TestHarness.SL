using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.TableStorage.CodeGeneration;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [TestClass]
    public class TableStorageModelEntityTemplateTest : CloudTestBase
    {
        #region Head
        private TableStorageModelEntityTemplate generator;

        [TestInitialize]
        public void TestSetup()
        {
            generator = new TableStorageModelEntityTemplate();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldWriteToFile()
        {
            generator.ModelType = typeof(MockEntity1);
            OutputFileWriter.Write(generator);
        }

        [TestMethod]
        public void ShouldThrowIfModelTypeIsNotATableStorageModelBase()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => generator.ModelType = typeof(string));
        }

        [TestMethod]
        public void ShouldHaveNestedNamespace()
        {
            generator.ModelType = null;
            generator.Namespace.ShouldBe(null);

            generator.ModelType = typeof(MockEntity1);
            generator.Namespace.ShouldBe(GetType().Namespace + ".Generated");
        }

        [TestMethod]
        public void ShouldEmitEntityWithinNamespace()
        {
            generator.ModelType = typeof (MockEntity1);
            generator.TransformText().Contains("namespace " + generator.Namespace).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldConstructNameOfEntityBasedOnModelType()
        {
            generator.ModelType = null;
            generator.ClassName.ShouldBe(null);

            generator.ModelType = typeof(MockEntity1);
            generator.ClassName.ShouldBe("MockEntity1TableEntity");
        }

        [TestMethod]
        public void ShouldEmitEntityWithCorrectClassName()
        {
            generator.ModelType = typeof(MockEntity1);
            generator.TransformText().Contains("public class " + generator.ClassName).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldEmitConstructors()
        {
            generator.ModelType = typeof(MockEntity1);
            var code = generator.TransformText();
            code.Contains("public MockEntity1TableEntity()").ShouldBe(true);
            code.Contains("public MockEntity1TableEntity(string partitionKey, string rowKey) : base(partitionKey, rowKey)").ShouldBe(true);
        }

        [TestMethod]
        public void ShouldFindPropertiesDecoratedWithPersistAttribute()
        {
            var type = typeof (MockEntity1);
            generator.ModelType = type;

            var propText = type.GetProperty("Text");
            var propNotPersisted = type.GetProperty("NotPersisted");

            generator.Properties.ShouldContain(propText);
            generator.Properties.ShouldNotContain(propNotPersisted);
        }

        [TestMethod]
        public void ShouldReturnEmptyCollectionOfPropertiesWhenNoModelTypeSet()
        {
            generator.ModelType = null;
            generator.Properties.Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHaveInterfaceName()
        {
            generator.ModelType = null;
            generator.InterfaceName.ShouldBe(null);

            generator.ModelType = typeof(MockEntity1);
            generator.InterfaceName.ShouldBe("IMockEntity1TableEntity");
        }

        [TestMethod]
        public void ShouldEmitEntityWithExportTag()
        {
            generator.ModelType = typeof(MockEntity1);
            var code = generator.TransformText();
            code.Contains("[Export(typeof(IMockEntity1TableEntity))]").ShouldBe(true);
        }

        [TestMethod]
        public void ShouldHaveContextName()
        {
            generator.ModelType = null;
            generator.ContextName.ShouldBe(null);

            generator.ModelType = typeof(MockEntity1);
            generator.ContextName.ShouldBe("MockEntity1Context");
        }
        #endregion

        public class MockEntity1 : TableEntityBase
        {
            [Persist]
            public string Text { get; set; }

            [Persist]
            public int Number { get; private set; }

            public string NotPersisted { get; set; }
        }

    }
}
