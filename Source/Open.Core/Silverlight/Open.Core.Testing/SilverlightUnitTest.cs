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
using System.Windows;
using Microsoft.Silverlight.Testing;

namespace Open.Core.Common.Testing
{
    /// <summary>Base class for silverlight unit-tests.</summary>
    public abstract class SilverlightUnitTest : SilverlightTest
    {
        #region Methods
        /// <summary>Adds the given element to the 'TestPanel' and waits for the 'Loaded' event to fire.</summary>
        /// <typeparam name="T">The type of the element.</typeparam>
        /// <param name="element">The element to add.</param>
        /// <remarks>See the 'WaitFor' method for more details.</remarks>
        /// <example>
        ///    [TestMethod]
        ///    [Asynchronous]
        ///    public void ShouldHaveTextboxWithHelloWorld()
        ///    {
        ///        Page page = new Page();
        ///        AddAndWaitForLoaded(page);
        ///
        ///        EnqueueCallback(() => page.txtInput.Text.ShouldBe("Hello World!"));
        ///
        ///        EnqueueTestComplete();
        ///    }
        /// </example>
        protected void AddAndWaitForLoaded<T>(T element) where T : UIElement
        {
            WaitFor(element, "Loaded");
            TestPanel.Children.Add(element);
        }


        /// <summary>Waits for execution of the given event.</summary>
        /// <typeparam name="T">The type of UI element the event is on.</typeparam>
        /// <param name="objectToWaitForItsEvent">The UI element the event is on.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <remarks> Source: http://silverlight.net/blogs/justinangel/archive/2009/02.aspx  </remarks>
        /// <example>
        ///    [TestMethod]
        ///    [Asynchronous]
        ///    public void ShouldHaveTextboxWithHelloWorld()
        ///    {
        ///        Page page = new Page();
        ///
        ///        WaitFor(page, "Loaded");
        ///        TestPanel.Children.Add(page);
        ///
        ///        EnqueueCallback(() => page.txtInput.Text.ShouldBe("Hello World!"));
        ///
        ///        EnqueueTestComplete();
        ///    }
        /// </example>
        protected void WaitFor<T>(T objectToWaitForItsEvent, string eventName)
        {
            // Setup initial conditions.
            var eventInfo = objectToWaitForItsEvent.GetType().GetEvent(eventName);
            var eventRaised = false;

            // Add appropriate handlers.
            if (typeof(RoutedEventHandler).IsAssignableFrom(eventInfo.EventHandlerType))
            {
                eventInfo.AddEventHandler(objectToWaitForItsEvent, (RoutedEventHandler)delegate { eventRaised = true; });
            }
            else if (typeof(EventHandler).IsAssignableFrom(eventInfo.EventHandlerType))
            {
                eventInfo.AddEventHandler(objectToWaitForItsEvent, (EventHandler)delegate { eventRaised = true; });
            }

            // Asynchronously pause completion of the test until the event has been raised.
            EnqueueConditional(() => eventRaised);
        }
        #endregion
    }
}
