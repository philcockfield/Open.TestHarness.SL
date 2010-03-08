using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Composite.Command;
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
            control.Width = 300;
            control.StretchContent();
            control.Background = Colors.Black.ToBrush(0.08);

            CompositionInitializer.SatisfyImports(this);
            control.Content = ToolBar.CreateView();

            Add_Tools_Two_Rows(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Add_Tools_Single_Row(ContentControl control)
        {
            ToolBar.Clear();
            ToolBar.Add(MockTool.Create());
            ToolBar.Add(MockTool.Create());
            ToolBar.Add(MockTool.Create());

            ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void Add_Tools_Two_Rows(ContentControl control)
        {
            ToolBar.Clear();
            ToolBar.Add(MockTool.Create(54, 54), column: 0, rowSpan: 2);
            ToolBar.Add(MockTool.Create(), column: 1);
            ToolBar.Add(MockTool.Create(), column: 2);
            ToolBar.Add(MockTool.Create(), column: 3);
            ToolBar.Add(MockTool.Create(width: 50), column: 1, row: 1, columnSpan: 3);

            ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void UpdateLayout(ContentControl control)
        {
            ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void Write_Properties(ContentControl control)
        {
            Output.WriteProperties(ToolBar);
            Output.WriteCollection(ToolBar.Tools);
            Output.Break();
        }
        #endregion
        
        public class MockTool : ToolBase
        {
            public double Width= 16;
            public double Height = 16;

            public MockTool()
            {
                Command = new DelegateCommand<Button>(button => Output.Write("Click"));
            }

            public override FrameworkElement CreateView()
            {
                var button = new Button{Width = Width, Height = Height};
                Composite.Command.Click.SetCommand(button, Command);
                return button;
            }

            public static MockTool Create(int width = 24, int height = 24)
            {
                return new MockTool{Width = width, Height = height};
            }
        }
    }
}
