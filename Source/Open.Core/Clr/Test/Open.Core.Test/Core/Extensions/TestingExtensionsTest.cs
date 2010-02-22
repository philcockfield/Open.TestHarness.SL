//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.Composite;

namespace Open.Core.Common.Test.Extensions
{
    [TestClass]
    public class TestingExtensionsTest
    {
        #region Basic Assertions
        [TestMethod]
        public void ShouldEvaluateShouldBeAssertion()
        {
            "one".ShouldBe("one");
            const string nullValue = null;
            nullValue.ShouldBe(null);
        }
        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldThrowErrorFromShouldBeAssertion()
        {
            "one".ShouldBe("two");
            const string nullValue = null;
            nullValue.ShouldBe("value");
        }

        [TestMethod]
        public void ShouldEvaluateShouldNotBeAssertion()
        {
            "one".ShouldNotBe("two");
            const string nullValue = null;
            nullValue.ShouldNotBe("value");
        }
        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldThrowErrorFromShouldNotBeAssertion()
        {
            "one".ShouldNotBe("one");
            const string nullValue = null;
            nullValue.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldCheckInstanceOfType()
        {
            Should.Throw<ArgumentNullException>(() => TestingExtensions.ShouldBeInstanceOfType<string>(null));

            "string".ShouldBeInstanceOfType<string>();
            Should.Throw<AssertionException>(() => DateTime.Now.ShouldBeInstanceOfType<string>());

            var childType = new ApplicationException();
            childType.ShouldBeInstanceOfType<Exception>();
        }

        [TestMethod]
        public void ShouldPerformActionOnFailureOverrides()
        {
            var failedCount = 0;
            "one".ShouldBe("two", () => failedCount++);
            failedCount.ShouldBe(1);

            failedCount = 0;

            "one".ShouldNotBe("one", () => failedCount++);
            failedCount.ShouldBe(1);

            failedCount = 0;

            "string".ShouldBeInstanceOfType<double>(()=>failedCount++);
            failedCount.ShouldBe(1);

            // ---

            failedCount = 0;

            "one".ShouldBe("one", () => failedCount++);
            "one".ShouldNotBe("two", () => failedCount++);
            "string".ShouldBeInstanceOfType<string>(() => failedCount++);
            failedCount.ShouldBe(0);
        }
        #endregion

        #region Collection Assertions
        [TestMethod]
        public void ShouldContain()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ShouldContain("one");
            list.ShouldContain("one", "three");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldContainThrow()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ShouldContain("eight");
        }

        [TestMethod]
        public void ShouldNotContain()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ShouldNotContain("eight");
            list.ShouldNotContain("eight", "nine");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldNotContainThrow()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ShouldNotContain("one");
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldNotContainMultipleThrow()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ShouldNotContain("one", "two", "three", "nine");
        }

        [TestMethod]
        public void ShouldBeEmpty()
        {
            var list = new List<string> ();
            list.ShouldBeEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldBeEmptyThrow()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ShouldBeEmpty();
        }

