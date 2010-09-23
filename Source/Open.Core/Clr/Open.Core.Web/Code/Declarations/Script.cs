﻿using System;
using Open.Core.Common;

namespace Open.Core.Web
{
    public enum ScriptFile
    {
        JQuery,
        JQueryUi,
        JQueryCookie,
        JQueryTemplate,
        JQueryTemplatePlus,
        JQueryJson,
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
        private const string DefaultOpenCorePath = "/Open.Core/Scripts/";

        /// <summary>Constructor.</summary>
        public Script()
        {
            OpenCorePath = DefaultOpenCorePath;
        }
        #endregion

        #region Properties
        /// <summary>Gets the embed tag for the specified script.</summary>
        /// <param name="scriptFile">Flag indicating what script to retrieve the path for.</param>
        public string this[ScriptFile scriptFile]
        {
            get { return ToScriptLink(GetPath(scriptFile)); }
        }

        /// <summary>Gets or sets the base path used for script files.</summary>
        public string OpenCorePath { get; set; }
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
        private string GetPath(ScriptFile scriptFile)
        {
            string path;
            switch (scriptFile)
            {
                case ScriptFile.JQuery: path = "JQuery/jquery-1.4.2.min.js"; break;
                case ScriptFile.JQueryUi: path = "JQuery/jquery-ui-1.8.4.custom.min.js"; break;
                case ScriptFile.JQueryCookie: path = "JQuery/jquery.cookie.js"; break;
                case ScriptFile.JQueryTemplate: path = "JQuery/jquery.tmpl.js"; break;
                case ScriptFile.JQueryTemplatePlus: path = "JQuery/jquery.tmplPlus.js"; break;
                case ScriptFile.JQueryJson: path = "JQuery/jquery.json-2.2.min.js"; break;

                case ScriptFile.MsCoreLib: path = "mscorlib.js"; break;

                case ScriptFile.Core: path = "Open.Core.debug.js"; break;
                case ScriptFile.CoreControls: path = "Open.Core.Controls.debug.js"; break;
                case ScriptFile.CoreLists: path = "Open.Core.Lists.debug.js"; break;

                case ScriptFile.LibraryJit: path = "Open.Library.Jit.debug.js"; break;

                default: throw new NotSupportedException(scriptFile.ToString());
            }

            return string.Format(
                                "{0}/{1}",
                                GetBasePath(scriptFile).RemoveEnd("/"), 
                                path).PrependDomain();
        }

        private string GetBasePath(ScriptFile scriptFile)
        {
            if (scriptFile.IsJQuery()) return DefaultOpenCorePath;
            return OpenCorePath.IsNullOrEmpty(true) 
                                            ? DefaultOpenCorePath 
                                            : OpenCorePath;
        }
        #endregion
    }
}
