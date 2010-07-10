using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.TableStorage.CodeGeneration;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [TestClass]
    public class ModelTypesTest : CloudTestBase
    {
        #region Head
        private ModelTypes modelTypes;

        [TestInitialize]
        public void TestSetup()
        {
            modelTypes = new ModelTypes();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldAddModelType()
        {
            modelTypes.Types.Count().ShouldBe(0);
            modelTypes.Add<MockModelA>();
            modelTypes.Types.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInsertSameModelTypeTwice()
        {
            modelTypes.Add<MockModelA>();
            modelTypes.Add<MockModelA>();
            modelTypes.Add<MockModelA>();
            modelTypes.Types.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldAllowDifferentModelTypesToBeAdded()
        {
            modelTypes.Add<MockModelA>();
            modelTypes.Add<MockModelB>();
            modelTypes.Types.Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldRemoveModelType()
        {
            modelTypes.Add<MockModelA>();
            modelTypes.Types.Count().ShouldBe(1);

            modelTypes.Remove<MockModelA>();
            modelTypes.Types.Count().ShouldBe(0);

            modelTypes.Remove<MockModelA>();
            modelTypes.Remove<MockModelA>();
        }

        [TestMethod]
        public void ShouldAddMultipleClassesFromAssembly()
        {
            var types = GetType().Assembly.GetTableModelTypes();
            modelTypes.Add(GetType().Assembly);
            modelTypes.Add(GetType().Assembly); // Should not add types more than once.

            modelTypes.Types.Count().ShouldBe(types.Count());
            foreach (var type in types)
            {
                modelTypes.Types.Contains(type).ShouldBe(true);
            }
        }

        [TestMethod]
        public void ShouldThrowIfTypeNotDecoratedWithPersistClassAttribute()
        {
            Should.Throw<ArgumentOutOfRangeException>(() => modelTypes.Add<NonPersistableClass>());
        }
        #endregion
    }
}