        [TestMethod]
        public void ShouldNotBeEmpty()
        {
            var list = new List<string> { "one", "two", "three" };
            list.ShouldNotBeEmpty();
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldNotBeEmptyThrow()
        {
            var list = new List<string>();
            list.ShouldNotBeEmpty();
        }
        #endregion

        #region Event Assertions
        [TestMethod]
        public void ShouldFirePropertyChangedAssertion()
        {
            var model = new SampleModel();
            model.ShouldFirePropertyChanged(() => model.Text = "Hello", SampleModel.PropText);

            model.ShouldFirePropertyChanged(() =>
                        {
                            model.Text = "Hello";
                            model.Number = 123;
                        }, SampleModel.PropText, SampleModel.PropNumber);
        }

        [TestMethod]
        [ExpectedException(typeof(AssertionException))]
        public void ShouldThrowWhenMultiplePropertiesPassedButOneDidNotFire()
        {
            var model = new SampleModel();
            model.ShouldFirePropertyChanged(() =>
                        {
                            model.Text = "Hello";
                        }, SampleModel.PropText, SampleModel.PropNumber);
        }

        [TestMethod]
        public void ShouldFireFromLambdaNameReference()
        {
            var model = new SampleModel();
            model.ShouldFirePropertyChanged<SampleModel>(() =>
                                                {
                                                    model.Text = "Hello";
                                                    model.Number++;
                                                }, m => m.Text, m => m.Number);
        }

        [TestMethod]
        public void ShouldFireMultiplePropertiesChangedAssertion()
        {
            var model = new SampleModel();
            model.ShouldFirePropertyChanged(() => model.Text = "Hello", SampleModel.PropText);
        }

        [TestMethod]
        public void ShouldNotFirePropertyChangedAssertion()
        {
            var model = new SampleModel();
            model.ShouldNotFirePropertyChanged(() => model.FireTextProperty(0), SampleModel.PropText);
            model.ShouldNotFirePropertyChanged(() => model.FireTextProperty(0), SampleModel.PropText, SampleModel.PropNumber);
            Should.Throw<AssertionException>(() => model.ShouldNotFirePropertyChanged(() =>
                                                                                          {
                                                                                              model.Text = "Hello";
                                                                                              model.Number++;
                                                                                          }, SampleModel.PropText, SampleModel.PropNumber));
        }

        [TestMethod]
        public void ShouldNotFirePropertyChangedAssertionFromLamdaNameReference()
        {
            var model = new SampleModel();
            model.ShouldNotFirePropertyChanged<SampleModel>(() => model.FireTextProperty(0), m => m.Text, m => m.Number);
            Should.Throw<AssertionException>(() => model.ShouldNotFirePropertyChanged<SampleModel>(() =>
                                                                                        {
                                                                                            model.Text = "Hello";
                                                                                            model.Number++;
                                                                                        }, m => m.Text, m => m.Number));
        }

        [TestMethod]
        public void ShouldFirePropertyChangedTotalTimesAssertion()
        {
            var model = new SampleModel();
            model.ShouldFirePropertyChanged(() => model.FireTextProperty(3), SampleModel.PropText);
        }

        [TestMethod]
        public void ShouldFirePropertyChangedTotalTimesAssertionFromLamdaPropertyName()
        {
            var model = new SampleModel();
            model.ShouldFirePropertyChanged<SampleModel>(() => model.FireTextProperty(3), m => m.Text);
        }

        [TestMethod]
        public void ShouldThrowWhenFirePropertyChangedCorrectTotalTimes()
        {
            var model = new SampleModel();
            Should.Throw<AssertionException>(() => model.ShouldFirePropertyChanged(3, () => model.FireTextProperty(2), SampleModel.PropText));
        }

        public class SampleModel : ModelBase
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
            public void FireTextProperty(int total)
            {
                for (var i = 0; i < total; i++) OnPropertyChanged(PropText);
            }
        }
        #endregion

        #region Property Name Constant Assertions
        [TestMethod]
        public void ShouldHaveCorrectPropertyNameConstants()
        {
            typeof(Stub1).ShouldHavePropertyNameConstants();
        }

        [TestMethod]
        public void ShouldCatchIncorrectPropertyNameInConstant()
        {
            Should.Throw<AssertionException>(() => typeof(Stub2).ShouldHavePropertyNameConstants());
        }

        [TestMethod]
        public void ShouldCatchMissingProperty()
        {
            Should.Throw<AssertionException>(() => typeof(Stub3).ShouldHavePropertyNameConstants());
        }

        [TestMethod]
        public void ShouldThrowWhenTypeNotPassedTo_ShouldHavePropertyNameConstants()
        {
            Should.Throw<ArgumentNullException>(() => ((Type)null).ShouldHavePropertyNameConstants());
        }
        #endregion

        #region Assembly Assertions
        [TestMethod]
        public void ShouldIntantiateAllTypes()
        {
            var assembly = GetType().Assembly;
            assembly.ShouldIntantiateAllTypes<ConstructorStub>();
            assembly.ShouldIntantiateAllTypes<ConstructorStubChild>();
        }

