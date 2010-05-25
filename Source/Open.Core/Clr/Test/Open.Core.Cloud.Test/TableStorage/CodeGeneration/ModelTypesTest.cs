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
    public class ModelTypesTest
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
            modelTypes.Add<MockEntityA>();
            modelTypes.Types.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldNotInsertSameModelTypeTwice()
        {
            modelTypes.Add<MockEntityA>();
            modelTypes.Add<MockEntityA>();
            modelTypes.Add<MockEntityA>();
            modelTypes.Types.Count().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldAllowDifferentModelTypesToBeAdded()
        {
            modelTypes.Add<MockEntityA>();
            modelTypes.Add<MockEntityB>();
            modelTypes.Types.Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldRemoveModelType()
        {
            modelTypes.Add<MockEntityA>();
            modelTypes.Types.Count().ShouldBe(1);

            modelTypes.Remove<MockEntityA>();
            modelTypes.Types.Count().ShouldBe(0);

            modelTypes.Remove<MockEntityA>();
            modelTypes.Remove<MockEntityA>();
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
        #endregion

    }
}
