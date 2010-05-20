using System;
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
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.Test.ViewTests.Core.UI.Behavior
{
    [ViewTestClass(DisplayName = "Behavior: ViewModelSetter")]
    public class ViewModelSetterViewTest
    {
        #region Head
        private MyModel model;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ViewModelSetterTestControl control)
        {
            model = new MyModel();
            control.DataContext = model;

            ViewModelSetter.RegisterFactory("MySetter", ViewModelFactory);
        }

        public object ViewModelFactory(object dataContext)
        {
            return new MyViewModel(dataContext as MyModel);
        }
        #endregion
    }

    public class MyModel : ModelBase
    {
        public string Text
        {
            get { return GetPropertyValue<MyModel, string>(m => m.Text); }
            set { SetPropertyValue<MyModel, string>(m => m.Text, value); }
        }
    }

    public class MyViewModel : ViewModelBase
    {
        private static int count;
        public MyViewModel(MyModel model)
        {
            count++;
            Model = model;
            Color = Colors.Green;
        }
        public MyModel Model { get; private set; }
        public string Text { get { return string.Format("{0}. {1}", count, Model.Text); } }
        public Color Color { get; private set; }
    }

}
