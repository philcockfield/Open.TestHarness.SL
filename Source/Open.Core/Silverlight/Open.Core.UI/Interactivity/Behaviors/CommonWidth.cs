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

using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Ensures a set of elements conform to the width of the widest item.</summary>
    public class CommonWidth : Behavior<FrameworkElement>
    {
        #region Head
        private readonly List<FrameworkElement> elements = new List<FrameworkElement>();
        #endregion

        #region Properties
        /// <summary>Gets or sets a space delimited set of elemnt names that define the set that is under control.</summary>
        public string ElementNames { get; set; }
        #endregion

        #region Event Handlers
        void Handle_Element_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateWidths();
        }
        #endregion

        #region Methods - Override
        protected override void OnAttached()
        {
            // Setup initial conditions.
            base.OnAttached();
            ElementNames = ElementNames.AsNullWhenEmpty();
            if (ElementNames == null) return;

            // Retrieve the elements.
            AssociatedObject.Loaded += delegate
                                           {
                                               RetrieveElements();
                                               UpdateWidths();
                                           };
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            foreach (var element in elements)
            {
                WireEvents(element, false);
            }
        }
        #endregion

        #region Internal
        private void RetrieveElements()
        {
            foreach (var name in ElementNames.Split(" ".ToCharArray()))
            {
                var element = AssociatedObject.FindName(name) as FrameworkElement;
                if (element != null)
                {
                    elements.Add(element);
                    WireEvents(element, true);
                }
            }
        }

        private void UpdateWidths()
        {
            var maxWidth = elements.Max(item => item.ActualWidth);
            foreach (var element in elements)
            {
                element.MinWidth = maxWidth;
            }
        }

        private void WireEvents(FrameworkElement element, bool add )
        {
            if (add) element.SizeChanged += Handle_Element_SizeChanged;
            else element.SizeChanged -= Handle_Element_SizeChanged;
        }
        #endregion
    }
}
