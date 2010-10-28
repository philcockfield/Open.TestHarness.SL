using System;

namespace Open.Core.Controls.Buttons
{
    /// <summary>A simple button which displays either an icon, or text, or both.</summary>
    internal class IconTextButtonView : ButtonView
    {
        #region Head
        public const string RootClass = "iconTextButton";
        private const string TemplateBg = "#btnIconText_bg";

        public IconTextButtonView(IconTextButton model) : base(model)
        {
            // Setup initial conditions.
            Css.InsertLink(Css.Urls.CoreButtons);
            Container.AddClass(RootClass);

            // Ensure the required templates are downloaded.
            AddRequiredTemplate(ButtonTemplates.CommonBg, ButtonTemplates.TemplateUrl(ButtonTemplate.Common));
            AddRequiredTemplate(TemplateBg, ButtonTemplates.TemplateUrl(ButtonTemplate.IconText));
            DownloadTemplates(delegate
                                  {
                                      //TEMP 
                                      Log.Warning("Completed Download");

                                      ButtonStyles.SilverBackground(0, this, true);

                                      // Finish up.
                                      UpdateLayout();
                                  });


            //TEMP 
            SetSize(80, 24);
            //Background = Color.HotPink;
            //Css.RoundedCorners(Container, 5);

            // Finish up.
        }
        #endregion
    }
}
