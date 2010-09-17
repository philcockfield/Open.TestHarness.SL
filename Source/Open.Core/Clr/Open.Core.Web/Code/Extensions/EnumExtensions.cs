using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Open.Core.Web
{
    public static class EnumExtensions
    {
        /// <summary>Determines whether the given script-flag represents a JQuery script file.</summary>
        /// <param name="scriptFile">Flag to examine.</param>
        public static bool IsJQuery(this ScriptFile scriptFile)
        {
            switch (scriptFile)
            {
                case ScriptFile.JQuery:
                case ScriptFile.JQueryUi:
                case ScriptFile.JQueryCookie:
                    return true;
            }
            return false;
        }
    }
}
