using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated;
using Open.Core.Common.Testing;

using TModel = Open.Core.Cloud.Test.TableStorage.CodeGeneration.MockModelA;
using TEntity = Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated.MockModelATableEntity;

namespace Open.Core.Cloud.Test.TableStorage
{
    [TestClass]
    public class TableModelBaseTest : CloudTestBase
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
            
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldHavePropertyManagerFromEntityConstructor()
        {
            var entity = new MockModelATableEntity();
            var model = new MockModel(entity);
            model.Property.BackingEntity.ShouldBe(entity);
        }

        [TestMethod]
        public void ShouldHavePropertyManagerFromPropertyManagerConstructor()
        {
            var entity = new MockModelATableEntity();
            var propManager = new TablePropertyManager<TModel, TEntity>(entity);

            var model = new MockModel(propManager);
            model.Property.ShouldBe(propManager);
        }
        #endregion
    }

    public class MockModel : TableModelBase<TModel, TEntity>
    {
        public MockModel(TEntity entity) : base(entity){}
        public MockModel(TablePropertyManager<TModel, TEntity> propertyManager): base(propertyManager){}

        public new TablePropertyManager<TModel, TEntity> Property { get { return base.Property; } }
    }
}
