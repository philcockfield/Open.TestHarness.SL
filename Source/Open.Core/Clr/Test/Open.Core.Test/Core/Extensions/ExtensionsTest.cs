using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Extensions
{
    [TestClass]
    public class ExtensionsTest
    {

        [TestMethod]
        public void ShouldInvokeAction()
        {
            var count = 0;
            Action action = () => { count++; };

            action.InvokeOrDefault();
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldTakeNoAction()
        {
            ((Action)null).InvokeOrDefault();
        }

    }
}
