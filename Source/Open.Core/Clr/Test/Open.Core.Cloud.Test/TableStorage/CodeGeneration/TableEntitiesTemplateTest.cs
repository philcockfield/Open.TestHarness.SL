﻿using System;
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
    public class TableEntitiesTemplateTest : CloudTestBase
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
        public void ShouldHaveModelTypes()
        {
            generator.ModelTypes.ShouldNotBe(null);
        }

        [TestMethod]
        public void WriteToFile()
        {
            generator.ModelTypes.Add<MockEntityA>();
            generator.ModelTypes.Add<MockEntityB>();
            OutputFileWriter.Write("TableEntities.g.cs", generator.TransformText());
        }
        #endregion
    }
}
