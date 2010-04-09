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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Open.Core.Composite;

namespace Open.Core.Common.Testing
{
    public static partial class TestingExtensions
    {
        #region Methods - Basic Assertions
        /// <summary>Asserts that the expected value is the same as the source object.</summary>
        /// <param name="o">The source object.</param>
        /// <param name="expected">The expected value.</param>
        public static void ShouldBe(this object o, object expected)
        {
            o.ShouldBe(expected, () => ThrowExpectedError(o, expected));
        }

        /// <summary>Asserts that the expected value is the same as the source object.</summary>
        /// <param name="o">The source object.</param>
        /// <param name="expected">The expected value.</param>
        /// <param name="onFailed">Action to perform if the asertion is not satisfied.</param>
        public static void ShouldBe(this object o, object expected, Action onFailed)
        {
            if (o == null && expected == null) return;
            if (o != null && o.Equals(expected)) return;
            if (onFailed != null) onFailed();
        }

        /// <summary>Asserts that the expected value is not the same as the source object.</summary>
        /// <param name="o">The source object.</param>
        /// <param name="notExpected">The value that is not expected.</param>
        public static void ShouldNotBe(this object o, object notExpected)
        {
            o.ShouldNotBe(notExpected, () => ThrowNotExpectedError(o));
        }

        /// <summary>Asserts that the expected value is not the same as the source object.</summary>
        /// <param name="o">The source object.</param>
        /// <param name="notExpected">The value that is not expected.</param>
        /// <param name="onFailed">Action to perform if the asertion is not satisfied.</param>
        public static void ShouldNotBe(this object o, object notExpected, Action onFailed)
        {
            if (o == null && notExpected == null && onFailed != null) onFailed();
            if (o != null && o.Equals(notExpected) && onFailed != null) onFailed();
        }

        /// <summary>Asserts that the given object is of a particular type.</summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="o">The object to examine.</param>
        public static void ShouldBeInstanceOfType<T>(this object o)
        {
            o.ShouldBeInstanceOfType<T>(() =>
                        {
                            throw new AssertionException(
                                            string.Format("Expected type was <{0}>, but actual type was <{1}>.",
                                                        typeof(T).FullName,
                                                        o.GetType().FullName));
                        });
        }

        /// <summary>Asserts that the given object is of a particular type.</summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="o">The object to examine.</param>
        /// <param name="onFailed">Action to perform if the asertion is not satisfied.</param>
        public static void ShouldBeInstanceOfType<T>(this object o, Action onFailed)
        {
            if (o == null) throw new ArgumentNullException("o", "The given object was null and cannot be compared.");
            if (o is T) return;
            if (onFailed != null) onFailed();
        }
        #endregion

        #region Methods - Collection Assertions
        /// <summary>Asserts that the specified item exists within the collection.</summary>
        /// <param name="enumerable">The source collection.</param>
        /// <param name="elements">The objects that should be contained within the collection.</param>
        public static void ShouldContain(this IEnumerable enumerable, params object[] elements)
        {
            foreach (var element in elements)
            {
                if (!enumerable.Contains(element)) throw new AssertionException("The expected item was not contained in the enumerable.");
            }
        }

        /// <summary>Asserts that the specified item does not exist within the collection.</summary>
        /// <param name="enumerable">The source collection.</param>
        /// <param name="elements">The objects that should not be contained within the collection.</param>
        public static void ShouldNotContain(this IEnumerable enumerable, params object[] elements)
        {
            foreach (var element in elements)
            {
                if (enumerable.Contains(element)) throw new AssertionException("The unexpected item was contained in the enumerable.");
            }
        }

        private static bool Contains(this IEnumerable enumerable, object obj)
        {
            if (enumerable == null) throw new AssertionException("Could not check the enumerable because it is null.");
            foreach (var o in enumerable)
            {
                if (o.Equals(obj)) return true;
            }
            return false;
        }

        /// <summary>Asserts that the collection is empty.</summary>
        /// <param name="enumerable">The source collection.</param>
        public static void ShouldBeEmpty(this IEnumerable enumerable)
        {
            foreach (var obj in enumerable)
            {
                throw new AssertionException("Expected to be empty, but was not.");
            }
        }

        /// <summary>Asserts that the collection is not empty.</summary>
        /// <param name="enumerable">The source collection.</param>
        public static void ShouldNotBeEmpty(this IEnumerable enumerable)
        {
            foreach (var obj in enumerable)
            {
                return;
            }
            throw new AssertionException("Expected collection to not be empty, but it was.");
        }
        #endregion

