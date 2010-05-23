using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Cloud.Test
{
    [TestClass]
    public class CloudSettingsTest : CloudTestBase
    {
        [TestMethod]
        public void ShouldHaveCurrentSettingsAsSingleton()
        {
            Cloud.CloudSettings.Current.ShouldBe(base.CloudSettings);
        }
    }
}
