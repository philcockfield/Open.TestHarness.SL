using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Common Template selectors for buttons.</summary>
    public static class ButtonTemplates
    {
        #region Contants
        public const string CommonBg = "#btnCommon_Bg";
        #endregion

        #region Methods
        /// <summary>Retrieves the URL for the specified button HTML template(s).</summary>
        /// <param name="template">Flag indicating what template(s) to load.</param>
        public static string TemplateUrl(ButtonTemplate template)
        {
            return string.Format("/Open.Core/Buttons/Template?type={0}", template.ToString());
        }
        #endregion
    }
}
