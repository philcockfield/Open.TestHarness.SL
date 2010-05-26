using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Extensions
{
    [TestClass]
    public class EventExtensionsTest
    {

        [TestMethod]
        public void ShouldFirePropertyChangedViaExtensionMethod()
        {
            var mock = new Mock();
            mock.ShouldFirePropertyChanged<Mock>(mock.FireTest, m => m.Text);
        }

        private class Mock : ModelBase
        {
            public string Text { get; private set; }
            public void FireTest()
            {
                this.FirePropertyChanged<Mock>(OnPropertyChanged, m => m.Text);
            }
        }
    }
}
