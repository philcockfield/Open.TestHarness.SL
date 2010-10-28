using System;
using Open.Core.Web;

namespace Open.TestHarness.Web
{
    public enum TestHarnessCssFile
    {
        TestHarness,
        TestHarnessIe,
    }

    /// <summary>Gets URL paths to CSS files.</summary>
    public class TestHarnessCss
    {
        #region Properties
        /// <summary>Gets the embed tag for the specified script.</summary>
        /// <param name="cssFile">Flag indicating what script to retrieve the path for.</param>
        public string this[TestHarnessCssFile cssFile]
        {
            get
            {
                bool ieOnly = cssFile == TestHarnessCssFile.TestHarnessIe;
                return Css.ToCssLink(GetPath(cssFile), ieOnly);
            }
        }
        #endregion

        #region Internal
        private static string GetPath(TestHarnessCssFile cssFile)
        {
            string path;
            switch (cssFile)
            {
                case TestHarnessCssFile.TestHarness: path = "/Open.TestHarness.css"; break;
                case TestHarnessCssFile.TestHarnessIe: path= "/Open.TestHarness.IE.css"; break;

                default: throw new NotSupportedException(cssFile.ToString());
            }
            return (Css.CssFolderPath + path).PrependDomain();
        }
        #endregion
    }
}
