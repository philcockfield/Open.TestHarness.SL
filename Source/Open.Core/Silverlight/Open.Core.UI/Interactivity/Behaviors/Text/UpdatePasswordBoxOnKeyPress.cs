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

using System.Windows.Controls;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Updates a textbox's source databinding value on key-press.</summary>
    /// <remarks>
    ///    Without this behavior, focus has to be removed from the textbox before the data-binding is syncronized.<BR/>
    ///    When using the behavior you may want ot use the 'UpdateSourceTrigger=Explicit' flag in the binding, for example:<BR/>
    ///    <BR/>
    ///    Text="{Binding Mode=TwoWay, Path=PropertyName, UpdateSourceTrigger=Explicit}"
    /// </remarks>
    public class UpdatePasswordBoxOnKeyPress : UpdateCharacterFieldOnKeyPressBase<PasswordBox>
    {
        public UpdatePasswordBoxOnKeyPress() : base(PasswordBox.PasswordProperty)
        {
        }
    }
}
