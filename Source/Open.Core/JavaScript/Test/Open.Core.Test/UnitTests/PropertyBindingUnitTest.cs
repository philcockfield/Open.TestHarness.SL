using System;

namespace Open.Core.Test.UnitTests
{
    public class PropertyBindingUnitTest
    {
        #region Head
        public const string PropText = "Text";
        private BindingModel sourceModel;
        private BindingModel targetModel;
        private PropertyRef sourceProp;
        private PropertyRef targetProp;

        public void TestInitialize()
        {
            sourceModel = new BindingModel();
            targetModel = new BindingModel();

            sourceProp = sourceModel.GetPropertyRef(PropText);
            targetProp = targetModel.GetPropertyRef(PropText);
        }
        #endregion

        #region Tests
        public void ShouldBindOneWay()
        {
            targetProp.BindTo(sourceProp, BindingMode.OneWay);
            WriteModels("Before");

            // --

            sourceModel.Text = "Foo";
            WriteModels("After change to 'Foo'");
            Assert.That(targetModel.Text).Is("Foo");

            // --

            // No change - only one-way binding
            targetModel.Text = "Bar";
            Assert.That(sourceModel.Text).Is("Foo");
        }

        public void ShouldClearBinding()
        {
            targetProp.BindTo(sourceProp, BindingMode.OneWay);
            sourceModel.Text = "Foo";
            Assert.That(targetModel.Text).Is("Foo");
            
            // --

            WriteModels("Before");
            Log.Info("ClearBinding");
            targetProp.ClearBinding();

            // --

            sourceModel.Text = "Bar";
            WriteModels("After Changed to 'Bar'");
            Assert.That(targetModel.Text).Is("Foo");
        }

        public void ShouldChangeBinding()
        {
            targetProp.BindTo(sourceProp, BindingMode.OneWay);
            sourceModel.Text = "Foo";
            Assert.That(targetModel.Text).Is("Foo");

            // --

            WriteModels("Before");

            // Change binding.
            Log.Info("Bind to another source");

            BindingModel source2 = new BindingModel();
            targetProp.BindTo(source2.GetPropertyRef(PropText));

            source2.Text = "Zana";
            sourceModel.Text = "Another value";
            WriteModels("After change to second bound source ('Zana')");
            Assert.That(targetModel.Text).Is("Zana");
        }

        public void ShouldSetValueOnBind()
        {
            sourceModel.Text = "One";
            targetProp.BindTo(sourceProp, BindingMode.OneWay);
            WriteModels("Bound with initial value of 'One'.");
            Assert.That(targetModel.Text).Is("One");
        }

        public void ShouldBindTwoWays()
        {
            targetProp.BindTo(sourceProp, BindingMode.TwoWay);
            sourceModel.Text = "Foo";
            WriteModels("After source changed to 'Foo'.");
            Assert.That(targetModel.Text).Is("Foo");

            targetModel.Text = "Bar";
            WriteModels("After target changed to 'Bar'.");
            Assert.That(sourceModel.Text).Is("Bar");
        }
        #endregion

        #region Internal
        private void WriteModels(string title )
        {
            Log.Title(title);
            Log.Info("source.Text: " + Helper.String.FormatToString(sourceModel.Text));
            Log.Info("target.Text: " + Helper.String.FormatToString(targetModel.Text));
            Log.LineBreak();
        }
        #endregion
    }

    #region Sample Models
    public class BindingModel : ModelBase
    {
        public const string PropText = "Text";
        public string Text
        {
            get { return (string) Get(PropText, null); }
            set { Set(PropText, value, null); }
        }
    }
    #endregion
}
