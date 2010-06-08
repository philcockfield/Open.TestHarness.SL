using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.Controls.ToolBar
{
    [ViewTestClass]
    public class ToolBarViewTest
    {
        #region Head
        private Thickness defaultToolMargin;
        private IButtonTool largeButton;

        [Import]
        public ExportFactory<IButtonTool> ToolCreator { get; set; }

        [Import(RequiredCreationPolicy = CreationPolicy.NonShared)]
        public IToolBar ToolBar { get; set; }


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ContentControl control)
        {
            CompositionInitializer.SatisfyImports(this);

            control.Width = 600;
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
            // Setup initial conditions.
            ToolBar.Clear();

            // Add buttons.
            largeButton = AddLargeButton(1, 0);
            ToolBar.AddDivider(column: 1, rowSpan: 3);
            
            var smallButton = ToolBar.AddButton(2, IconImage.SilkCut, "Cut", column: 2, row: 0, columnSpan: 3);
            ToolBar.AddButton(3, IconImage.SilkPageCopy, "Copy", column: 2, row: 1, columnSpan: 3);
            ToolBar.AddButton(4, IconImage.SilkClock, "Something", column: 2, row: 2, columnSpan: 3).IsEnabled = false;

            ToolBar.AddCustomTool(new PlaceholderTool(), column: 5, rowSpan: 3);

            ToolBar.AddSpacer(column:6);
            AddLargeButton(null, 7);

            // Finish up.
            ToolBar.UpdateLayout();
        }

        private IButtonTool AddLargeButton(object id, int column)
        {
            return ToolBar.AddButton(
                                    id,
                                    "/Images/Icon.Clipboard.png".ToImageSource().ToImage(),
                                    "Paste" + Environment.NewLine + "Something",
                                    Orientation.Vertical,
                                    column: column,
                                    rowSpan: 3);
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
        public void Toggle__DefaultMargin(ContentControl control)
        {
            ToolBar.DefaultToolMargin = ToolBar.DefaultToolMargin.Left == 0
                            ? defaultToolMargin 
                            : new Thickness(0);
            UpdateLayout(control);
            Output.Write("DefaultToolMargin: " + ToolBar.DefaultToolMargin); 
        }

        [ViewTest]
        public void Toggle__Toolbar_Margin(ContentControl control)
        {
            ToolBar.Margin = ToolBar.Margin.Left == 0
                            ? new Thickness(10)
                            : new Thickness(0);
        }

        [ViewTest]
        public void Write_Properties(ContentControl control)
        {
            Output.WriteProperties(ToolBar);
            Output.WriteCollection(ToolBar.Tools);
            Output.Break();
        }

        [ViewTest]
        public void Register_RegisterFileSaveDialog(ContentControl control)
        {
            if (largeButton == null) return;
            largeButton.RegisterAsFileSaveDialog(
                                "XML (.xml)|*.xml", 1, ".xml",
                                acceptedDialog =>
                                    {
                                        Output.Write("SafeFileName: " + acceptedDialog.SafeFileName);
                                        Output.Break();
                                    });
        }

        [ViewTest]
        public void Register_RegisterFileOpenDialog(ContentControl control)
        {
            if (largeButton == null) return;
            largeButton.RegisterAsFileOpenDialog(
                                "Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*",
                                2,
                                true,
                                acceptedDialog =>
                                    {
                                        Output.Write("File: " + acceptedDialog.File);
                                        Output.Write("Files: " + acceptedDialog.Files);
                                        Output.WriteCollection(acceptedDialog.Files);
                                        Output.Break();
                                    });
        }
        #endregion

        public class MockTool : ToolBase
        {
            public double Width = 16;
            public double Height = 16;

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

        public class PlaceholderTool : IViewFactory
        {
            public FrameworkElement CreateView()
            {
                return new Placeholder
                           {
                               Text = "Custom Tool", 
                               Width = 180,
                               Height = 66
                           };
            }
        }
    }
}
