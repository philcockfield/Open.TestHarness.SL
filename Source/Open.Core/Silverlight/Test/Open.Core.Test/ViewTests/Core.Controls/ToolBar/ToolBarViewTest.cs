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
        private Thickness defaultToolMargin;

        [Import(typeof(IButtonTool))]
        public ExportFactory<IButtonTool> ToolCreator { get; set; }

        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IToolBar ToolBar { get; set; }


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ContentControl control)
        {
            CompositionInitializer.SatisfyImports(this);

            control.Width = 300;
            control.StretchContent();
            control.Background = Colors.Black.ToBrush(0.08);

            control.Content = ToolBar.CreateView();

            defaultToolMargin = ToolBar.DefaultToolMargin;

            Add_ButtonTools(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Add_ButtonTools(ContentControl control)
        {
            ToolBar.Clear();

            //var buttonTool = ToolCreator.CreateExport().Value;
            //buttonTool.Text = "My Label";
            //buttonTool.Icon = IconImage.SilkAccept.ToImage();

            ToolBar.Clear();
            ToolBar.AddButton(
                    "/Images/Icon.Clipboard.png".ToImageSource().ToImage(), 
                    "Paste", 
                    Orientation.Vertical, 
                    column: 0, 
                    rowSpan: 3);
            ToolBar.AddButton(IconImage.SilkCut, "Cut", column: 1, row: 0, columnSpan: 3);
            ToolBar.AddButton(IconImage.SilkPageCopy, "Copy", column: 1, row: 1, columnSpan: 3);
            ToolBar.AddButton(IconImage.SilkClock, "Something", column: 1, row: 2, columnSpan: 3);
            ToolBar.UpdateLayout();

            //ToolBar.AddButton(IconImage.SilkAccept, showDefaultBackground: false, column: 1);
            //ToolBar.AddButton(IconImage.SilkCake, showDefaultBackground: true, column: 2);
            //ToolBar.AddButton(IconImage.SilkConnect, showDefaultBackground: true, column: 3);

        }


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
        public void Toggle_DefaultMargin(ContentControl control)
        {
            ToolBar.DefaultToolMargin = ToolBar.DefaultToolMargin.Left == 0 
                            ? ToolBar.DefaultToolMargin = defaultToolMargin 
                            : new Thickness(0);
            UpdateLayout(control);
            Output.Write("DefaultToolMargin: " + ToolBar.DefaultToolMargin); 
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
            public double Width = 16;
            public double Height = 16;

            public MockTool()
            {
            }

            public override FrameworkElement CreateView()
            {
                var button = new Button { Width = Width, Height = Height };
                return button;
            }

            public static MockTool Create(int width = 24, int height = 24)
            {
                return new MockTool { Width = width, Height = height };
            }
        }
    }
}
