using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.UnitTest
{
    [TestClass]
    public class FooTest
    {

        [TestMethod]
        public void ShouldDo()
        {
            // NB: Fails.
            var foo = new MyView();
            foo.Background.ShouldBe(null);
        }


        public class MyView : ViewBase
        {
            
        }

    }


}
