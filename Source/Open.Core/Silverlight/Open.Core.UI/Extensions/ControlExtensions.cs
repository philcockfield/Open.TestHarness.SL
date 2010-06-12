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
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;

namespace Open.Core.Common
{
    public static class ControlExtensions
    {
        /// <summary>Adds XAML content to a RichTextBox.</summary>
        /// <param name="richTextBox">The textbox to add to.</param>
        /// <param name="xaml">The block to add.</param>
        /// <remarks>This method effectively handles a Section block element if specified (adding all child Blocks to the textbox).</remarks>
        public static void AddXaml(this RichTextBox richTextBox, Block xaml)
        {
            if (richTextBox == null) throw new ArgumentNullException("richTextBox");
            if (xaml == null) throw new ArgumentNullException("xaml");
            if (xaml is Section)
            {
                AddBlocks(richTextBox, ((Section)xaml).Blocks);
            }
            else
            {
                richTextBox.Blocks.Add(xaml);
            }
        }

        private static void AddBlocks(RichTextBox richTextBox, BlockCollection blocks)
        {
            var list = blocks.ToList();
            foreach (var block in list)
            {
                blocks.Remove(block);
                richTextBox.Blocks.Add(block);
            }
        }

        /// <summary>Scrolls to the top of a rich texbox.</summary>
        /// <param name="richTextBox">The rich textbox to scroll.</param>
        public static void ScrollToTop(this RichTextBox richTextBox)
        {
            TextPointer pstart = richTextBox.ContentStart;
            richTextBox.Selection.Select(pstart, pstart);
        }

        /// <summary>Applys the flat-style to the scrollbars within the given scroll-viewer.</summary>
        /// <param name="scrollViewer">The scroll-viewer to effect.</param>
        /// <param name="size">The size of the scroll bars (automatically skinny).</param>
        /// <param name="opacity">The opacity of the scrollbar.</param>
        public static void ApplyFlatScrollBarStyle(this ScrollViewer scrollViewer, double size = 10, double opacity = 1)
        {
            if (scrollViewer ==null) throw new ArgumentNullException("scrollViewer");
            new ScrollBarStyleApplier(scrollViewer, size, opacity);
        }

        private class ScrollBarStyleApplier
        {
            private readonly ScrollViewer scrollViewer;
            private readonly double size;
            private readonly double opacity;

            public ScrollBarStyleApplier(ScrollViewer scrollViewer, double size, double opacity)
            {
                this.scrollViewer = scrollViewer;
                this.size = size;
                this.opacity = opacity.WithinBounds(0, 1);
                if (!ApplyStyles()) scrollViewer.Loaded += OnLoaded;
            }

            private void OnLoaded(object sender, RoutedEventArgs e)
            {
                scrollViewer.Loaded -= OnLoaded;
                ApplyStyles();
            }

            private bool ApplyStyles()
            {
                var scrollbars = scrollViewer.FindChildrenOfType<ScrollBar>();
                if (scrollbars.Count() == 0) return false;

                var style = StyleResources.ControlStyles["ScrollBar.Flat"] as Style;
                foreach (var scrollBar in scrollbars)
                {
                    scrollBar.Style = style;
                    scrollBar.Opacity = opacity;
                    if (scrollBar.Orientation == Orientation.Horizontal) scrollBar.Height = size;
                    if (scrollBar.Orientation == Orientation.Vertical) scrollBar.Width = size;
                }
                return true;
            }
        }
    }
}
