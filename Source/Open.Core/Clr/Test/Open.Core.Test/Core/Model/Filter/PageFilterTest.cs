using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Model.Filter
{
    [TestClass]
    public class PageFilterTest
    {
        [TestMethod]
        public void ShouldHaveDefaultValues()
        {
            var model = new PageFilter();
            model.TakeCount.ShouldBe(int.MaxValue);
            model.SkipCount.ShouldBe(0);
        }
    }
}