        #region Methods - Event Assertions
        /// <summary>Asserts that the 'PropertyChanged' event was fired at least once for each of the specified properties after some action is executed.</summary>
        /// <param name="self">The object firing the event.</param>
        /// <param name="action">The action that causes the event to fire.</param>
        /// <param name="propertyNames">The name of the property that should change.</param>
        public static void ShouldFirePropertyChanged(this INotifyPropertyChanged self, Action action, params string[] propertyNames)
        {
            var args = InvokePropertyChangedAction(self, action);
            foreach (var name in propertyNames)
            {
                if (args.Count(item => item.PropertyName == name) == 0)
                {
                    throw new AssertionException(string.Format("The PropertyChanged event for the property '{0}' did not fire.", name));
                }
            }
        }

        /// <summary>Asserts that the 'PropertyChanged' event was fired at least once for each of the specified properties after some action is executed.</summary>
        /// <param name="self">The object firing the event.</param>
        /// <param name="action">The action that causes the event to fire.</param>
        /// <param name="properties">
        ///    The collection of expressions that represent the properties 
        ///    that should have been fired (for example 'n => n.PropertyName'.)
        /// </param>
        public static void ShouldFirePropertyChanged<T>(this INotifyPropertyChanged self, Action action, params Expression<Func<T, object>>[] properties)
        {
            self.ShouldFirePropertyChanged(action, properties.ToList().ToArray());
        }

        /// <summary>Asserts that a 'PropertyChanged' event was not fired for the specified property after some action is executed.</summary>
        /// <param name="self">The object firing the event.</param>
        /// <param name="action">The action that causes the event to fire.</param>
        /// <param name="propertyNames">The name of the property that should not have changed.</param>
        public static void ShouldNotFirePropertyChanged(this INotifyPropertyChanged self, Action action, params string[] propertyNames)
        {
            var args = InvokePropertyChangedAction(self, action);
            foreach (var name in propertyNames)
            {
                if (args.Count(item => item.PropertyName == name) != 0)
                {
                    throw new AssertionException(string.Format("The PropertyChanged event for the property '{0}' fired when it was not expected to.", name));
                }
            }
        }

        /// <summary>Asserts that a 'PropertyChanged' event was not fired for the specified property after some action is executed.</summary>
        /// <param name="self">The object firing the event.</param>
        /// <param name="action">The action that causes the event to fire.</param>
        /// <param name="properties">
        ///    The collection of expressions that represent the properties 
        ///    that should have been fired (for example 'n => n.PropertyName').
        /// </param>
        public static void ShouldNotFirePropertyChanged<T>(this INotifyPropertyChanged self, Action action, params Expression<Func<T, object>>[] properties)
        {
            self.ShouldNotFirePropertyChanged(action, properties.ToList().ToArray());
        }

        /// <summary>Asserts that a 'PropertyChanged' event was fired for the specified property a certain number of times after some action is executed.</summary>
        /// <param name="self">The object firing the event.</param>
        /// <param name="propertyName">The name of the property that should change.</param>
        /// <param name="total">The total number of times the property was expected to fire.</param>
        /// <param name="action">The action that causes the event to fire.</param>
        public static void ShouldFirePropertyChanged(this INotifyPropertyChanged self, int total, Action action, string propertyName)
        {
            var fireTotal = PropertyChangedFireCount(self, action, propertyName);
            if (fireTotal != total)
            {
                throw new AssertionException(string.Format("The PropertyChanged event for the property '{0}' did not fire {1} times.", propertyName, total));
            }
        }

        /// <summary>Asserts that a 'PropertyChanged' event was fired for the specified property a certain number of times after some action is executed.</summary>
        /// <param name="self">The object firing the event.</param>
        /// <param name="propertyName">The name of the property that should change (for example 'n => n.PropertyName').</param>
        /// <param name="total">The total number of times the property was expected to fire.</param>
        /// <param name="action">The action that causes the event to fire.</param>
        public static void ShouldFirePropertyChanged<T>(this INotifyPropertyChanged self, int total, Action action, Expression<Func<T, object>> propertyName)
        {
            self.ShouldFirePropertyChanged(action, propertyName.GetPropertyName());
        }

        private static int PropertyChangedFireCount(this INotifyPropertyChanged self, Action action, string propertyName)
        {
            var args = InvokePropertyChangedAction(self, action);
            return args.Count(item => item.PropertyName == propertyName);
        }

        private static IEnumerable<PropertyChangedEventArgs> InvokePropertyChangedAction(this INotifyPropertyChanged self, Action action)
        {
            // Wire up event.
            var args = new List<PropertyChangedEventArgs>();
            PropertyChangedEventHandler handler = (s, e) => args.Add(e);
            self.PropertyChanged += handler;

            // Perform the action.
            action();
            self.PropertyChanged -= handler; // Unhook event.

            // Finish up.
            return args;
        }
        #endregion

