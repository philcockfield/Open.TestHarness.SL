using System.ComponentModel.Composition;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI.Controls.Prompts
{
    [ViewTestClass]
    public class PromptButtonBarViewTest
    {
        #region Head
        [Import]
        public IPromptButtonBar ButtonBar { get; set; }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(PromptButtonsTestControl control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.Width = 800;
            control.content.ViewFactory = ButtonBar;

            ButtonBar.Background.Color = Colors.Black.ToBrush();
            ButtonBar.Background.Opacity = 0.1;

            ButtonBar.Buttons.Click += (s, e) => Output.Write("!! Click: " + e.ButtonType);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Change_Configuration(PromptButtonsTestControl control, PromptButtonConfiguration configuration = PromptButtonConfiguration.YesNoCancel)
        {
            ButtonBar.Buttons.Configuration = configuration;
        }

        [ViewTest]
        public void Toggle__Alignment(PromptButtonsTestControl control)
        {
            ButtonBar.Alignment = ButtonBar.Alignment == HorizontalEdge.Left ? HorizontalEdge.Right : HorizontalEdge.Left;
        }

        [ViewTest]
        public void Toggle__IsVisible(PromptButtonsTestControl control)
        {
            ButtonBar.IsVisible = !ButtonBar.IsVisible;
        }
        #endregion
    }
}
