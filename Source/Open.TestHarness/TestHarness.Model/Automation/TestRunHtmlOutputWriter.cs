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
using System.Reflection;
using System.Windows.Browser;
using Open.Core.Common;

namespace Open.TestHarness.Automation
{
    /// <summary>Write test run output to the HTML DOM.</summary>
    public class TestRunHtmlOutputWriter
    {
        #region Head
        public const string HtmlOutputId = "TestHarness.Output";
        private readonly string tagName;
        private readonly TimeSpan elapsedTime;

        public TestRunHtmlOutputWriter(string tagName, TimeSpan elapsedTime, IEnumerable<MethodInfo> passed, IEnumerable<MethodInfo> failed)
        {
            this.tagName = tagName;
            this.elapsedTime = elapsedTime;
            ElapsedTime = elapsedTime;
            Passed = passed;
            Failed = failed;
        }

        #endregion

        #region Properties

        public TimeSpan ElapsedTime { get; private set; }
        public IEnumerable<MethodInfo> Passed { get; private set; }
        public IEnumerable<MethodInfo> Failed { get; private set; }
        #endregion

        #region Methods
        public void Write()
        {
            // Create the DIV element.
            var div = CreateRootDiv();

            // Insert root XML element.
            var xml = CreateElement(tagName);
            xml.SetAttribute("passed", Passed.Count().ToString());
            xml.SetAttribute("failed", Failed.Count().ToString());
            xml.SetAttribute("duration", elapsedTime.ToString());
            div.AppendChild(xml);

            // Insert method report.
            InsertMethods(xml, "passed", Passed);
            InsertMethods(xml, "failed", Failed);
        }
        #endregion

        #region Internal
        private static void InsertMethods(HtmlElement parent, string name, IEnumerable<MethodInfo> methods)
        {
            // Setup initial conditions.
            if (methods.IsEmpty()) return;

            // Create the method container.
            var methodContainer = CreateElement(name);
            parent.AppendChild(methodContainer);

            // Insert each method.
            foreach (var methodInfo in methods)
            {
                var htmMethod = CreateElement("method");
                htmMethod.SetAttribute("name", methodInfo.Name);
                htmMethod.SetAttribute("class", methodInfo.DeclaringType.FullName);
                methodContainer.AppendChild(htmMethod);
            }
        }

        private static HtmlElement CreateRootDiv()
        {
            var doc = HtmlPage.Document;

            var div = doc.GetElementById(HtmlOutputId);
            if (div != null) return div;
                
            div =  CreateElement("div");
            div.SetAttribute("id", HtmlOutputId);
            div.SetStyleAttribute("display", "none");
            
            doc.Body.AppendChild(div);
            return div;
        }


        private static HtmlElement CreateElement(string tag)
        {
            return HtmlPage.Document.CreateElement(tag);
        }
       #endregion
    }
}
