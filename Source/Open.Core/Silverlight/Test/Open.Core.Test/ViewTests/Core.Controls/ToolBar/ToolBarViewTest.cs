using System;
using System.ComponentModel.Composition;
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

namespace Open.Core.Test.ViewTests.Core.Controls.ToolBar
{
    [ViewTestClass]
    public class ToolBarViewTest
    {
        #region Head
        [Import]
        public IToolBar ToolBar { get; set; }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ContentControl control)
        {
            control.Width = 400;
            control.Height = 80;
            control.StretchContent();

            CompositionInitializer.SatisfyImports(this);
            control.Content = ToolBar.CreateView();
        }
        #endregion

    }
}
