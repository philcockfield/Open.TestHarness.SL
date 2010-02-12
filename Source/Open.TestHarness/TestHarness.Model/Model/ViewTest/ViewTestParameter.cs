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
using System.Reflection;
using Open.Core.Common;

namespace Open.TestHarness.Model
{
    /// <summary>Represents a single parameter.</summary>
    public class ViewTestParameter : ModelBase
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="info">The reflection definiton of the parameter.</param>
        public ViewTestParameter(ParameterInfo info)
        {
            if (info == null) throw new ArgumentNullException("info");
            Info = info;
        }
        #endregion

        #region Properties
        /// <summary>Gets reflection definiton of the parameter.</summary>
        public ParameterInfo Info { get; private set; }

        /// <summary>Gets the Type of the parameter.</summary>
        public Type Type { get { return Info.ParameterType; } }

        /// <summary>Gets or sets the value of the parameter.</summary>
        public object Value
        {
            get { return GetPropertyValue<ViewTestParameter, object>(m => m.Value); }
            set { SetPropertyValue<ViewTestParameter, object>(m => m.Value, value); }
        }
        #endregion
    }
}
