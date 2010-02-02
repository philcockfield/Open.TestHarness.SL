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

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Updates an element's opacity based upon it's enabled state.</summary>
    public class EnabledOpacity : Behavior<Control>
    {
        #region Head
        public EnabledOpacity()
        {
            // Set default values.
            Enabled = 1;
            Disabled = 0.35;
        }
        #endregion

        #region Event Handlers
        private void HandleIsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateOpacity();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the opacity of the control when it is enabled.</summary>
        public double Enabled { get; set; }

        /// <summary>Gets or sets the opacity of the control when it is disabled.</summary>
        public double Disabled { get; set; }
        #endregion

        #region Methods
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.IsEnabledChanged += HandleIsEnabledChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.IsEnabledChanged -= HandleIsEnabledChanged;
        }

        /// <summary>Updates the opacity of the Control based on it's current IsEnabled state.</summary>
        /// <remarks>See the 'Enabled' and 'Disabled' properties which determine the opacity values.</remarks>
        public void UpdateOpacity()
        {
            if (AssociatedObject == null) return;
            AssociatedObject.Opacity = AssociatedObject.IsEnabled ? Enabled : Disabled;
        }
        #endregion
    }
}