        #region Methods - Property Name Constant Assertions
        /// <summary>
        ///    Examines the type looking for string constants that match the pattern "Prop[property-name]" and then asserts
        ///    that there is a corresponding property on the type that correctly matches the 'property-name'.
        /// </summary>
        /// <param name="type">The type to examine.</param>
        public static void ShouldHavePropertyNameConstants(this Type type)
        {
            // Setup initial conditions.
            if (type == null) throw new ArgumentNullException("type");

            // Determine if the type is valid.
            var error = new TestPropertyNameConstants(type).GetError();
            if (error != null) throw error;
        }

        /// <summary>
        ///    Examines all types in the given assembly looking for string constants that match the pattern "Prop[property-name]" and then asserts
        ///    that there is a corresponding property on the type that correctly matches the 'property-name'.
        /// </summary>
        /// <param name="assembly">The assembly to examine.</param>
        public static void ShouldHavePropertyNameConstants(this Assembly assembly)
        {
            // Setup initial conditions.
            if (assembly == null) throw new ArgumentNullException("assembly");

            // Compile the list of errors.
            var list = new List<AssertionException>();
            foreach (var type in assembly.GetTypes())
            {
                try
                {
                    type.ShouldHavePropertyNameConstants();
                }
                catch (Exception e)
                {
                    if (e is AssertionException) list.Add(e as AssertionException);
                }
            }
            if (list.Count == 0) return;

            // Report errors.
            string errorMessages = null;
            foreach (var exception in list)
            {
                errorMessages += exception.Message + Environment.NewLine + Environment.NewLine;
            }
            throw new AssertionException(
                string.Format("The following errors related to property-name constants occured:{0}{0}", Environment.NewLine)
                + errorMessages);
        }
        #endregion

        #region Methods - Assembly Assertions
                /// <summary>Attempts to instantiate all instances of the given type within an assembly (that has a parameterless constructor).</summary>
        /// <typeparam name="T">The base type of classes to include in the set.</typeparam>
        /// <param name="assembly">The assembly to look within.</param>
        public static void ShouldIntantiateAllTypes<T>(this Assembly assembly)
        {
            string errorMessage;
            if (!assembly.ShouldIntantiateAllTypes<T>(out errorMessage)) throw new AssertionException(errorMessage);
        }

        /// <summary>Attempts to instantiate all instances of the given type within an assembly (that has a parameterless constructor).</summary>
        /// <typeparam name="T">The base type of classes to include in the set.</typeparam>
        /// <param name="assembly">The assembly to look within.</param>
        /// <param name="errorMessage">The error message to return (null if no error).</param>
        /// <returns>True if the types were all instantiated successfully, otherwise False.</returns>
        public static bool ShouldIntantiateAllTypes<T>(this Assembly assembly, out string errorMessage)
        {
            // Setup initial conditions.
            if (assembly == null) throw new ArgumentNullException("assembly");
            var baseType = typeof (T);
            errorMessage = null;

            // Get the types.
            var types = from t in assembly.GetTypes()
                            where t.IsA(baseType) && t.IsPublic
                            select t;

            // Attempt to instantiate the types.
            var failedTypes = new Dictionary<Type, Exception>();
            foreach (var type in types)
            {
                // Get the parameterless constructor.
                var constructor = type.GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new Type[0], null);
                if (constructor == null) continue;

                // Attempt to instantiate.
                try
                {
                    Activator.CreateInstance(type);
                }
                catch (Exception e)
                {
                    failedTypes.Add(type, e);
                }
            }
            if (failedTypes.Count == 0) return true;

            // Throw the failed error.
            var msg = failedTypes.Aggregate("", (current, failedType)
                            => current + string.Format("\r- {0}",
                                    failedType.Key.FullName));
            errorMessage = string.Format("Failed to intantiate the following types:\r{0}", msg);

            // Finish up.
            return false;
        }
        #endregion

        #region Methods - Should Fire
        /// <summary>Asserts that when the specified action was invoked the event was fired at least once.</summary>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="self">The event-bus to monitor.</param>
        /// <param name="action"></param>
        public static void ShouldFire<TEvent>(this EventBus self, Action action)
        {
            if (GetFireCount<TEvent>(self, action) == 0) throw new AssertionException(string.Format("Expected the event '{0}' to be fired at least once.", typeof(TEvent).Name));
        }

        /// <summary>Asserts that when the specified action is invoked the event was fired a specific number of times.</summary>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="self">The event-bus to monitor.</param>
        /// <param name="total">The total number of times the event should have fired.</param>
        /// <param name="action"></param>
        public static void ShouldFire<TEvent>(this EventBus self, int total, Action action)
        {
            var fireCount = GetFireCount<TEvent>(self, action);
            if (fireCount != total) throw new AssertionException(string.Format("Expected the event '{0}' to be fired {1} times. Instead it fired {2} times.", typeof(TEvent).Name, total, fireCount));
        }

