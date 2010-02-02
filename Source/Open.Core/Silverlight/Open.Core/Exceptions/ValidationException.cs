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
    /// <summary>Error thrown during databinding validation.</summary>
    /// <remarks>
    ///    Throw this within a property setting when the incoming databound value is invalid.
    ///    The binding hooks to invoke the UI validation system are (example with TextBox):
    ///    <P/>
    ///        Text="{Binding Text, Mode=TwoWay, NotifyOnValidationError=True, ValidatesOnExceptions=True}"
    ///    <P/>
    ///    To avoid having validation exception caught by the Visual Studio debugger, add this exception type to the
    ///    exclusion list.<BR/>
    ///    1. Menu: Debug -> Exceptions... (Ctrl + Alt + E) -> Add...<BR/>
    ///    2. Add 'Open.Core.Common.ValidationException' to the list.
    ///    3. Uncheck 'Thrown' and 'User Handled'.
    /// </remarks>
    public class ValidationException : ArgumentException
    {
        public ValidationException() { }
        public ValidationException(string message) : base(message) { }
        public ValidationException(string message, Exception innerException) : base(message, innerException) { }
    }
}
