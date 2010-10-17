using System;
using jQueryApi;
using Open.Core.Controls.HtmlPrimitive;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class PartTest
    {
        #region Head
        private jQueryObject container;
        private PartDefinition partDefinition;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            container = TestHarness.AddElement();
            Css.SetSize(container, 400, 300);
            container.CSS(Css.Background, Color.Red(0.1));
            TestHarness.UpdateLayout();

            // Create the part-definition.
            partDefinition = new PartDefinition(
                "Open.Core.Test.Samples.Create();",
                "/Content/Scripts/Open.Core.Controls.debug.js;/Content/Scripts/Open.Core.debug.js",
                "/Content/Css/Sample.css; http://localhost:8022/Open.Core/Css/Core.css"
                );

            // Finish up.
            Write_Properties();
        }
        #endregion

        #region Methods
        
        public void Write_Properties()
        {
            Log.Title("PartDefinition");
            Log.Info("- EntryPoint: " + partDefinition.EntryPoint);
            WriteUrls("- ScriptUrls: ", partDefinition.ScriptUrls.Split(";"));
            WriteUrls("- CssUrls: ", partDefinition.CssUrls.Split(";"));
        }
        #endregion

        #region Internal
        private static void WriteUrls(string title, string[] items )
        {
            Log.Info(title);
            HtmlList list = new HtmlList();

            //TEMP 
            //list.Add(Css.Classes.)
            //foreach (string item in items)
            //{
            //    list.Add(Html.ToHyperlink(item));
            //}
            Log.Info(list.OuterHtml);
        }
        #endregion
    }
}
