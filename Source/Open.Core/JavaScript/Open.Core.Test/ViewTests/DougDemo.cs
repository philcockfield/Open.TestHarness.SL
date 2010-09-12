// DougDemo.cs
//

using System;
using System.Collections;
using Open.Testing;

namespace Open.Core.Test.ViewTests
{
    public class DougDemo
    {
        #region Head

        private DougView view;

        public void ClassInitialize()
        {
            view = new DougView();
            view.SetSize(120, 300);
            TestHarness.AddControl(view);
        }
        public void ClassCleanup() { }

        public void TestInitialize() { }
        public void TestCleanup() { }
        #endregion

        #region Tests

        public void Toggle__Visibility()
        {
            view.IsVisible = !view.IsVisible;
        }

        public void Toggle__Opacity()
        {
            view.Opacity = view.Opacity == 1 ? 0.3 : 1;
        }

        public void ChangeText()
        {
          view.Foo("Yo!!!!!");  
        }

        #endregion
    }

    public class DougView : ViewBase
    {
        public DougView()
        {
            Initialize(Html.CreateDiv());

            Container.Append("<b>Hello</b> Doug");
            Container.CSS(Css.Background, "orange");
        }

        public void Foo(string text)
        {
            Container.Append(text);
            
        }

    }


}