        [TestMethod]
        public void ShouldFailToIntantiateTypes()
        {
            var assembly = GetType().Assembly;
            Should.Throw<AssertionException>(() => assembly.ShouldIntantiateAllTypes<ConstructorStubFailureChild>());
        }
        #endregion

        #region EventBus Assertions
        [TestMethod]
        public void ShouldDoNothingWhenUnsubscribingFromEventThatHasNotBeenSubscribedTo()
        {
            var aggregator = new EventBus();
            aggregator.Unsubscribe<MyEvent>(obj => { });

            // --

            Action<MyEvent> action1 = e => { };
            Action<MyEvent> action2 = e => { };

            aggregator.Subscribe(action1);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);

            aggregator.Unsubscribe(action2);
            aggregator.SubscribedCount<MyEvent>().ShouldBe(1);
        }

        [TestMethod]
        public void ShouldFireTestMethod_FireAtLeastOnce()
        {
            var aggregator = new EventBus();
            aggregator.ShouldFire<MyEvent>(() => aggregator.Publish(new MyEvent()));
            aggregator.ShouldFire<MyEvent>(() =>
                        {
                            aggregator.Publish(new MyEvent());
                            aggregator.Publish(new MyEvent());
                        });
            Should.Throw<AssertionException>(() => aggregator.ShouldFire<MyEvent>(() => { }));
        }

        [TestMethod]
        public void ShouldFireTestMethod_SpecificNumberOfTimes()
        {
            var aggregator = new EventBus();
            aggregator.ShouldFire<MyEvent>(2, () =>
                        {
                            aggregator.Publish(new MyEvent());
                            aggregator.Publish(new MyEvent());
                        });
            Should.Throw<AssertionException>(() => aggregator.ShouldFire<MyEvent>(2, () =>
                        {
                            aggregator.Publish(new MyEvent());
                            aggregator.Publish(new MyEvent());
                            aggregator.Publish(new MyEvent());
                        }));

            Should.Throw<AssertionException>(() => aggregator.ShouldFire<MyEvent>(2, () => { }));
        }

        [TestMethod]
        public void ShouldNotFireTestMethod()
        {
            var aggregator = new EventBus();
            aggregator.ShouldNotFire<MyEvent>(() => { });
            Should.Throw<AssertionException>(() => aggregator.ShouldNotFire<MyEvent>(() => aggregator.Publish(new MyEvent())));
        }


        [TestMethod]
        public void ShouldRetainOriginalAsyncSetting()
        {
            var aggregator = new EventBus();

            aggregator.IsAsynchronous.ShouldBe(true);
            aggregator.ShouldFire<MyEvent>(() => aggregator.Publish(new MyEvent()));
            aggregator.IsAsynchronous.ShouldBe(true);

            aggregator.IsAsynchronous = false;
            aggregator.ShouldFire<MyEvent>(() => aggregator.Publish(new MyEvent()));
            aggregator.IsAsynchronous.ShouldBe(false);
        }

        public class MyEvent{}
        #endregion

        #region Sample Data
        public class Stub1 : ViewModelBase
        {
            public const string OtherConstant = "Not a match";
            public const string propText = "Not a match";
            public const int PropNumber = 5;
            public const string PropText = "Text";
            private string text;
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }
        }

        private class Stub2 : ViewModelBase
        {
            public const string PropNumber = "Numbr";
            public const string PropText = "Txt";
            private int number;
            private string text;
            public int Number
            {
                get { return number; }
                set { number = value; OnPropertyChanged(PropNumber); }
            }
            public string Text
            {
                get { return text; }
                set { text = value; OnPropertyChanged(PropText); }
            }
        }

        private class Stub3 : ViewModelBase
        {
            public const string PropText = "Text";
        }
        #endregion
    }

    public class ConstructorStub { }
    public class ConstructorStubChild { }
    public class ConstructorStubFailure
    {
        public ConstructorStubFailure()
        {
            throw new InitializationException("Test error");
        }
    }
    public class ConstructorStubFailureChild : ConstructorStubFailure{}


}