        /// <summary>Asserts that when the specified action was invoked the event was not fired.</summary>
        /// <param name="self">The event-bus to monitor.</param>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="action"></param>
        public static void ShouldNotFire<TEvent>(this EventBus self, Action action)
        {
            if (GetFireCount<TEvent>(self, action) != 0) throw new AssertionException(string.Format("Expected the event '{0}' to not fire.", typeof(TEvent).Name));
        }

        private static int GetFireCount<TEvent>(EventBus self, Action action)
        {
            // Setup initial conditions.
            if (self == null) throw new ArgumentNullException("self");
            if (action == null) throw new ArgumentNullException("action", "A test action was not passed to the method");
            var originalAsyncValue = self.IsAsynchronous;
            self.IsAsynchronous = false;

            // Wire up the event.
            var eventTester = new EventBusTester<TEvent>();
            self.Subscribe<TEvent>(eventTester.OnFire);

            // Invoke the action.
            action();

            // Reset state.
            self.IsAsynchronous = originalAsyncValue;
            self.Unsubscribe<TEvent>(eventTester.OnFire);

            // Finish up.
            return eventTester.FireCount;
        }

        public class EventBusTester<TEvent>
        {
            public int FireCount { get; private set; }
            public void OnFire(TEvent e)
            {
                FireCount++;
            }
        }
        #endregion

        #region Internal
        private static void ThrowNotExpectedError(object obj)
        {
            var objectText = obj == null ? "<null>" : string.Format("<{0}>", obj);
            var message = string.Format("Expected any value other than {0}.", objectText);
            throw new AssertionException(message);
        }

        private static void ThrowExpectedError(object obj, object expected)
        {
            var objectText = obj == null ? "<null>" : string.Format("<{0}>", obj);
            var expectedText = expected == null ? "<null>" : string.Format("<{0}>", expected);
            var message = string.Format("Expected {0} but was {1}.", expectedText, objectText);
            throw new AssertionException(message);
        }
        #endregion

        private class TestPropertyNameConstants
        {
            #region Head
            public TestPropertyNameConstants(Type type)
            {
                Type = type;
                PropConstants = GetPropConstants(type);
                Properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            }
            #endregion

            #region Properties
            private const string Prop = "Prop";
            private Type Type { get; set; }
            private IEnumerable<FieldInfo> PropConstants { get; set; }
            private IEnumerable<PropertyInfo> Properties { get; set; }
            #endregion

            #region Methods
            public AssertionException GetError()
            {
                // Ensure constant values match the name in the const.
                var mismatchError = GetMismatchingConstantNamesError();
                if (mismatchError != null) throw mismatchError;

                // Ensure there is a property for each declared property-name.
                var missingPropertyError = GetMissingPropertyNamesError();
                if (missingPropertyError != null) throw missingPropertyError;

                // Finish up.
                return null;
            }
            #endregion

            #region Internal
            private AssertionException GetMismatchingConstantNamesError()
            {
                var mismatches = GetMismatchingConstantNames();
                if (mismatches == null) return null;
                return new AssertionException(
                    string.Format("The following property-name constants on '{0}' do not have values that match their const names:{1}",
                                  Type.FullName,
                                  Environment.NewLine) + mismatches);
            }
            private string GetMismatchingConstantNames()
            {
                string list = null;
                foreach (var constant in PropConstants)
                {
                    var propertyName = constant.Name.RemoveStart(Prop);
                    var value = constant.GetValue(null) as string;

                    if (propertyName != value) list += string.Format("> {0} = '{1}'{2}", constant.Name, value, Environment.NewLine);
                }
                return list;
            }

            private AssertionException GetMissingPropertyNamesError()
            {
                var missingProperties = GetMissingPropertyNames();
                if (missingProperties == null) return null;
                return new AssertionException(
                    string.Format("The following properties declared in constants do not exist on '{0}':{1}",
                                  Type.FullName,
                                  Environment.NewLine) + missingProperties);
            }

            private string GetMissingPropertyNames()
            {
                string list = null;
                foreach (var constant in PropConstants)
                {
                    var propertyName = constant.Name.RemoveStart(Prop);
                    if (Properties.Count(p => p.Name == propertyName) == 0)
                    {
                        list += string.Format("> {0} ({1}){2}", propertyName, constant.Name, Environment.NewLine);
                    }
                }
                return list;
            }

            private static IEnumerable<FieldInfo> GetPropConstants(Type type)
            {
                return from p in type.GetFields(BindingFlags.Public | BindingFlags.Static)
                       where p.Name.StartsWith(Prop) && p.FieldType == typeof(string)
                       select p;
            }
            #endregion
        }
    }
}
