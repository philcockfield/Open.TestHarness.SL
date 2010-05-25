using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage.CodeGeneration;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [TestClass]
    public class TableServiceContextsTemplateTest : CloudTestBase
    {
        #region Head
        private TableServiceContextsTemplate generator;

        [TestInitialize]
        public void TestSetup()
        {
            generator = new TableServiceContextsTemplate();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHaveModelTypes()
        {
            generator.ModelTypes.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldHaveContextName()
        {
            TableServiceContextsTemplate.GetContextName(typeof(MockEntityA)).ShouldBe("MockEntityAContext");
        }

        [TestMethod]
        public void WriteToFile()
        {
            generator.ModelTypes.Add<MockEntityA>();
            generator.ModelTypes.Add<MockEntityB>();
            OutputFileWriter.Write("TableContexts.g.cs", generator.TransformText());
        }
        #endregion
    }
}
