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

        public void SilkAccept() { FromName("SilkAccept"); }
        public void SilkAdd() { FromName("SilkAdd.png"); }
        public void SilkArrowBranch() { FromName("SilkArrowBranch"); }

        public void From_Enum__SilkAccept() { FromEnum(Icons.SilkAccept); }
        #endregion

        #region Internal
        private  void FromName(string name)
        {
            Log.Info("Icon: " + Html.ToHyperlink(Helper.Icon.Path(name, useGreyscale)));
            TestHarness.AddElement(Helper.Icon.Image(name, useGreyscale));
        }

        private void FromEnum(Icons icon)
        {
            Log.Info("Icon: " + Html.ToHyperlink(Helper.Icon.Path(icon, useGreyscale)));
            TestHarness.AddElement(Helper.Icon.Image(icon, useGreyscale));
        }
        #endregion
    }
}
