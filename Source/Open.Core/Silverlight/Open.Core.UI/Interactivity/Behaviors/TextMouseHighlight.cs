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
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Highlights a text-block on mouse-over.</summary>
    public class TextMouseHighlight : Behavior<TextBlock>
    {
        #region Head
        private Cursor defaultCursor;

        public TextMouseHighlight()
        {
            // Set default values.
            Underline = true;
            HandCursor = true;
        }
        #endregion

        #region Event Handlers
        void Handle_MouseEnter(object sender, MouseEventArgs e)
        {
            SetHighlight();
        }

        void Handle_MouseLeave(object sender, MouseEventArgs e)
        {
            ClearHighlight();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the textblock is underlined on mouseover.</summary>
        public bool Underline { get; set; }

        /// <summary>Gets or sets the cursor to use when mouse over.</summary>
        public bool HandCursor { get; set; }
        #endregion

        #region Methods - Override
        protected override void OnAttached()
        {
            base.OnAttached();
            defaultCursor = AssociatedObject.Cursor;

            AssociatedObject.MouseEnter += Handle_MouseEnter;
            AssociatedObject.MouseLeave += Handle_MouseLeave;

            ClearHighlight();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.MouseEnter -= Handle_MouseEnter;
            AssociatedObject.MouseLeave -= Handle_MouseLeave;
            ClearHighlight();
        }
        #endregion

        #region Internal
        private void SetHighlight()
        {
            if (Underline) AssociatedObject.TextDecorations = TextDecorations.Underline;
            if (HandCursor) AssociatedObject.Cursor = Cursors.Hand;
        }

        private void ClearHighlight()
        {
            if (Underline) AssociatedObject.TextDecorations = null;
            AssociatedObject.Cursor = defaultCursor;
        }
        #endregion
    }
}
