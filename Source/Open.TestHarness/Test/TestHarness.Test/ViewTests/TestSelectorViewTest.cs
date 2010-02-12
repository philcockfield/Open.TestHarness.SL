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
using Open.Core.UI.Controls;
using Open.TestHarness.Model;
using Open.TestHarness.View.Selector;

namespace Open.TestHarness.Test.ViewTests
{
    [ViewTestClass]
    public class TestSelectorViewTest
    {
        #region Head
        private TestSelectorViewModel viewModel;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(TestSelector control)
        {
            control.Width = 320;
            viewModel = new TestSelectorViewModel();
            control.ViewModel = viewModel;

            Set_Model__This(control);
        }
        #endregion

        #region Tests

        [ViewTest]
        public void Set_Model__This(TestSelector control)
        {
            SetModel(GetType());
        }

        [ViewTest]
        public void Set_Model__Null(TestSelector control)
        {
            viewModel.Model = null;
        }

        [ViewTest]
        public void Set_Model__No_ViewTests(TestSelector control)
        {
            SetModel(typeof(MyViewTestClass));
        }

        [ViewTest]
        public void Set_Model__Tests_With_Enums(TestSelector control)
        {
            SetModel(typeof(MyViewTestClassWithEnums));
        }
        #endregion

        #region Internal
        private void SetModel(Type viewTestClassType)
        {
            viewModel.Model = new ViewTestClass(viewTestClassType, "File.xap");
        }
        #endregion

        [ViewTestClass]
        public class MyViewTestClass { }

        [ViewTestClass]
        public class MyViewTestClassWithEnums
        {
            [ViewTest]
            public void MyTest(Placeholder control, Visibility visibility)
            {
            }

        }

    }
}
