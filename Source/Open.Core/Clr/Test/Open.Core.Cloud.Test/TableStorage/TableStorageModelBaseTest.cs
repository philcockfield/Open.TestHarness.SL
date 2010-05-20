﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Cloud.TableStorage;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test.TableStorage
{
    [TestClass]
    public class TableStorageModelBaseTest
    {
        #region Head

        [TestInitialize]
        public void TestSetup()
        {
            
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldBeModel()
        {
            var mock = new Mock();
            mock.ShouldBeInstanceOfType<TableStorageModelBase>();
        }
        #endregion

        public class Mock : TableStorageModelBase
        {
            
        }

    }
}