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

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using System.Reflection;
using Open.Core.Common.Testing;

namespace Core.Common.Test.Utility
{
    [TestClass]
    public class ReflectionUtilTest
    {
        #region Assembly
        [TestMethod]
        public void ShouldExtractName()
        {
            const string fullName = "Open.TestHarness.Test, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
            const string name = "Open.TestHarness.Test";

            ReflectionUtil.GetAssemblyName(null as string).ShouldBe(null);
            ReflectionUtil.GetAssemblyName("  ").ShouldBe(null);
            ReflectionUtil.GetAssemblyName(fullName).ShouldBe(name);
            ReflectionUtil.GetAssemblyName(name).ShouldBe(name);
        }

        [TestMethod]
        public void ShouldExtractNameFromAssembly()
        {
            var assembly = GetType().Assembly;
            const string name = "Open.Core.Common.Test.Clr";

            (null as Assembly).GetAssemblyName().ShouldBe(null);
            assembly.GetAssemblyName().ShouldBe(name);
        }
        #endregion
    }
}
