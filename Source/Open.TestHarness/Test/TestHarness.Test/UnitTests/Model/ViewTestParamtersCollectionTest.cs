using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.TestHarness.Model;
using Open.Core.Common.Testing;

namespace Open.TestHarness.Test.UnitTests.Model
{
    [TestClass]
    public class ViewTestParamtersCollectionTest
    {
        #region Head
        public enum MethodName
        {
            MethodZero,
            MethodOne,
            MethodTwo,
            MethodFour,
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldStoreViewTest()
        {
            var viewTest = GetViewTest(MethodName.MethodOne);
            var collection = new ViewTestParametersCollection(viewTest);
            collection.ViewTest.ShouldBe(viewTest);
        }

        [TestMethod]
        public void ShouldHaveMatchingParameters()
        {
            GetParamsCollection(MethodName.MethodZero).Items.Count().ShouldBe(0);
            GetParamsCollection(MethodName.MethodOne).Items.Count().ShouldBe(1);
            GetParamsCollection(MethodName.MethodTwo).Items.Count().ShouldBe(2);
        }

        [TestMethod]
        public void ShouldMatchParameterTypes()
        {
            var collection = GetParamsCollection(MethodName.MethodTwo);
            collection.Items.ElementAt(0).Type.ShouldBe(typeof(Placeholder));
            collection.Items.ElementAt(1).Type.ShouldBe(typeof(Visibility));
        }

        [TestMethod]
        public void ShouldHaveTwoControlsInCollection()
        {
            var controlsCollection = new CurrentControlsCollection();
            var viewTest = GetViewTest(MethodName.MethodFour);
            controlsCollection.Populate(viewTest);

            controlsCollection.Count.ShouldBe(2);
        }

        [TestMethod]
        public void ShouldPopulateParamsCollectionWithInstances()
        {
            var controlsCollection = new CurrentControlsCollection();
            var paramsCollection = GetParamsCollection(MethodName.MethodFour);

            controlsCollection.Populate(paramsCollection);

            paramsCollection.Items.ElementAt(0).Value.ShouldBe(controlsCollection[0]);
            paramsCollection.Items.ElementAt(3).Value.ShouldBe(controlsCollection[1]);
        }

        [TestMethod]
        public void ShouldConvertToAnArrayOfValues()
        {
            var paramsCollection = GetParamsCollection(MethodName.MethodFour);
            var items = paramsCollection.Items;

            items.ElementAt(0).Value = new Placeholder();
            items.ElementAt(1).Value = Visibility.Visible;
            items.ElementAt(2).Value = "Hello";
            items.ElementAt(3).Value = new Grid();

            var array = paramsCollection.ToArray();
            array[0].ShouldBe(items.ElementAt(0).Value);
            array[1].ShouldBe(items.ElementAt(1).Value);
            array[2].ShouldBe(items.ElementAt(2).Value);
            array[3].ShouldBe(items.ElementAt(3).Value);
        }
        #endregion

        #region Sample Methods
        private ViewTestParametersCollection GetParamsCollection(MethodName methodName)
        {
            return new ViewTestParametersCollection(GetViewTest(methodName));
        }

        private ViewTest GetViewTest(MethodName methodName)
        {
            var method = GetType().GetMethod(methodName.ToString());
            return new ViewTest(method);
        }

        [ViewTest]
        public void MethodZero(){}

        [ViewTest]
        public void MethodOne(Border control){}

        [ViewTest]
        public void MethodTwo(Placeholder control, Visibility visibility){}

        [ViewTest]
        public void MethodFour(Placeholder control, Visibility visibility, string text, Grid grid) { }
        #endregion
    }
}
