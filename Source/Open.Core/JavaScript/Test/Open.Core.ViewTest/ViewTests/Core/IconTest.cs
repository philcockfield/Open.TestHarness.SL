using System;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class IconTest
    {
        #region Head
        public void TestInitialize()
        {
            TestHarness.Reset();
        }
        #endregion

        #region Tests
        public void SilkAccept() { Insert("SilkAccept"); }
        public void SilkAdd() { Insert("SilkAdd.png"); }
        public void SilkArrowBranch() { Insert("SilkArrowBranch"); }
        #endregion

        #region Internal
        private void Insert(string name)
        {
            TestHarness.AddHtml(Helper.Icon.Image(name));
        }
        #endregion
    }
}
