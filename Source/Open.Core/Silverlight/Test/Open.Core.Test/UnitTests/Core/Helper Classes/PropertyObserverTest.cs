using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Helper_Classes
{
    /// <remarks>
    ///    This class only tests the SL specific portions of the partial class.
    ///    The full set of tests for the 'PropertyObserver' are in the Core.Common.Test CLR project.
    /// </remarks>

    [Tag("p")]
    [TestClass]
    public class PropertyObserverTest : SilverlightUnitTest
    {
        #region Tests
        [TestMethod]
        public void ShouldRegisterHandler()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, s => { fireCount++; });

            stub.Text = "Hello";
            fireCount.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldUnregisterHandler()
        {
            var stub = new Stub();

            var fireCount = 0;
            var observer = new PropertyObserver<Stub>(stub).RegisterHandler(s => s.Text, s => { fireCount++; });
            observer.UnregisterHandler(s => s.Text);

            stub.Text = "Hello";
            fireCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldFireOnce()
        {
            var stub = new Stub();

            var fireCount = 0;
            Action handler = () => { fireCount++; };

            var observer = new PropertyObserver<Stub>(stub)
                .RegisterHandler(s => s.Text, handler)
                .RegisterHandler(s => s.Number, s => { });

            stub.Text = "Hello";
            stub.Number = 50;

            fireCount.ShouldBe(1);
        }
        #endregion

        #region Stubs
        public class Stub : ModelBase
        {
            public const string PropText = "Text";
            public const string PropNumber = "Number";
            private string text;
            private int number;

            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }

            public int Number
            {
                get { return number; }
                set { number = value; OnPropertyChanged(PropNumber); }
            }

            public object DoSomething() { return null; }
        }
        #endregion
    }
}
