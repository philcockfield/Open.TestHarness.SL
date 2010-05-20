using System.Diagnostics;
using System.Windows;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Silverlight.Test.View_Tests.Common.Behavior
{
    [ViewTestClass]
    public class UpdateOnKeyPressViewTestBase<T> where T : FrameworkElement
    {
        #region Head
        private UpdateCharacterFieldOnKeyPressBase<T> behavior;
        private readonly SampleModel model = new SampleModel();

        public virtual void Initialize(UpdateOnKeyPressTestControl control, UpdateCharacterFieldOnKeyPressBase<T> fieldBehavior)
        {
            behavior = fieldBehavior;
            control.Width = 200;
            behavior.Updated += delegate
                                    {
                                        Output.Write("Updated - model.Text: " + model.Text);
                                    };

            Set_DataContext(control);
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Set_DataContext(UpdateOnKeyPressTestControl control)
        {
            control.DataContext = model;
        }

        [ViewTest]
        public void Set_DataContext_Null(UpdateOnKeyPressTestControl control)
        {
            control.DataContext = null;
        }

        [ViewTest]
        public void Set_Delay_0(UpdateOnKeyPressTestControl control) { behavior.Delay = 0; }

        [ViewTest]
        public void Set_Delay_Fast(UpdateOnKeyPressTestControl control) { behavior.Delay = 0.1; }

        [ViewTest]
        public void Set_Delay_Slow(UpdateOnKeyPressTestControl control) { behavior.Delay = 1; }

        [ViewTest]
        public void Toggle_IsActive(UpdateOnKeyPressTestControl control)
        {
            behavior.IsActive = !behavior.IsActive;
            Debug.WriteLine("IsActive: " + behavior.IsActive);
        }
        #endregion

        #region Sample Data
        public class SampleModel : ModelBase
        {
            public const string PropText = "Text";
            private string text = "My Value";
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }
        }
        #endregion
    }
}
