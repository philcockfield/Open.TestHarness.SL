using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI.Controls.Dialogs
{
    [ViewTestClass(SizeMode = TestControlSize.FillWithMargin)]
    public class DropdownDialogViewTest
    {
        #region Head
        [Import]
        public IDropdownDialog Dialog { get; set; }
        private MyContent myContent = new MyContent();

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.ViewFactory = Dialog;

            DelayedAction.Invoke(0.6, action: () => Show(control));
//            DelayedAction.Invoke(0.6, action: () => Load_ModelessMessageContent(control));

            Dialog.Showing += delegate { Output.Write(Colors.Green, "!! Showing"); };
            Dialog.Shown += delegate { Output.Write(Colors.Red, "!! Shown"); Output.Break(); };
            Dialog.Hiding += delegate { Output.Write(Colors.Green, "!! Hiding"); };
            Dialog.Hidden += delegate { Output.Write(Colors.Red, "!! Hidden"); Output.Break(); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Show(ViewFactoryContent control)
        {
            Dialog.Show(
                    myContent,
                    result => Output.Write("Callback - OnComplete: " + result));
        }

        [ViewTest]
        public void Show(
                            ViewFactoryContent control,
                            PromptButtonConfiguration buttonConfiguration = PromptButtonConfiguration.YesNoCancel,
                            DialogSize sizeMode = DialogSize.Fill)
        {
            Dialog.Show(
                            myContent,
                            result => Output.Write("Callback - OnComplete: " + result),
                            sizeMode: sizeMode,
                            buttonConfiguration: buttonConfiguration);
        }

        [ViewTest]
        public void Toggle__IsShowing(ViewFactoryContent control)
        {
            Set__Content(control);
            Dialog.IsShowing = !Dialog.IsShowing;
            Output.Write("IsShowing: " + Dialog.IsShowing);
        }

        [ViewTest]
        public void Disable_Buttons(ViewFactoryContent control)
        {
            foreach (PromptResult buttonType in typeof(PromptResult).GetEnumValues())
            {
                Dialog.ButtonBar.Buttons.GetButton(buttonType).IsEnabled = false;
            }
        }

        [ViewTest]
        public void Enable_Buttons(ViewFactoryContent control)
        {
            foreach (PromptResult buttonType in typeof(PromptResult).GetEnumValues())
            {
                Dialog.ButtonBar.Buttons.GetButton(buttonType).IsEnabled = true;
            }
        }

        private ModelessMessageDialogContentViewModel modelessMessage;
        [ViewTest]
        public void Load_ModelessMessageContent(ViewFactoryContent control)
        {
            if (modelessMessage == null) modelessMessage = new ModelessMessageDialogContentViewModel();
            modelessMessage.Text = RandomData.LoremIpsum(5, 50);
            modelessMessage.Icon.Source = IconImage.SilkError.ToImageSource();
            modelessMessage.Initialize(Dialog);
            Dialog.Show(modelessMessage, result => Output.Write("Callback - OnComplete: " + result));
        }

        [ViewTest]
        public void Change_Button_Configuration(ViewFactoryContent control, PromptButtonConfiguration configuration = PromptButtonConfiguration.YesNoCancel)
        {
            Dialog.ButtonBar.Buttons.Configuration = configuration;
        }

        [ViewTest]
        public void Change_SizeMode(ViewFactoryContent control, DialogSize sizeMode = DialogSize.Fill)
        {
            Dialog.SizeMode = sizeMode;
        }

        [ViewTest]
        public void Toggle_ButtonBar__IsVisible(ViewFactoryContent control)
        {
            Dialog.ButtonBar.IsVisible = !Dialog.ButtonBar.IsVisible;
        }

        [ViewTest]
        public void Set__Content(ViewFactoryContent control)
        {
            Dialog.Content = myContent;
        }

        [ViewTest]
        public void Set__Content_Null(ViewFactoryContent control)
        {
            Dialog.Content = null;
        }

        [ViewTest]
        public void Change_Content_Width(ViewFactoryContent control)
        {
            myContent.Width = myContent.Width==500 ? 600 : 500;
        }

        [ViewTest]
        public void Change_Content_Height(ViewFactoryContent control)
        {
            myContent.Height = myContent.Height == 200 ? 300 : 200;
        }

        [ViewTest]
        public void Toggle__DropShadowOpacity(ViewFactoryContent control)
        {
            Dialog.DropShadowOpacity = Dialog.DropShadowOpacity == 0 ? 0.3 : 0;
        }

        [ViewTest]
        public void Toggle__Margin(ViewFactoryContent control)
        {
            Dialog.Margin = Dialog.Margin.Left == 0 ? new Thickness(20) : new Thickness(0);
        }
        #endregion

        #region Mocks
        public class MyContent : ViewModelBase, IViewFactory
        {
            public double Width
            {
                get { return Property.GetValue<MyContent, double>(m => m.Width, 500); }
                set { Property.SetValue<MyContent, double>(m => m.Width, value, 500); }
            }

            public double Height
            {
                get { return Property.GetValue<MyContent, double>(m => m.Height, 200); }
                set { Property.SetValue<MyContent, double>(m => m.Height, value, 200); }
            }

            public FrameworkElement CreateView()
            {
                var view = new SampleDialogContent { DataContext = this };
                view.SetBinding(FrameworkElement.WidthProperty, new Binding("Width"));
                view.SetBinding(FrameworkElement.HeightProperty, new Binding("Height"));
                return view;
            }
        }
        #endregion
    }
}
