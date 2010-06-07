using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Base_Classes
{
    [TestClass]
    public class AutoPropertyManagerTest
    {
        #region Tests
        [TestMethod]
        public void ShouldDispose()
        {
            var mock = new Mock1 { Text = "123" };
            mock.Property.ValueCount.ShouldBe(1);
            mock.Property.Dispose();
            mock.Property.ValueCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldNotStoreNewValuesAfterDisposal()
        {
            var mock = new Mock1 { Text = "123" };
            mock.Property.Dispose();
            mock.Property.ValueCount.ShouldBe(0);

            mock.Text = "Cat";
            mock.Property.ValueCount.ShouldBe(0);
        }

        [TestMethod]
        public void ShouldStoreSimpleValue()
        {
            var mock = new Mock1 { Text = "123" };
            mock.Text.ShouldBe("123");
        }

        [TestMethod]
        public void ShouldStoreObjectReference()
        {
            var child = new Mock1 {Text = "Hello"};
            var mock = new Mock2 {Child = child};
            mock.Child.Text.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldNotStoreSameValueTwice()
        {
            var mock = new Mock1();
            mock.Property.SetValue<Mock1, string>(m => m.Text, "abc").ShouldBe(true);
            mock.Property.SetValue<Mock1, string>(m => m.Text, "abc").ShouldBe(false);
        }

        [TestMethod]
        public void ShouldReturnDefaultValue()
        {
            var mock = new Mock1();
            mock.Property.GetValue<Mock1, string>(m => m.Text, "cat").ShouldBe("cat");
            mock.Property.GetValue<Mock1, string>(m => m.Text, "cat").ShouldBe("cat");
        }

        [TestMethod]
        public void ShouldFireSinglePropertyChangedEvent()
        {
            var mock = new Mock1();
            mock.ShouldFirePropertyChanged<Mock1>(1, () => mock.Text = "cat", m => m.Text);
        }

        [TestMethod]
        public void ShouldFireMultiplePropertyChangedEvents()
        {
            var mock = new Mock2();
            mock.ShouldFirePropertyChanged<Mock2>(() => mock.Child = new Mock1(), m => m.Text, m => m.Child);
            mock.ShouldFirePropertyChanged<Mock2>(1, () => mock.Child = new Mock1(), m => m.Child);
            mock.ShouldFirePropertyChanged<Mock2>(1, () => mock.Child = new Mock1(), m => m.Text);
        }

        [TestMethod]
        public void ShouldNotFireSinglePropertyChangedEvent()
        {
            var mock = new Mock1{Text = "abc"};
            mock.ShouldNotFirePropertyChanged<Mock1>(() => mock.Text = "abc", m => m.Text);
            mock.ShouldNotFirePropertyChanged<Mock1>(() => mock.Text = "abc", m => m.Text);
            mock.ShouldNotFirePropertyChanged<Mock1>(() => mock.Text = "abc", m => m.Text);
        }

        [TestMethod]
        public void ShouldNotFirePropertyChangedWhenReadingFromDefaultProperty()
        {
            var mock = new Mock3();
            mock.ShouldNotFirePropertyChanged<Mock3>(() => mock.MyText.ShouldBe("foo"), m => m.MyText);
            mock.Property.OnSetValueCount.ShouldBe(1); // Default value written to store.
        }


        [TestMethod]
        public void ShouldCallOnReadPropertyOverride()
        {
            var mock = new Mock1();
            mock.Property.OnReadValueCount.ShouldBe(0);

            var value = mock.Text;
            mock.Property.OnReadValueCount.ShouldBe(1);
            mock.Property.OnReadValueProperty.Name.ShouldBe("Text");
        }

        [TestMethod]
        public void ShouldCallOnSetValueOverrideWhileReadingDefaultValue()
        {
            var mock = new Mock1();
            var value = mock.Text;

            mock.Property.OnSetValueCount.ShouldBe(1);
            mock.Property.OnSetValueProperty.Name.ShouldBe("Text");
        }

        [TestMethod]
        public void ShouldCallSetValueOverride()
        {
            var mock = new Mock1();
            mock.Property.OnSetValueCount.ShouldBe(0);

            mock.Text = "cat";
            mock.Property.OnSetValueCount.ShouldBe(2); // Called twice. (1) Checking for default value, (2) setting new, non-default, value.
            mock.Property.OnSetValueProperty.Name.ShouldBe("Text");
        }

        [TestMethod]
        public void ShouldCallReadWritePropertyOverridesMultipleTimes()
        {
            var mock = new Mock1();
            mock.Text = "1";
            mock.Text = "2";
            mock.Text = "3";

            var value = mock.Text;
            value = mock.Text;
            value = mock.Text;

            mock.Property.OnReadValueCount.ShouldBe(6); // Read on each write operation + the 3 reads.
            mock.Property.OnSetValueCount.ShouldBe(4); // Additional write operation on the first write, where the default value is stored.
        }

        [TestMethod]
        public void ShouldFirePropertySetEvent()
        {
            var mock = new Mock1();

            var fireCount = 0;
            AutoPropertySetEventArgs args = null;
            mock.Property.PropertySet += (s, e) =>
                                             {
                                                 fireCount++;
                                                 args = e;
                                             };

            mock.Text = "1";
            mock.Text = "1";
            mock.Text = "1";
            mock.Text = "2";

            fireCount.ShouldBe(2);
            args.Property.ShouldBe(typeof(Mock1).GetProperty("Text"));
            args.Value.ShouldBe("2");
        }

        [TestMethod]
        public void ShouldGetSerializedValues()
        {
            var manager = new AutoPropertyManager();
            manager.GetSerializedValues().Length.ShouldNotBe(0);

            var mock = new Mock1 { Text = "cat" };

            var serializedValues = mock.Property.GetSerializedValues();
            serializedValues.Length.ShouldNotBe(0);
        }

        [TestMethod]
        public void ShouldPopulateFromSerializedString()
        {
            var child = new MockSimple2{ Text = "Hello", Date = DateTime.Now, Number = 42};
            var manager = new AutoPropertyManager();
            manager.SetValue<MockSimple1, MockSimple2>(m => m.Child, child);

            var json = manager.GetSerializedValues();
            Debug.WriteLine("GetSerializedValues: ");
            Debug.WriteLine(json);
            Debug.WriteLine("");

            // ---

            var rebuild = new Mock2();
            rebuild.Property.Populate(json);
            var childRebuilt = manager.GetValue<MockSimple1, MockSimple2>(m => m.Child);
            childRebuilt.Text.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldCreateFromSerializedString()
        {
            var child = new MockSimple2 { Text = "Hello", Date = DateTime.Now, Number = 42 };
            var manager1 = new AutoPropertyManager();
            manager1.SetValue<MockSimple1, MockSimple2>(m => m.Child, child);

            var json = manager1.GetSerializedValues();
            var manager2 = AutoPropertyManager.Create(json);

            var rebuild = new Mock2();
            rebuild.Property.Populate(json);
            var childRebuilt = manager2.GetValue<MockSimple1, MockSimple2>(m => m.Child);
            childRebuilt.Text.ShouldBe("Hello");
        }

        [TestMethod]
        public void ShouldClear()
        {
            var mock = new Mock1 { Text = "cat" };
            mock.Text.ShouldBe("cat");

            mock.Property.Clear();
            mock.Text.ShouldBe(null);

            mock.Property.Clear();
            mock.Text.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldChangeSerializationWhenCleared()
        {
            var mock = new Mock1 { Text = "cat" };
            var serialzied1 = mock.Property.GetSerializedValues();
            
            mock.Property.Clear();
            var serialzied2 = mock.Property.GetSerializedValues();

            serialzied1.ShouldNotBe(serialzied2);
        }
        #endregion

        #region Stubs
        public class MockSimple1
        {
            public DateTime Date { get; set; }
            public MockSimple2 Child { get; set; }
        }

        public class MockSimple2
        {
            public string Text { get; set; }
            public double Number { get; set; }
            public DateTime Date { get; set; }
        }

        public class AutoPropertyManagerTester : AutoPropertyManager
        {
            public AutoPropertyManagerTester(Action<PropertyChangedEventArgs> firePropertyChanged)
                : base(firePropertyChanged)
            {
            }
            public int OnReadValueCount;
            public int OnSetValueCount;
            public PropertyInfo OnReadValueProperty;
            public PropertyInfo OnSetValueProperty;

            protected override bool OnReadValue<T>(PropertyInfo property, out T value)
            {
                OnReadValueCount++;
                OnReadValueProperty = property;
                return base.OnReadValue(property, out value);
            }

            protected override void OnWriteValue<T>(PropertyInfo property, T value, bool isDefaultValue)
            {
                OnSetValueCount++;
                OnSetValueProperty = property;
                base.OnWriteValue(property, value, isDefaultValue);
            }
        }

        public abstract class MockBase : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;
            private void FirePropertyChanged(PropertyChangedEventArgs e) { if (PropertyChanged != null) PropertyChanged(this, e); }
            protected MockBase()
            {
                Property = new AutoPropertyManagerTester(FirePropertyChanged);
            }
            public AutoPropertyManagerTester Property { get; private set; } // Only public for testing.  Keep this protected in implementation.
        }

        public class Mock1 : MockBase
        {
            public string Text
            {
                get { return Property.GetValue<Mock1, string>(m => m.Text); }
                set { Property.SetValue<Mock1, string>(m => m.Text, value); }
            }
        }

        public class Mock2 : Mock1
        {
            public Mock1 Child
            {
                get { return Property.GetValue<Mock2, Mock1>(m => m.Child); }
                set { Property.SetValue<Mock2, Mock1>(m => m.Child, value, m => m.Text); }
            }
        }

        public class Mock3 : MockBase
        {
            public string MyText
            {
                get { return Property.GetValue<Mock3, string>(m => m.MyText, "foo"); }
                set { Property.SetValue<Mock3, string>(m => m.MyText, value, "foo"); }
            }
        }
        #endregion
    }
}
