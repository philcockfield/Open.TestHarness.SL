using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage.Converters
{
    [TestClass]
    public class EnumToIntConverterTest : CloudTestBase
    {
        #region Head
        public enum MyEnum
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3
        }
        private EnumToIntConverter converter;

        [TestInitialize]
        public void TestSetup()
        {
            converter = new EnumToIntConverter();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldConvertToEnumFromInt()
        {
            converter.ToSource(0).ShouldBe((int)MyEnum.Zero);
            converter.ToSource(1).ShouldBe((int)MyEnum.One);
            converter.ToSource(2).ShouldBe((int)MyEnum.Two);
            converter.ToSource(3).ShouldBe((int)MyEnum.Three);
        }

        [TestMethod]
        public void ShouldConvertToIntFromEnum()
        {
            converter.ToTarget(MyEnum.Zero).ShouldBe(0);
            converter.ToTarget(MyEnum.One).ShouldBe(1);
            converter.ToTarget(MyEnum.Two).ShouldBe(2);
            converter.ToTarget(MyEnum.Three).ShouldBe(3);
        }
        #endregion
    }
}
