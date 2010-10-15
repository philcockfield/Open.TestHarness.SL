using System;
using jQueryApi;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class PartTest
    {
        #region Head
        private jQueryObject container;

        public void ClassInitialize()
        {
            container = TestHarness.AddElement();
            Css.SetSize(container, 400, 300);
            container.CSS(Css.Background, Color.Red(0.1));
        }
        #endregion
    }
}
