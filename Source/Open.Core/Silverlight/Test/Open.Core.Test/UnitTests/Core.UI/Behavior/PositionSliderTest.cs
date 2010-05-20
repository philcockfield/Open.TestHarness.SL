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

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;
using Open.Core.Common.Testing;
using Open.Core.UI.Controls;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.AttachedBehavior
{
    [TestClass]
    public class PositionSliderTest : SilverlightTest
    {
        #region Head
        private Placeholder element;
        private PositionSlider behavior;

        [TestInitialize]
        public void Setup()
        {
            element = new Placeholder();
            behavior = new PositionSlider{Duration = 0.1};

            var canvas = new Canvas{Width = 500, Height = 500};
            canvas.Children.Add(element);

            Behaviors.SetPositionSlider(element, behavior);
        }
        #endregion

        #region Tests
        [TestMethod]
        public void ShouldBeWithinCanvas()
        {
            behavior.IsWithinCanvas.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldChangePositionWhenSlidingIsDisabled()
        {
            behavior.IsSlideEnabled = false;
            behavior.Position.ShouldBe(new Point(0, 0));

            behavior.Position= new Point(100, 300);

            behavior.X.ShouldBe(100d);
            behavior.Y.ShouldBe(300d);

            behavior.Canvas.GetChildPosition(element).ShouldBe(new Point(100, 300));
        }

        [TestMethod][Asynchronous]
        public void ShouldAnimatePositionChange()
        {
            behavior.Position.ShouldBe(new Point(0, 0));

            behavior.SlideComplete += delegate
                                          {
                                              behavior.IsAnimating.ShouldBe(false);
                                              behavior.X.ShouldBe(100d);
                                              behavior.Y.ShouldBe(300d);
                                              behavior.Position.ShouldBe(new Point(100, 300));
                                              behavior.Canvas.GetChildPosition(element).ShouldBe(new Point(100, 300));

                                              EnqueueTestComplete();
                                          };

            behavior.Position = new Point(100, 300);
            behavior.Canvas.GetChildPosition(element).ShouldBe(new Point(0, 0));
            behavior.IsAnimating.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldUpdatePositionValue()
        {
            PropertyChangedEventArgs args = null;
            behavior.PropertyChanged += (sender, e) => args = e;

            behavior.Position.ShouldBe(new Point(0, 0));
            behavior.Canvas.SetPosition(element, 500, 500);
            behavior.Position.ShouldBe(new Point(0, 0));

            behavior.UpdatePositionValue();
            behavior.Position.ShouldBe(new Point(500, 500));
            args.ShouldBe(null);
        }
        #endregion
    }
}
