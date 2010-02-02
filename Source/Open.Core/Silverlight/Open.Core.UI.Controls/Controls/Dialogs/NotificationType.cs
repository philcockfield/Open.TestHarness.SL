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
using System.Windows.Media;
using Open.Core.Common;

namespace Open.Core.UI.Controls.Dialogs
{
    /// <summary>Flags representing various type of notification.</summary>
    public enum NotificationType
    {
        Ok,
        Error,
        Warning,
        Question
    }

    internal static partial class EnumExtensions
    {
        public static ImageSource ToIcon(this NotificationType type)
        {
            switch (type)
            {
                case NotificationType.Ok: return "/Images/Icon.Shield.GreenTick.59x66.png".ToImageSource();
                case NotificationType.Error: return "/Images/Icon.Shield.Error.59x66.png".ToImageSource();
                case NotificationType.Warning: return "/Images/Icon.Shield.Warning.59x66.png".ToImageSource();
                case NotificationType.Question: return "/Images/Icon.Shield.Question.59x66.png".ToImageSource();

                default: throw new NotSupportedException(type.ToString());
            }
        }
    }
}
