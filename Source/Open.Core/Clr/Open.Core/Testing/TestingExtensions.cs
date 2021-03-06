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
using System.ComponentModel.DataAnnotations;
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
        /// <param name="message">Optional message to use for the assertion.</param>
        public static void ShouldBe(this object o, object expected, string message = null)
        {
            o.ShouldBe(expected, () => ThrowExpectedError(o, expected, message));
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
        /// <param name="message">Optional message to use for the assertion.</param>
        public static void ShouldNotBe(this object o, object notExpected, string message = null)
        {
            o.ShouldNotBe(notExpected, () => ThrowNotExpectedError(o, message));
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
        /// <param name="message">Optional message to use for the assertion.</param>
        public static void ShouldBeInstanceOfType<T>(this object o, string message = null)
        {
            o.ShouldBeInstanceOfType<T>(() =>
                        {
                            if (message.IsNullOrEmpty(true))
                            {
                                message = string.Format("Expected type was <{0}>, but actual type was <{1}>.",
                                                        typeof (T).FullName,
                                                        o.GetType().FullName);
                            }
                            throw new AssertionException(message);
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

        /// <summary>Asserts that the given object is a GUID.</summary>
        /// <param name="o">The object to examine.</param>
        /// <remarks>Throws an error if the given object is an Empty GUID type.</remarks>
        /// <param name="message">Optional message to use for the assertion.</param>
        public static void ShouldBeGuid(this object o, string message = null)
        {
            Action throwError = () =>
                                    {
                                        if (message.IsNullOrEmpty(true))
                                        {
                                            message = "The given object is not a GUID and cannot be parsed as a GUID.";
                                        }
                                        throw new AssertionException(message);
                                    };
            if (o is Guid)
            {
                if (Equals(o, Guid.Empty)) throwError();
            }
            else if (o is string)
            {
                var guidString = o as string;
                try
                {
                    new Guid(guidString);
                }
                catch (Exception) { throwError(); }
            }
            else { throwError(); }
        }

        /// <summary>Asserts equality.</summary>
        /// <param name="self">The source value.</param>
        /// <param name="value">The value to compare with.</param>
        /// <param name="message">Optional message to use for the assertion.</param>
        public static void ShouldEqual(this object self, object value, string message = null)
        {
            if (!Equals(self, value)) throw new AssertionException(
                                                message.IsNullOrEmpty(true) 
                                                                    ? "The given values are not equal, and they were expected to be equal." 
                                                                    : message);
        }

        /// <summary>Asserts inequality.</summary>
        /// <param name="self">The source value.</param>
        /// <param name="value">The value to compare with.</param>
        /// <param name="message">Optional message to use for the assertion.</param>
        public static void ShouldNotEqual(this object self, object value, string message = null)
        {
            if (Equals(self, value)) throw new AssertionException(
                                            message.IsNullOrEmpty(true) 
                                                                ? "The given values are equal, and they were expected to not be equal."
                                                                : message);
        }
        #endregion

        #region Methods - DateTime
        /// <summary>Asserts that date-time is within the specified number of milli-seconds of the given date-time.</summary>
        /// <param name="self">The date.</param>
        /// <param name="msecs">The number of milli-seconds the date must be within.</param>
        /// <param name="dateTime">The date to compare</param>
        public static void ShouldBeWithin(this DateTime self, int msecs, DateTime dateTime)
        {
            // Setup initial conditions.
            if (msecs < 0) throw new ArgumentOutOfRangeException("msecs", "Must be greater than zero.");

            // Get bounds.
            var timeSpan = new TimeSpan(0, 0, 0, 0, (int) (msecs*0.5));
            var upper = dateTime.Ticks + timeSpan.Ticks;
            var lower = dateTime.Ticks - timeSpan.Ticks;

            // Exception thrower.
            Action throwError = () =>
                                    {
                                        throw new AssertionException(string.Format(
                                                    "The date value '{0}' was not within {1} milliseconds of '{2}'.", 
                                                    self, msecs, dateTime));
                                    };

            // Perform comparison.
            if (self.Ticks > upper) throwError();
            if (self.Ticks < lower) throwError();
        }
        #endregion

        #region Methods - Property Equality
        /// <summary>Asserts that all the properties on the object equal the corresponding properties on the given object.</summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="self">The source object.</param>
        /// <param name="value">The object to examine.</param>
        public static void ShouldHaveEqualProperties<T>(this T self, T value)
        {
            // Setup initial conditions.
            if (Equals(self, default(T))) throw new ArgumentNullException("self");
            if (Equals(value, default(T))) throw new ArgumentNullException("value");

            // Compare properties.
            foreach (var propertyInfo in self.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                AssertValuesAreEqual(propertyInfo, self, value);
            }
        }

        /// <summary>Asserts that all the specified properties on the object equal the corresponding properties on the given object.</summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="self">The source object.</param>
        /// <param name="value">The object to examine.</param>
        /// <param name="properties">
        ///     The collection of properties to match.<br/>
        ///     Each expression represents a property (for example 'n => n.PropertyName')
        /// </param>
        public static void ShouldHaveEqualProperties<T>(this T self, T value, params Expression<Func<T, object>>[] properties)
        {
            // Setup initial conditions.
            if (Equals(self, default(T))) throw new ArgumentNullException("self");
            if (Equals(value, default(T))) throw new ArgumentNullException("value");
            if (properties == null || properties.Count() == 0) return;
            var selfType = self.GetType();

            // Compare properties.
            foreach (var propertyExpression in properties)
            {
                var propertyInfo = selfType.GetProperty(propertyExpression.GetPropertyName());
                AssertValuesAreEqual(propertyInfo, self, value);
            }
        }

        private static void AssertValuesAreEqual<T>(PropertyInfo property, T obj1, T obj2)
        {
            var value1 = property.GetGetMethod(true).Invoke(obj1, null);
            var value2 = property.GetGetMethod(true).Invoke(obj2, null);
            if (!Equals(value1, value2))
            {
                throw new AssertionException(string.Format("The property '{0}' is not the same on both objects.", property.Name));
            }
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
        public static void ShouldInstantiateAllTypes<T>(this Assembly assembly)
        {
            string errorMessage;
            if (!assembly.ShouldInstantiateAllTypes<T>(out errorMessage)) throw new AssertionException(errorMessage);
        }

        /// <summary>Attempts to instantiate all instances of the given type within an assembly (that has a parameterless constructor).</summary>
        /// <typeparam name="T">The base type of classes to include in the set.</typeparam>
        /// <param name="assembly">The assembly to look within.</param>
        /// <param name="errorMessage">The error message to return (null if no error).</param>
        /// <returns>True if the types were all instantiated successfully, otherwise False.</returns>
        public static bool ShouldInstantiateAllTypes<T>(this Assembly assembly, out string errorMessage)
        {
            // Setup initial conditions.
            if (assembly == null) throw new ArgumentNullException("assembly");
            var baseType = typeof(T);
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
            errorMessage = string.Format("Failed to instantiate the following types:\r{0}", msg);

            // Finish up.
            return false;
        }
        #endregion

        #region Method - ShouldHaveValidValidationAttributes
        /// <summary>
        ///     Ensures that any property or field decorated with the [Validation] attribute
        ///     that references a language resource (via the 'ErrorMessageResourceType' 
        ///     and 'ErrorMessageResourceName' properties) points to an existing
        ///     language resource.
        /// </summary>
        /// <param name="assembly">The assembly containing the types to examine.</param>
        public static void ShouldHaveValidValidationAttributes(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            foreach (var type in assembly.GetTypes())
            {
                ShouldHaveValidValidationAttributes(type);
            }
        }

        /// <summary>
        ///     Ensures that any property or field decorated with the [Validation] attribute
        ///     that references a language resource (via the 'ErrorMessageResourceType' 
        ///     and 'ErrorMessageResourceName' properties) points to an existing
        ///     language resource.
        /// </summary>
        /// <param name="type">The type to examine.</param>
        public static void ShouldHaveValidValidationAttributes(this Type type)
        {
            if (type == null) throw new ArgumentNullException("type");

            foreach (var memberInfo in type.GetMembers())
            {
                memberInfo.ShouldHaveValidValidationAttribute();
            }
        }

        /// <summary>
        ///     Ensures that the [Validation] attribute
        ///     that references a language resource (via the 'ErrorMessageResourceType' 
        ///     and 'ErrorMessageResourceName' properties) points to an existing
        ///     language resource.
        /// </summary>
        /// <param name="member">The member to examine.</param>
        /// <param name="ignoreIfAttributeNotPresent">Flag indicating if no exception should be thrown if the member does not have a [Required] attribute.</param>
        public static void ShouldHaveValidValidationAttribute(this MemberInfo member, bool ignoreIfAttributeNotPresent = true)
        {
            // Setup initial conditions.
            if (member == null) throw new ArgumentNullException("member");
            var attribute = member.GetCustomAttributes(typeof(ValidationAttribute), true).FirstOrDefault() as ValidationAttribute;
            if (attribute == null)
            {
                if (ignoreIfAttributeNotPresent) return;
                throw new AssertionException(string.Format("{0} is not present.", GetValidationAttributeErrorMessage(member, attribute)));
            }

            // Extract key and type.
            var type = attribute.ErrorMessageResourceType;
            var key = attribute.ErrorMessageResourceName.AsNullWhenEmpty();
            if (type == null && key == null) return;

            // Check for existence of key and type.
            var errorMessage = GetValidationAttributeErrorMessage(member, attribute);
            if (type == null && key != null) throw new AssertionException(string.Format("{0} references an resource-key but not a resource-type.", errorMessage));
            if (type != null && key == null) throw new AssertionException(string.Format("{0} references an resource-type but not a resource-key.", errorMessage));

            // Check that the resource exists.
            var value = type.GetProperty(key, BindingFlags.Public | BindingFlags.Static);
            if (value == null) throw new AssertionException(string.Format("{0} does not have a corresponding entry in a resource (resx) file.", errorMessage));
        }

        private static string GetValidationAttributeErrorMessage(this MemberInfo member, Attribute attribute)
        {
            var attributeName = attribute == null ? typeof(ValidationAttribute).Name : attribute.GetType().Name;
            var msg = string.Format("The [{0}] on member '{1}' in class '{2}'",
                                    attributeName,
                                    member.Name,
                                    member.DeclaringType.Name);
            return msg;
        }
        #endregion

        #region Methods - Should Fire (Event Bus)
        /// <summary>Asserts that when the specified action was invoked the event was fired at least once.</summary>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="self">The event-bus to monitor.</param>
        /// <param name="triggerAction">The action that causes the event to fire.</param>
        /// <param name="onEvent">Option action which is invoked when the event fires (passing in the event args).</param>
        public static void ShouldFire<TEvent>(this IEventBus self, Action triggerAction, Action<TEvent> onEvent = null)
        {
            if (GetFireCount(self, triggerAction, onEvent) == 0) throw new AssertionException(string.Format("Expected the event '{0}' to be fired at least once.", typeof(TEvent).Name));
        }

        /// <summary>Asserts that when the specified action is invoked the event was fired a specific number of times.</summary>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="self">The event-bus to monitor.</param>
        /// <param name="total">The total number of times the event should have fired.</param>
        /// <param name="triggerAction">The action that causes the event to fire.</param>
        /// <param name="onEvent">Option action which is invoked when the event fires (passing in the event args).</param>
        public static void ShouldFire<TEvent>(this IEventBus self, int total, Action triggerAction, Action<TEvent> onEvent = null)
        {
            var fireCount = GetFireCount(self, triggerAction, onEvent);
            if (fireCount != total) throw new AssertionException(string.Format("Expected the event '{0}' to be fired {1} times. Instead it fired {2} times.", typeof(TEvent).Name, total, fireCount));
        }

        /// <summary>
        ///     Asserts that when the specified action was invoked the event was fired at least once (asynchronously).
        /// </summary>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="self">The event-bus to monitor.</param>
        /// <param name="triggerAction">The action that causes the event to fire.</param>
        /// <param name="onEvent">Option action which is invoked when the event fires (passing in the event args).</param>
        public static void ShouldFireAsync<TEvent>(this IEventBus self, Action triggerAction, Action<TEvent> onEvent = null)
        {
            GetFireCountAsync(self, triggerAction, onEvent, fireCount =>
                        {
                            if (fireCount == 0) throw new AssertionException(
                                                string.Format("Expected the event '{0}' to be fired at least once.", 
                                                typeof(TEvent).Name));
                        });
        }

        /// <summary>Asserts that when the specified action was invoked the event was not fired.</summary>
        /// <param name="self">The event-bus to monitor.</param>
        /// <typeparam name="TEvent">The event type.</typeparam>
        /// <param name="triggerAction">The action that causes the event to fire.</param>
        /// <param name="onEvent">Option action which is invoked when the event fires (passing in the event args).</param>
        public static void ShouldNotFire<TEvent>(this IEventBus self, Action triggerAction, Action<TEvent> onEvent = null)
        {
            if (GetFireCount(self, triggerAction, onEvent) != 0) throw new AssertionException(string.Format("Expected the event '{0}' to not fire.", typeof(TEvent).Name));
        }

        private static int GetFireCount<TEvent>(IEventBus self, Action action, Action<TEvent> onEvent)
        {
            // Set the event-bus to work synchonously.
            var originalAsyncValue = self.IsAsynchronous;
            self.IsAsynchronous = false;

            // Invoke test.
            int returnValue = 0;
            GetFireCountAsync(self, action, onEvent, i => returnValue = i);

            // Finish up (reset state).
            self.IsAsynchronous = originalAsyncValue;
            return returnValue;
        }

        private static void GetFireCountAsync<TEvent>(IEventBus self, Action action, Action<TEvent> onEvent, Action<int> fireTotal)
        {
            // Setup initial conditions.
            if (self == null) throw new ArgumentNullException("self");
            if (action == null) throw new ArgumentNullException("action", "A test action was not passed to the method");

            // Internal callback.
            EventBusTester<TEvent> eventTester = null;
            Action unsubscribe = () => { if (eventTester != null) self.Unsubscribe<TEvent>(eventTester.OnFire);};
            Action<TEvent> onEventInternal = e =>
                                         {
                                             // Finish up.
                                             if (onEvent != null) onEvent(e);
                                             if (self.IsAsynchronous) unsubscribe(); // Is Async: Can only test for at least one event firing.
                                             fireTotal(eventTester.FireCount);
                                         };

            // Setup up the event.
            eventTester = new EventBusTester<TEvent> { OnEvent = onEventInternal };
            self.Subscribe<TEvent>(eventTester.OnFire);

            // Invoke the action.
            action();

            // Finish up.
            if (!self.IsAsynchronous) unsubscribe(); // Not async : Everything should be complete by now.
        }

        public class EventBusTester<TEvent>
        {
            public Action<TEvent> OnEvent { get; set; }
            public int FireCount { get; private set; }
            public void OnFire(TEvent e)
            {
                FireCount++;
                if (OnEvent != null) OnEvent(e);
            }
        }
        #endregion

        #region Methods - Serialization
        /// <summary>Asserts that all public (non-abstract) classes deriving from the given type within the assembly can be serialized.</summary>
        /// <typeparam name="TBase">The base type.</typeparam>
        /// <param name="assembly">The assembly to look within.</param>
        /// <returns>The types that were checked.</returns>
        public static IEnumerable<Type> ShouldSerializeAllTypesOf<TBase>(this Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            var types = assembly.GetTypes()
                .Where(m => !m.IsAbstract && m.IsA<TBase>() && (m.IsNestedPublic || m.IsPublic));
            types.ShouldSerialize();
            return types;
        }

        /// <summary>Asserts that all the given types can be serialized.</summary>
        /// <param name="types">The collection of types to examine.</param>
        public static void ShouldSerialize(this IEnumerable<Type> types)
        {
            // Setup initial conditions.
            if (types == null) throw new ArgumentNullException("types");
            var errorList = new List<string>();

            // Attempt to serialize each type.
            const string bullet = "- ";
            foreach (var type in types)
            {
                try
                {
                    type.ShouldSerialize();
                }
                catch (Exception error)
                {
                    errorList.Add(string.Format("{0}{1}{2}", bullet, error.Message, Environment.NewLine));
                }
            }

            // Raise assertion error if required.
            if (errorList.IsEmpty()) return;
            if (errorList.Count == 1)
            {
                var message = errorList.First().RemoveStart(bullet).RemoveEnd(Environment.NewLine);
                throw new AssertionException(message);
            }
            else
            {
                var message = string.Format("The following types could not be serialized:\r\n{0}", errorList.ToLines());
                throw new AssertionException(message);
            }
        }

        /// <summary>Asserts that an instance of the given object can be serialized.</summary>
        /// <param name="type">The type to serialize ().</param>
        public static void ShouldSerialize(this Type type)
        {
            // Create the instance to serialize.
            object instance = null;
            try
            {
                instance = Activator.CreateInstance(type);
            }
            catch (Exception error)
            {
                var message = string.Format(
                    "The type '{0}' could not be instantiated.  Ensure it is public and has a parameterless constructor.{1}",
                    type.FullName,
                    Environment.NewLine);
                throw new AssertionException(message, error);
            }

            // Assert that it can be serialized.
            instance.ShouldSerialize();
        }

        /// <summary>Asserts that the given object can be serialized.</summary>
        /// <param name="self">The object instance to serialize.</param>
        public static void ShouldSerialize(this object self)
        {
            if (self == null) throw new ArgumentNullException("self");
            try
            {
                self.ToSerializedXml().Length.ShouldNotBe(0);
            }
            catch (Exception error)
            {
                throw new AssertionException(string.Format("The type '{0}' could not be serialized.", self.GetType().FullName), error);
            }
        }
        #endregion

        #region Internal
        private static void ThrowNotExpectedError(object obj, string message = null)
        {
            if (message.AsNullWhenEmpty() == null)
            {
                var objectText = obj == null ? "<null>" : string.Format("<{0}>", obj);
                message = string.Format("Expected any value other than {0}.", objectText);
            }
            throw new AssertionException(message);
        }

        private static void ThrowExpectedError(object obj, object expected, string message = null)
        {
            if (message.AsNullWhenEmpty() == null)
            {
                var objectText = obj == null ? "<null>" : string.Format("<{0}>", obj);
                var expectedText = expected == null ? "<null>" : string.Format("<{0}>", expected);
                message = string.Format("Expected {0} but was {1}.", expectedText, objectText);
            }
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
