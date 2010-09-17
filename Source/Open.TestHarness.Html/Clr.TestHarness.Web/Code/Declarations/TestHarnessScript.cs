using System;
using Open.Core.Web;

namespace Open.TestHarness.Web
{
    public enum TestHarnessScriptFile
    {
        TestHarness,
        Bootstrap,
    }

    /// <summary>Gets URL paths to CSS files.</summary>
    public class TestHarnessScript
    {
        #region Properties
        /// <summary>Gets the embed tag for the specified script.</summary>
        /// <param name="scriptFile">Flag indicating what script to retrieve the path for.</param>
        public string this[TestHarnessScriptFile scriptFile]
        {
            get
            {
                return Script.ToScriptLink(GetPath(scriptFile));
            }
        }

        /// <summary>Gets the Application and init script for the TestHarness.</summary>
        public string Application
        {
            get
            {
                return string.Format(
                    "{0}\r\n{1}", 
                    this[TestHarnessScriptFile.TestHarness],
                    Script.EntryPoint("Open.Testing.Application.main"));
            }
        }

        /// <summary>Gets the Bootstrapper script for the TestHarness.</summary>
        public string Bootstrap
        {
            get
            {
                return string.Format(
                    "{0}\r\n{1}",
                    this[TestHarnessScriptFile.Bootstrap],
                    Script.EntryPoint("Open.Testing.Bootstrap.Application.main"));
            }
        }

        #endregion

        #region Internal
        private static string GetPath(TestHarnessScriptFile cssFile)
        {
            string path;
            switch (cssFile)
            {
                case TestHarnessScriptFile.TestHarness: path = "/Content/Scripts/Open.TestHarness.debug.js"; break;
                case TestHarnessScriptFile.Bootstrap: path = "/Content/Scripts/Open.TestHarness.Bootstrap.debug.js"; break;
                default: throw new NotSupportedException(cssFile.ToString());
            }
            return path.PrependDomain();
        }
        #endregion
    }
}
