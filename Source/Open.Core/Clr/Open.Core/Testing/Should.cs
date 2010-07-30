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

namespace Open.Core.Common.Testing
{
    /// <summary>Static assertion class.</summary>
    public static partial class Should
    {
        #region Methods - Throw (Error) Assertions
        /// <summary>Asserts that the specified type of exception was thrown when the given action is invoked.</summary>
        /// <typeparam name="T">The type of exception that was excpected.</typeparam>
        /// <param name="action">The action to invoke that should cause the exception to be thrown.</param>
        public static void Throw<T>(Action action)
        {
            // Setup initial conditions.
            if (action == null) throw new ArgumentNullException("action", "No action was specified");
            var errorType = typeof(T);

            // Ensure the error type is an exception
            if (!(errorType.IsA(typeof(Exception))))
                throw new ArgumentOutOfRangeException("T",
                            string.Format("The given type '{0}' is not an exception.", errorType));

            // Invoke the Action and ensure the exception was thrown.
            try
            {
                action.Invoke();
            }
            catch (Exception e)
            {
                var thrownType = e.GetType();
                if (thrownType == errorType || thrownType.IsA(errorType)) return; // Success.
                throw new AssertionException(
                                            string.Format("Expected exception of type '{0}' but was '{1}'.",
                                                errorType.Name,
                                                thrownType.Name));
            }

            // Exception was not thrown.
            throw new AssertionException(string.Format("Expected exception of type '{0}'.", errorType.Name));
        }
        #endregion
    }
}
