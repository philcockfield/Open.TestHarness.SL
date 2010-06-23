using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;
using System.ComponentModel.Composition;

namespace Open.Core.Test.ViewTests.Core.UI.Controls.Lists
{
    [ViewTestClass]
    public class NamedControlListViewTest
    {
        #region Head
        [Import]
        public INamedControlList ViewModel { get; set; }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewFactoryContent control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.Width = 300;
            control.Height = 300;
            control.ViewFactory = ViewModel;
            
            ViewModel.ControlMargin = new Thickness(40,0,0,0);
            ViewModel.ItemSpacing = 30;

            for (int i = 0; i < 4; i++)
            {
                Add(control);
            }

            ViewModel.Items.ElementAt(1).Margin = new Thickness(10,0,30,0);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Add(ViewFactoryContent control)
        {
            ViewModel.Add(CreateTitle(), new MyViewFactory());
        }

        [ViewTest]
        public void Insert(ViewFactoryContent control)
        {
            ViewModel.Insert(0, CreateTitle(), new MyViewFactory());
        }

        [ViewTest]
        public void Remove_First__via_Item(ViewFactoryContent control)
        {
            if (ViewModel.Items.Count == 0) return;
            var item = ViewModel.Items.First();
            ViewModel.Remove(item);
        }

        [ViewTest]
        public void Remove_First__via_Control(ViewFactoryContent control)
        {
            if (ViewModel.Items.Count == 0) return;
            var item = ViewModel.Items.First();
            ViewModel.Remove(item.Control);
        }

        [ViewTest]
        public void Toggle_TextWrapping(ViewFactoryContent control)
        {
            ViewModel.TitleFont.TextWrapping = ViewModel.TitleFont.TextWrapping.NextValue<TextWrapping>();
            Output.Write("TextWrapping: " + ViewModel.TitleFont.TextWrapping);
        }

        [ViewTest]
        public void Toggle_ControlMargin(ViewFactoryContent control)
        {
            ViewModel.ControlMargin = ViewModel.ControlMargin.Left == 0 ? new Thickness(40, 0, 0, 0) : new Thickness(0);
        }

        [ViewTest]
        public void Toggle_ItemSpacing(ViewFactoryContent control)
        {
            ViewModel.ItemSpacing = ViewModel.ItemSpacing == 0 ? 30 : 0;
            Output.Write("ItemSpacing: " + ViewModel.ItemSpacing);
        }
        #endregion

        #region Internal
        private string CreateTitle()
        {
            return string.Format("{0} - {1}", ViewModel.Items.Count, RandomData.LoremIpsum(3, 8));
        }
        #endregion

        public class MyViewFactory : IViewFactory
        {
            public FrameworkElement CreateView()
            {
                return new Border
                           {
                               Background = new SolidColorBrush(Colors.Orange),
                               Height = 50,
                           };
            }
        }
    }
}
