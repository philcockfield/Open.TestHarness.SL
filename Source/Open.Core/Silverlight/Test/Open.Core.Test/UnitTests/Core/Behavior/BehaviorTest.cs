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

using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.AttachedBehavior;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Common.AttachedBehavior
{
    [TestClass]
    public class BehaviorTest : SilverlightTest
    {
        #region Tests
        [TestMethod]
        public void ShouldAttach()
        {
            var border = new Border();
            var behavior = new SampleBehavior();

            SampleIndex.SetSampleBehavior(border, behavior);
            behavior.OnAttachInvoked.ShouldBe(true);
            behavior.AssociatedObject.ShouldBe(border);
        }

        [TestMethod]
        public void ShouldDetach()
        {
            var border = new Border();
            var behavior = new SampleBehavior();
            SampleIndex.SetSampleBehavior(border, behavior);

            SampleIndex.SetSampleBehavior(border, null);
            behavior.OnDetachInvoked.ShouldBe(true);
            behavior.AssociatedObject.ShouldBe(null);
        }
        #endregion

        #region Sample Data
        private static class SampleIndex
        {
            public static readonly DependencyProperty SampleBehaviorProperty =
                DependencyProperty.RegisterAttached(
                    "SampleBehavior",
                    typeof(SampleBehavior),
                    typeof(SampleIndex),
                    new PropertyMetadata(Behaviors.HandleAttachedBehaviorPropertyChanged));
            public static SampleBehavior GetSampleBehavior(DependencyObject element) { return (SampleBehavior)element.GetValue(SampleBehaviorProperty); }
            public static void SetSampleBehavior(DependencyObject element, SampleBehavior value) { element.SetValue(SampleBehaviorProperty, value); }
        }

        private class SampleBehavior : Behavior<Border>
        {
            #region Head
            public bool OnAttachInvoked { get; private set; }
            public bool OnDetachInvoked { get; private set; }
            #endregion

            #region Properties
            public new Border AssociatedObject { get { return base.AssociatedObject; } }
            #endregion

            #region Methods
            protected override void OnAttached()
            {
                base.OnAttached();
                OnAttachInvoked = true;
            }

            protected override void OnDetaching()
            {
                base.OnDetaching();
                OnDetachInvoked = true;
            }
            #endregion
        }
        #endregion
    }
}
