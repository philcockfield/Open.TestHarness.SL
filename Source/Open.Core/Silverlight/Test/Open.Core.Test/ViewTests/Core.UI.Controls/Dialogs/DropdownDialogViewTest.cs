using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Open.Core.Common;
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

            Set__Content(control);
            DelayedAction.Invoke(0.3, action: () => Dialog.IsShowing = true);
//            Dialog.IsShowing = true;

            Dialog.Showing += delegate { Output.Write(Colors.Green, "!! Showing"); };
            Dialog.Shown += delegate { Output.Write(Colors.Red, "!! Shown"); Output.Break(); };
            Dialog.Hiding += delegate { Output.Write(Colors.Green, "!! Hiding"); };
            Dialog.Hidden += delegate { Output.Write(Colors.Red, "!! Hidden"); Output.Break(); };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Toggle__IsShowing(ViewFactoryContent control)
        {
            Dialog.IsShowing = !Dialog.IsShowing;
            Output.Write("IsShowing: " + Dialog.IsShowing);
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
                var view = new Placeholder
                           {
                               DataContext = this,
                               Text = "Content",
                           };

                view.SetBinding(FrameworkElement.WidthProperty, new Binding("Width"));
                view.SetBinding(FrameworkElement.HeightProperty, new Binding("Height"));

                return view;
            }
        }
    }
}
