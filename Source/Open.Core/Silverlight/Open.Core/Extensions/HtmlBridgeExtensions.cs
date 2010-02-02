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

using System.Windows.Browser;
using System;

namespace Open.Core.Common
{
    public static class HtmlBridgeExtensions
    {
        /// <summary>Sets the absolute position using styles on the given element.</summary>
        /// <param name="element">The element to apply the styles to.</param>
        /// <param name="left">The pixel-left position.</param>
        /// <param name="top">The pixel-top position.</param>
        public static void SetAbsolutePosition(this HtmlElement element, double left, double top)
        {
            if (element == null) return;
            element.SetStyleAttribute("position", "absolute");
            element.SetStyleAttribute("left", (int)left + "px");
            element.SetStyleAttribute("top", (int)top + "px");
        }

        /// <summary>Sets the size using styles on the given element.</summary>
        /// <param name="element">The element to apply the styles to.</param>
        /// <param name="width">The pixel-width.</param>
        /// <param name="height">The pixel-height.</param>
        public static void SetSize(this HtmlElement element, double width, double height)
        {
            if (element == null) return;
            element.SetStyleAttribute("width", (int)width + "px");
            element.SetStyleAttribute("height", (int)height + "px");
        }

        /// <summary>Sets the size using styles on the given element.</summary>
        /// <param name="element">The element to apply the styles to.</param>
        /// <param name="html">The html content.</param>
        public static void SetInnerHtml(this HtmlElement element, string html)
        {
            if (element == null) return;
            if (html == null) html = "";
            element.SetProperty("innerHTML", html);
        }

        /// <summary>Navigates the browser window to the given address.</summary>
        /// <param name="address">The URL to navigate to.</param>
        public static void NavigateTo(this Uri address)
        {
            address.NavigateTo(null);
        }

        /// <summary>Navigates the browser window to the given address.</summary>
        /// <param name="address">The URL to navigate to.</param>
        /// <param name="target">The name of the window or tab that navigateToUri should be opened in.</param>
        public static void NavigateTo(this Uri address, string target)
        {
            // Setup initial conditions.
            if (address == null) return;
            target = target.AsNullWhenEmpty();

            // Generate the script to execute.
            if (target == null)
            {
                // Use a JavaScript eval, as the SL Navigate method does not work in Chrome.
                // NB: This will probably no longer be required when SL version 4 is released and
                //       Chrome is officially supported.  This was as of SL3.
                var script = string.Format("window.location.href = '{0}'", address);
                HtmlPage.Window.Eval(script);
            }
            else
            {
                HtmlPage.Window.Navigate(address, target);
            }
        }

        /// <summary>Opens the address in a new browser window.</summary>
        /// <param name="address">The URL to navigate to.</param>
        public static void NewWindow(this Uri address)
        {
            address.NavigateTo("_blank");
        }
    }
}
