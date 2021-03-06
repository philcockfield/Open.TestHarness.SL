﻿using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.Composite;
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

        [Import]
        public IEventBus EventBus { get; set; }


        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(Border control)
        {
            CompositionInitializer.SatisfyImports(this);

            control.Width = 800;
            control.Background = Colors.Black.ToBrush(0.03);
            control.Child = ToolBar.CreateView();

            defaultToolMargin = ToolBar.DefaultToolMargin;

//            Add_ButtonTools(control);
            Add_ToolGroups(control);
        }
        #endregion

        #region Tests 

        [ViewTest]
        public void Clear(Border control)
        {
            ToolBar.Clear();
            ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void Add_Empty_ToolGroup(Border control)
        {
            ToolBar.Clear();
            var group1 = ToolBar.AddToolGroup(title: "One");
            var group2 = ToolBar.AddToolGroup(title: "Two");
            var group3 = ToolBar.AddToolGroup(title: "Three");
            ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void Add_ToolGroups(Border control, bool updateLayout = true)
        {
            // Setup initial conditions.
            ToolBar.Clear();
            ToolBar.Title.IsVisible = false;

            var group1 = ToolBar.AddToolGroup(title:"Group One", column:0, rowSpan:3);
            var group2 = ToolBar.AddToolGroup(title: "Group Two", column: 1, rowSpan: 3);
            ToolBar.AddSpacer(column:2);
            var group3 = ToolBar.AddToolGroup(title: "Group Three", column: 3, rowSpan: 3);
            group3.Dividers = RectEdgeFlag.Left;

            // ---

            AddButtonSet(group1);
            AddButtonSet(group2);
            group3.AddCustomTool(new PlaceholderTool(), "custom", column: 0, rowSpan: 3);

            // ---

            if (updateLayout) ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void Add_ButtonTools(Border control)
        {
            // Setup initial conditions.
            ToolBar.Clear();

            AddButtonSet(ToolBar);
            ToolBar.AddCustomTool(new PlaceholderTool(), "custom", column: 5, rowSpan: 3);

            ToolBar.AddSpacer(column:6);
            AddLargeButton(null, 7);

            // Finish up.
            ToolBar.UpdateLayout();
            Show__Both_Dividers(control);
        }

        private void AddButtonSet(IToolBar toolbar)
        {
            largeButton = AddLargeButton(1, 0, toolbar);
            toolbar.AddDivider(column: 1, rowSpan: 3);

            var smallButton = toolbar.AddButton(2, IconImage.SilkCut, "Cut", column: 2, row: 0, columnSpan: 3);
            toolbar.AddButton(3, IconImage.SilkPageCopy, "Copy", column: 2, row: 1, columnSpan: 3);
            toolbar.AddButton(4, IconImage.SilkClock, "Something", column: 2, row: 2, columnSpan: 3).IsEnabled = false;
        }

        private IButtonTool AddLargeButton(object id, int column, IToolBar toolbar = null)
        {
            if (toolbar == null) toolbar = ToolBar;
            return toolbar.AddButton(
                                    id,
                                    "/Images/Icon.Clipboard.png".ToImageSource().ToImage(),
                                    "Paste" + Environment.NewLine + "Something",
                                    Orientation.Vertical,
                                    column: column,
                                    rowSpan: 3);
        }

        [ViewTest]
        public void Add_Tools_Single_Row(Border control)
        {
            ToolBar.Clear();
            ToolBar.Add(MockTool.Create());
            ToolBar.Add(MockTool.Create());
            ToolBar.Add(MockTool.Create());

            ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void Add_Tools_Two_Rows(Border control)
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
        public void UpdateLayout(Border control)
        {
            ToolBar.UpdateLayout();
        }

        [ViewTest]
        public void Fire_UpdateToolbarLayoutEvent(Border control)
        {
            EventBus.Publish(new UpdateToolbarLayoutEvent());
        }

        [ViewTest]
        public void Toggle__DefaultMargin(Border control)
        {
            ToolBar.DefaultToolMargin = ToolBar.DefaultToolMargin.Left == 0
                            ? defaultToolMargin 
                            : new Thickness(0);
            UpdateLayout(control);
            Output.Write("DefaultToolMargin: " + ToolBar.DefaultToolMargin); 
        }

        [ViewTest]
        public void Toggle__Toolbar_Margin(Border control)
        {
            ToolBar.Margin = ToolBar.Margin.Left == 0
                            ? new Thickness(10)
                            : new Thickness(0);
        }

        [ViewTest]
        public void Write_Properties(Border control)
        {
            Output.WriteProperties(ToolBar);
            Output.WriteCollection(ToolBar.Tools);
            Output.Break();
        }

        [ViewTest]
        public void Register_RegisterFileSaveDialog(Border control)
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
        public void Register_RegisterFileOpenDialog(Border control)
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

        [ViewTest]
        public void Show__Left_Divider(Border control) { ToolBar.Dividers = RectEdgeFlag.Left; }

        [ViewTest]
        public void Show__Right_Divider(Border control) { ToolBar.Dividers = RectEdgeFlag.Right; }

        [ViewTest]
        public void Show__Both_Dividers(Border control) { ToolBar.Dividers = RectEdgeFlag.Left | RectEdgeFlag.Right; }

        [ViewTest]
        public void Show__No_Dividers(Border control) { ToolBar.Dividers = RectEdgeFlag.None; }

        [ViewTest]
        public void Toggle__IsEnabled(Border control)
        {
            ToolBar.IsEnabled = !ToolBar.IsEnabled;
            Output.Write("IsEnabled: " + ToolBar.IsEnabled);
        }

        [ViewTest]
        public void Toggle_CustomTool__IsEnabled(Border control)
        {
            var tool = ToolBar.GetTool("custom");
            if (tool ==null)
            {
                Output.Write("Custom Tool Not Found");
                return;
            }

            tool.IsEnabled = !tool.IsEnabled;
            Output.Write("IsEnabled: " + tool.IsEnabled);
        }

        [ViewTest]
        public void Toggle__IsVisible(Border control)
        {
            ToolBar.IsVisible = !ToolBar.IsVisible;
            Output.Write("IsVisible: " + ToolBar.IsVisible);
        }

        [ViewTest]
        public void Toggle__Title_IsVisible(Border control)
        {
            ToolBar.Title.IsVisible = !ToolBar.Title.IsVisible;
            Output.Write("ToolBar.Title.IsVisible: " + ToolBar.Title.IsVisible);
        }

        [ViewTest]
        public void Toggle__Title_ShowBackground(Border control)
        {
            ToolBar.Title.ShowBackground = !ToolBar.Title.ShowBackground;
            Output.Write("ToolBar.Title.ShowBackground: " + ToolBar.Title.ShowBackground);
        }

        [ViewTest]
        public void Change_Title__Name(Border control)
        {
            ToolBar.Title.Name = RandomData.LoremIpsum(1, 3);
        }

        [ViewTest]
        public void Create_New_Title(Border control)
        {
            ToolBar.Title = new ToolBarTitleViewModel();
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

        public class PlaceholderTool : ViewModelBase, IViewFactory
        {
            public bool IsEnabled
            {
                get { return GetPropertyValue<PlaceholderTool, bool>(m => m.IsEnabled, true); }
                set { SetPropertyValue<PlaceholderTool, bool>(m => m.IsEnabled, value, true); }
            }

            public FrameworkElement CreateView()
            {
                var checkbox = new CheckBox { Content = "My Custom Tool" };
                var border = new Border
                                 {
                                     Background = new SolidColorBrush(Colors.Orange) {Opacity = 0.3},
                                     Width = 180,
                                     Height = 66,
                                     Child = checkbox
                                 };
                checkbox.SetBinding(Control.IsEnabledProperty, new Binding("IsEnabled"));
                return border;
            }
        }
    }
}
