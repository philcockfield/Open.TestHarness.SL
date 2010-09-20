namespace Open.Core.Web
{
    public static class EnumExtensions
    {
        /// <summary>Determines whether the given script-flag represents a JQuery script file.</summary>
        /// <param name="scriptFile">Flag to examine.</param>
        public static bool IsJQuery(this ScriptFile scriptFile)
        {
            return scriptFile.ToString().StartsWith("JQuery");
        }
    }
}
