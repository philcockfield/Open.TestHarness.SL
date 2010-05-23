using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.Controls
{
    [ViewTestClass]
    public class ViewFactoryContentViewTest
    {
        #region Head

        private MockViewFactory viewFactory;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            control.Width = 300;
            control.Height = 200;
            viewFactory = new MockViewFactory();

            Set_ViewFactory(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set_ViewFactory(ViewFactoryContent control)
        {
            control.ViewFactory = viewFactory;
        }

        [ViewTest]
        public void Clear_ViewFactory(ViewFactoryContent control)
        {
            control.ViewFactory = null;
        }
        #endregion

        public class MockViewFactory : ViewModelBase, IViewFactory
        {
            private DateTime created;

            public MockViewFactory()
            {
                created = DateTime.Now;
            }

            public FrameworkElement CreateView()
            {
                return new Placeholder
                           {
                               Text = string.Format("View Model Created At: {0}",  created.TimeOfDay), 
                               ShowInstanceCount = true
                           };
            }
        }

    }
}
