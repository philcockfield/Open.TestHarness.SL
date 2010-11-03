using jQueryApi;
using Open.Core.Controls.HtmlPrimitive;
using Open.Core.Test.Samples;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class PartTest
    {
        #region Head
        private PartDefinition partDefinition;
        private SamplePart part;

        public void ClassInitialize()
        {
            // Create the part-definition.
            partDefinition = new PartDefinition(
                        "Open.Core.Test.Samples.SamplePart.Create();",
                        "/Content/Scripts/Open.Core.Test.debug.js;  /Content/Scripts/Open.Core.Controls.debug.js;/Content/Scripts/Open.Core.debug.js;",
                        "/Content/Css/Sample.css;    /Open.Core/Css/Core.css"
                        );
            partDefinition.DownloadTimeout = 2;

            // Finish up.
            Write__Properties();
        }

        private static jQueryObject InsertContainer()
        {
            jQueryObject container = TestHarness.AddElement();
            Css.SetSize(container, 400, 100);
            container.CSS(Css.Background, Color.Black(0.1));
            TestHarness.UpdateLayout();
            return container;
        }
        #endregion

        #region Methods
        public void Load()
        {
            TestHarness.Reset();
            LoadPart(InsertContainer());
        }

        public void Load_Twice()
        {
            TestHarness.Reset();
            LoadPart(InsertContainer());
            LoadPart(InsertContainer());
        }

        private void LoadPart(jQueryObject container)
        {
            Log.Info("Downloading part...");
            partDefinition.Initialize(container, delegate(Part value)
                                        {
                                            part = value as SamplePart;
                                            if (partDefinition.HasError) { Log.Error("Download callback (failed)"); } else { Log.Success("Download callback (succeeded) - " + part.InstanceId); }
                                        });
        }

        public void Clear__ScriptUrls() { partDefinition.ScriptUrls = null; }
        public void Clear__CssUrls() { partDefinition.ResourceUrls = null; }
        public void Clear__EntryPoint() { partDefinition.EntryPoint = null; }
        public void Failure_ScriptUrls() { partDefinition.ScriptUrls = "NotAFile.js"; }

        public void Write__Urls()
        {
            WriteUrls("ScriptUrls: ", partDefinition.ScriptUrls);
            WriteUrls("CssUrls: ", partDefinition.ResourceUrls);
        }

        public void Write__Properties()
        {
            Log.WriteProperties(partDefinition);
            Log.LineBreak();
            if (part !=null) Log.WriteProperties(part);
        }
        #endregion

        #region Methods : Static
        public static void WriteUrls(string title, string urls)
        {
            if (!Helper.String.HasValue(urls)) return;
            IHtmlList list = Log.WriteList(title);
            foreach (string item in urls.Split(PartDefinition.PathDivider))
            {
                list.Add(Html.ToHyperlink(item));
            }
        }
        #endregion
    }
}
