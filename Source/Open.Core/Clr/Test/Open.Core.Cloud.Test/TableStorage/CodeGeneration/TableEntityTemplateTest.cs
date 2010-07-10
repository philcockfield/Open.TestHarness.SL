using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.TableStorage.CodeGeneration;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [TestClass]
    public class TableEntityTemplateTest : CloudTestBase
    {
        #region Head
        private TableEntityTemplate generator;

        [TestInitialize]
        public void TestSetup()
        {
            generator = new TableEntityTemplate();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void WriteToFile()
        {
            generator.ModelType = typeof(MockModelA);
            OutputFileWriter.Write("MockModelATableEntity.g.cs", generator.TransformText());
        }

        [TestMethod]
        public void ShouldThrowIfModelTypeIsNotDecoratedWithPersistClassAttribute()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => generator.ModelType = typeof(NonPersistableClass));
        }

        [TestMethod]
        public void ShouldHaveNestedNamespace()
        {
            generator.ModelType = null;
            generator.Namespace.ShouldBe(null);

            generator.ModelType = typeof(MockModelA);
            generator.Namespace.ShouldBe(GetType().Namespace + ".Generated");
        }

        [TestMethod]
        public void ShouldEmitEntityWithinNamespace()
        {
            generator.ModelType = typeof (MockModelA);
            generator.TransformText().Contains("namespace " + generator.Namespace).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotEmitPropertyMarkedAsRowKeyInAttribute()
        {
            // ...because it maps to the default 'RowKey' property.
            generator.ModelType = typeof(MockModelA);
            generator.TransformText().Contains("public System.String Id { get; set; }").ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotEmitPropertyNamedRowKey()
        {
            // ...because it maps to the default 'RowKey' property.
            generator.ModelType = typeof(MockModelC);
            generator.TransformText().Contains("public System.String RowKey { get; set; }").ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotEmitPropertyNamedPartitionKey()
        {
            // ...because it maps to the default 'PartitionKey' property.
            generator.ModelType = typeof(MockModelC);
            generator.TransformText().Contains("public System.String PartitionKey { get; set; }").ShouldBe(false);
        }

        [TestMethod]
        public void ShouldConstructNameOfEntityBasedOnModelType()
        {
            generator.ModelType = null;
            generator.ClassName.ShouldBe(null);

            generator.ModelType = typeof(MockModelA);
            generator.ClassName.ShouldBe("MockModelATableEntity");
        }

        [TestMethod]
        public void ShouldEmitEntityWithCorrectClassName()
        {
            generator.ModelType = typeof(MockModelA);
            generator.TransformText().Contains("public partial class " + generator.ClassName).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldHaveContextName()
        {
            TableEntityTemplate.GetContextName(typeof(MockModelA)).ShouldBe("MockModelAContext");
        }

        [TestMethod]
        public void ShouldEmitConstructors()
        {
            generator.ModelType = typeof(MockModelA);
            var code = generator.TransformText();
            code.Contains("public MockModelATableEntity()").ShouldBe(true);
            code.Contains("public MockModelATableEntity(string partitionKey, string rowKey)").ShouldBe(true);
        }

        [TestMethod]
        public void ShouldFindPropertiesDecoratedWithPersistAttribute()
        {
            var type = typeof (MockModelA);
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

            generator.ModelType = typeof(MockModelA);
            generator.InterfaceName.ShouldBe("IMockModelATableEntity");
        }

        [TestMethod]
        public void ShouldIncludeHeaderDirectivesByDefault()
        {
            generator.IncludeHeaderDirectives.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldEmitHeaderDirectives()
        {
            generator.ModelType = typeof (MockModelA);
            generator.IncludeHeaderDirectives = true;
            var code = generator.TransformText();

            code.Contains("//   Generated code.").ShouldBe(true);
            code.Contains("using Open.Core.Cloud.TableStorage;").ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotEmitHeaderDirectives()
        {
            generator.ModelType = typeof(MockModelA);
            generator.IncludeHeaderDirectives = false;
            var code = generator.TransformText();

            code.Contains("//   Generated code.").ShouldBe(false);
            code.Contains("using Open.Core.Cloud.TableStorage;").ShouldBe(false);
        }
        #endregion
    }
}
