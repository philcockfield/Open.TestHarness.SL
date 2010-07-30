using System.ComponentModel.Composition;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI.Controls
{
    [ViewTestClass]
    public class PagerViewTest
    {
        #region Head
        [Import]
        public IPager Pager { get; set; }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.ViewFactory = Pager;
            Pager.CurrentIndexChanged += delegate { Output.Write("!! CurrentIndexChanged: " + Pager.CurrentPage); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Change__PageCount(ViewFactoryContent control)
        {
            Pager.TotalPages = Pager.TotalPages == 15 ? 100 : 15;
        }

        [ViewTest]
        public void Change__PageButtonCount(ViewFactoryContent control)
        {
            Pager.TotalPageButtons = Pager.TotalPageButtons == 2 ? 5 : 2;
        }

        [ViewTest]
        public void CurrentPageIndex__Increase(ViewFactoryContent control) { Pager.CurrentPageIndex++; }

        [ViewTest]
        public void CurrentPageIndex__Decrease(ViewFactoryContent control) { Pager.CurrentPageIndex--; }

        [ViewTest]
        public void CurrentPage__Increase(ViewFactoryContent control) { Pager.CurrentPage++; }

        [ViewTest]
        public void CurrentPage__Decrease(ViewFactoryContent control) { Pager.CurrentPage--; }

        [ViewTest]
        public void TotalPages__Reduce(ViewFactoryContent control) { Pager.TotalPages--; }

        [ViewTest]
        public void TotalPages__Increase(ViewFactoryContent control) { Pager.TotalPages++; }

        [ViewTest]
        public void Toggle__IsEnabled(ViewFactoryContent control) { Pager.IsEnabled = !Pager.IsEnabled; }

        [ViewTest]
        public void Toggle__IsLoading(ViewFactoryContent control) { Pager.IsLoading = !Pager.IsLoading; }

        [ViewTest]
        public void Write_Properties(ViewFactoryContent control)
        {
            Output.WriteProperties(Pager);
        }
        #endregion
    }
}
