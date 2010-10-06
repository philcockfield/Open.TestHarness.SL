using System;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class IconTest
    {
        #region Head
        private bool useGreyscale;

        public void TestInitialize()
        {
            TestHarness.Reset();
        }
        #endregion

        #region Tests
        public void Toggle_UseGreyscale()
        {
            useGreyscale = !useGreyscale;
            Log.Info("Using Greyscale Version: " + useGreyscale);
        }

        public void SilkAccept() { Insert("SilkAccept"); }
        public void SilkAdd() { Insert("SilkAdd.png"); }
        public void SilkArrowBranch() { Insert("SilkArrowBranch"); }
        #endregion

        #region Internal
        private  void Insert(string name)
        {
            TestHarness.AddElement(Helper.Icon.Image(name, useGreyscale));
            Log.Info("Icon: " + Html.ToHyperlink(Helper.Icon.Path(name, useGreyscale)));
        }
        #endregion
    }
}
