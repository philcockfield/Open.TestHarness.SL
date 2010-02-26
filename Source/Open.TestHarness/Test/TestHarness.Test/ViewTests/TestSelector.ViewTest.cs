using System;
using System.Collections.Generic;
using System.Windows;
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

        public enum MyEnum{One, Two, Three}

        [ViewTestClass]
        public class MyViewTestClassWithEnums
        {

            [ViewTest]
            public void MyTest1(Placeholder control, Visibility visibility)
            {
                Output.Write("Method Invoked - MyTest1", "Visibility: " + visibility);
            }

            [ViewTest]
            public void MyTest2(Placeholder control, Visibility visibility, MyEnum myEnum)
            {
                Output.Write("Method Invoked - MyTest2", "Visibility: " + visibility, "MyEnum: " + myEnum);
            }

        }
    }
}
