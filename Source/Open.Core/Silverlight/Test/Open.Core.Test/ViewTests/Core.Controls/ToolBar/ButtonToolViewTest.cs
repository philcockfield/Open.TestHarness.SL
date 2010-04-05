using System.ComponentModel.Composition;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.Controls.ToolBar
{
    [ViewTestClass (DisplayName = "ToolBar: ButtonTool")]
    public class ButtonToolViewTest
    {
        #region Head
        [Import(typeof(IButtonTool))]
        public ExportFactory<IButtonTool> ToolCreator { get; set; }

        private IButtonTool tool;
        private IconImage currentSmallIcon;
        private Image iconLarge;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ContentControl control)
        {
            // Setup initial conditions.
            CompositionInitializer.SatisfyImports(this);
            tool = ToolCreator.CreateExport().Value;
            control.Content = tool.CreateView();

            // Setup state.
            tool.ToolTip = "A tooltip that explains what this button does";
            Set_Text__Short(control);
            Set_Icon_Small__Next(control);
            iconLarge = new Image { Source = "/Images/Icon.Clipboard.png".ToImageSource(), Stretch = Stretch.None};

            // Wire up events.
            tool.Click += delegate { Output.Write("!! Click"); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set_Text__Short(ContentControl control)
        {
            tool.Text = "Copy";
        }

        [ViewTest]
        public void Set_Text__Long(ContentControl control)
        {
            if (tool.Orientation == Orientation.Horizontal)
            {
                tool.Text = "Format Painter";
            }
            else
            {
                tool.Text = string.Format("Two\rLines");
            }
        }

        [ViewTest]
        public void Set_Text__Null(ContentControl control)
        {
            tool.Text = null;
        }

        [ViewTest]
        public void Set_Icon_Small__Next(ContentControl control)
        {
            if (tool.Orientation != Orientation.Horizontal)
            {
                Output.Write("Can only cycle through icons when Tool is in a Horizontal orientation.");
                return;
            }
            currentSmallIcon = tool.Icon == null ? IconImage.SilkAccept : currentSmallIcon.NextValue<IconImage>();
            tool.Icon = currentSmallIcon.ToImage();
        }

        [ViewTest]
        public void Set_Icon__Null(ContentControl control)
        {
            tool.Icon = null;
        }

        [ViewTest]
        public void Toggle_Orientation(ContentControl control)
        {
            tool.Orientation = tool.Orientation.NextValue<Orientation>();

            if (tool.Orientation == Orientation.Horizontal)
            {
                tool.Icon = currentSmallIcon.ToImage();
            }
            else
            {
                tool.Icon = iconLarge;
            }

            Output.Write("Orientation: " + tool.Orientation);
        }

        [ViewTest]
        public void Set_ButtonType(ContentControl control, ButtonToolType buttonType = ButtonToolType.Split)
        {
            tool.ButtonType = buttonType;
        }

        [ViewTest]
        public void Set_ButtonType__NextValue(ContentControl control)
        {
            tool.ButtonType = tool.ButtonType.NextValue<ButtonToolType>();
            Output.Write("ButtonType: " + tool.ButtonType);
        }

        [ViewTest]
        public void Toggle__IsDefaultBackgroundVisible(ContentControl control)
        {
            tool.IsDefaultBackgroundVisible = !tool.IsDefaultBackgroundVisible;
            Output.Write("IsDefaultBackgroundVisible: " + tool.IsDefaultBackgroundVisible);
        }

        [ViewTest]
        public void Toggle__IsEnabled_on_Model(ContentControl control)
        {
            tool.IsEnabled = !tool.IsEnabled;
            Output.Write("tool.IsEnabled: " + tool.IsEnabled);
        }

        [ViewTest]
        public void Toggle__IsEnabled_on_View(ContentControl control)
        {
            control.IsEnabled = !control.IsEnabled;
            Output.Write("control.IsEnabled: " + control.IsEnabled);
        }

        [ViewTest]
        public void Write_Properties(ContentControl control)
        {
            Output.WriteProperties(tool, true);
        }
        #endregion
    }
}
