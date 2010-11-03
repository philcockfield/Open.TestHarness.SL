using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A simple button which displays either an icon, or text, or both.</summary>
    internal class IconTextButtonView : ButtonView
    {
        #region Head
        public const string RootClass = "iconTextButton";
        private const string TemplateContent = "#btnIconText_content";

        public IconTextButtonView(IconTextButton model) : base(model)
        {
            // Setup initial conditions.
            Css.InsertLink(Css.Urls.CoreButtons);
            Container.AddClass(RootClass);

            // Ensure the required templates are downloaded.
            AddRequiredTemplate(ButtonTemplates.CommonBg, ButtonTemplates.TemplateUrl(ButtonTemplate.Common));
            AddRequiredTemplate(TemplateContent, ButtonTemplates.TemplateUrl(ButtonTemplate.IconText));
            DownloadTemplates(delegate
                                  {
                                      // Initialize the button content.
                                      ButtonStyles.SilverBackground(0, this, true);
                                      ButtonStyles.Rounded(0, this);
                                      TemplateForStates(1, AllStates, TemplateContent, EnabledCondition.Either, FocusCondition.Either);

                                      // Finish up.
                                      UpdateLayout();
                                  });
        }
        #endregion
    }
}
