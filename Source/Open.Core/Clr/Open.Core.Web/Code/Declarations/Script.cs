using System;
using Open.Core.Common;

namespace Open.Core.Web
{
    public enum ScriptFile
    {
        JQuery,
        JQueryUi,
        JQueryCookie,
        MsCoreLib,
        Core,
        CoreControls,
        CoreLists,
        LibraryJit,
    }

    /// <summary>Constants and helpers for working with script.</summary>
    public class Script
    {
        #region Head
        public const string TextJavascript = "text/javascript";
        #endregion

        #region Properties
        /// <summary>Gets the embed tag for the specified script.</summary>
        /// <param name="scriptFile">Flag indicating what script to retrieve the path for.</param>
        public string this[ScriptFile scriptFile]
        {
            get { return ToScriptLink(GetPath(scriptFile)); }
        }
        #endregion

        #region Methods
        /// <summary>Creates the TypeKit embed tag.</summary>
        /// <param name="id">The ID for the typekit instance.</param>
        public string TypeKit(string id)
        {
            return string.Format(
                "<!-- TypeKit -->\r\n{0}\r\n{1}",
                            ToScriptLink(string.Format("http://use.typekit.com/{0}.js", id)),
                            WithinBlock("try { Typekit.load(); } catch (e) { }"));
        }
        #endregion

        #region Methods : Static
        /// <summary>Formats the given URL into a SCRIPT tag.</summary>
        /// <param name="url">The path to the script file.</param>
        /// <param name="type">The script language type.</param>
        public static string ToScriptLink(string url, string type = TextJavascript)
        {
            return string.Format("<script src='{0}' type='{1}'></script>", url, type);
        }

        /// <summary>Inserts the given script witin a SCRIPT tag.</summary>
        /// <param name="script">The script to embed</param>
        /// <param name="type">The script language type.</param>
        public static string WithinBlock(string script, string type = TextJavascript)
        {
            return string.Format("<script type='{0}'>\r\n{1}\r\n</script>", type, script);
        }

        /// <summary>Creates an entry point script for a Script# application.</summary>
        /// <param name="initMethod">The entry point initialization method (eg. 'MyNamespace.Application.main').</param>
        public static string EntryPoint(string initMethod)
        {
            initMethod = initMethod.RemoveEnd("();");
            string script = string.Format("$(document).ready(function () {{ {0}(); }});", initMethod);
            return WithinBlock(script);
        }
        #endregion

        #region Internal
        private static string GetPath(ScriptFile scriptFile)
        {
            string path;
            switch (scriptFile)
            {
                //case ScriptFile.JQuery: path = "http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"; break;
                //case ScriptFile.JQueryUi: path = "http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js"; break;

                case ScriptFile.JQuery: path = "/Open.Core/Scripts/JQuery/jquery-1.4.2.min.js"; break;
                case ScriptFile.JQueryUi: path = "/Open.Core/Scripts/JQuery/jquery-ui-1.8.4.custom.min.js"; break;

                case ScriptFile.JQueryCookie: path = "/Open.Core/Scripts/JQuery/jquery.cookie.js"; break;

                case ScriptFile.MsCoreLib: path = "/Open.Core/Scripts/mscorlib.js"; break;

                case ScriptFile.Core: path = "/Open.Core/Scripts/Open.Core.debug.js"; break;
                case ScriptFile.CoreControls: path = "/Open.Core/Scripts/Open.Core.Controls.debug.js"; break;
                case ScriptFile.CoreLists: path = "/Open.Core/Scripts/Open.Core.Lists.debug.js"; break;

                case ScriptFile.LibraryJit: path = "/Open.Core/Scripts/Open.Library.Jit.debug.js"; break;

                default: throw new NotSupportedException(scriptFile.ToString());
            }
            return path.PrependDomain();
        }
        #endregion
    }
}
