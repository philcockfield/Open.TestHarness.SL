using Open.Core.Web;

namespace Open.TestHarness.Web
{
    /// <summary>Gets URL paths to CSS files.</summary>
    public class TestHarnessScript
    {
        #region Properties
        /// <summary>Gets the path to the TestHarness script.</summary>
        public string Path { get { return WebConstants.Script.Path(ScriptFile.TestHarness); } }

        /// <summary>Gets the TestHarness script path within a SCRIPT tag.</summary>
        public string ScriptLink{get { return Script.ToScriptLink(Path); }}

        /// <summary>Gets the Application and init script for the TestHarness.</summary>
        public string Application
        {
            get
            {
                return string.Format(
                    "{0}\r\n{1}", 
                    ScriptLink,
                    Script.EntryPoint("Open.Testing.Application.main"));
            }
        }
        #endregion
    }
}
