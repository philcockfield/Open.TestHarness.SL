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
            generator.ModelType = typeof(MockEntityA);
            OutputFileWriter.Write("MockEntityA.g.cs", generator.TransformText());
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

            generator.ModelType = typeof(MockEntityA);
            generator.Namespace.ShouldBe(GetType().Namespace + ".Generated");
        }

        [TestMethod]
        public void ShouldEmitEntityWithinNamespace()
        {
            generator.ModelType = typeof (MockEntityA);
            generator.TransformText().Contains("namespace " + generator.Namespace).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldConstructNameOfEntityBasedOnModelType()
        {
            generator.ModelType = null;
            generator.ClassName.ShouldBe(null);

            generator.ModelType = typeof(MockEntityA);
            generator.ClassName.ShouldBe("MockEntityATableEntity");
        }

        [TestMethod]
        public void ShouldEmitEntityWithCorrectClassName()
        {
            generator.ModelType = typeof(MockEntityA);
            generator.TransformText().Contains("public partial class " + generator.ClassName).ShouldBe(true);
        }

        [TestMethod]
        public void ShouldEmitConstructors()
        {
            generator.ModelType = typeof(MockEntityA);
            var code = generator.TransformText();
            code.Contains("public MockEntityATableEntity()").ShouldBe(true);
            code.Contains("public MockEntityATableEntity(string partitionKey, string rowKey)").ShouldBe(true);
        }

        [TestMethod]
        public void ShouldFindPropertiesDecoratedWithPersistAttribute()
        {
            var type = typeof (MockEntityA);
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

            generator.ModelType = typeof(MockEntityA);
            generator.InterfaceName.ShouldBe("IMockEntityATableEntity");
        }

        [TestMethod]
        public void ShouldIncludeHeaderDirectivesByDefault()
        {
            generator.IncludeHeaderDirectives.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldEmitHeaderDirectives()
        {
            generator.ModelType = typeof (MockEntityA);
            generator.IncludeHeaderDirectives = true;
            var code = generator.TransformText();

            code.Contains("//   Generated code.").ShouldBe(true);
            code.Contains("using Open.Core.Cloud.TableStorage;").ShouldBe(true);
        }

        [TestMethod]
        public void ShouldNotEmitHeaderDirectives()
        {
            generator.ModelType = typeof(MockEntityA);
            generator.IncludeHeaderDirectives = false;
            var code = generator.TransformText();

            code.Contains("//   Generated code.").ShouldBe(false);
            code.Contains("using Open.Core.Cloud.TableStorage;").ShouldBe(false);
        }
        #endregion
    }
}
