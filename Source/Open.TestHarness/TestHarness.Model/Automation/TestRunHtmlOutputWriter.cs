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
            var doc = HtmlPage.Document;
            var div = doc.GetElementById(HtmlOutputId) ?? CreateElement("div");
            div.SetAttribute("id", HtmlOutputId);
            doc.Body.AppendChild(div);

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

        private static HtmlElement CreateElement(string tag)
        {
            return HtmlPage.Document.CreateElement(tag);
        }
       #endregion
    }
}
