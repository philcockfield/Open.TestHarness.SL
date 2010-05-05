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

namespace Open.Core.Common
{
    /// <summary>Flags that define how a control under test is sized.</summary>
    public enum TestControlSize
    {
        Manual,
        Fill
    }


    /// <summary>
    ///    Declares a method as being test that exercises a control within the TestHarness.
    ///    Test methods must be void and take parameters of type UIElement.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ViewTestAttribute : Attribute
    {
        #region Head
        public ViewTestAttribute()
        {
            IsVisible = true;
            AllowAutoRun = true;
        }
        #endregion

        #region Properties
        /// <summary>The alternative name to give the method.</summary>
        /// <remarks>When this value is null the name of the method is used ("_" characters are replaced with spaces).</remarks>
        public string DisplayName { get; set; }

        /// <summary>Flag indicating if the method should automatically be run when the control is loaded.</summary>
        /// <remarks>If multiple methods are marked as Default, the first one within the list is chosen.</remarks>
        public bool Default { get; set; }

        /// <summary>Flag indicating whether the method should be listed in the TestHarness.</summary>
        /// <remarks>This option is useful when setting an "Default" method, which is not useful to show.</remarks>
        public bool IsVisible { get; set; }

        /// <summary>Gets or sets a space-delimited category tag.</summary>
        public string Tag { get; set; }

        /// <summary>Gets or sets whether the test can be automatically run from the TestRunner.</summary>
        public bool AllowAutoRun { get; set; }

        /// <summary>Gets or sets how the control under test is sized within the TestHarness.</summary>
        public TestControlSize SizeMode { get; set; }
        #endregion
    }
}
