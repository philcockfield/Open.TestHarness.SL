using System;
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
        public void Download()
        {
            TestHarness.Reset();
            DownloadInternal(InsertContainer());
        }

        public void DownloadTwice()
        {
            TestHarness.Reset();
            DownloadInternal(InsertContainer());
            DownloadInternal(InsertContainer());
        }

        private void DownloadInternal(jQueryObject container)
        {
            Log.Info("Downloading part...");
            partDefinition.Download(container, delegate(Part value)
                                        {
                                            part = value as SamplePart;
                                            if (partDefinition.HasError) { Log.Error("Download callback (failed)"); } else { Log.Success("Download callback (succeeded) - " + part.InstanceId); }
                                        });
        }

        public void Clear__ScriptUrls() { partDefinition.ScriptUrls = null; }
        public void Clear__CssUrls() { partDefinition.CssUrls = null; }
        public void Clear__EntryPoint() { partDefinition.EntryPoint = null; }
        public void Failure_ScriptUrls() { partDefinition.ScriptUrls = "NotAFile.js"; }

        public void Write__Urls()
        {
            WriteUrls("ScriptUrls: ", partDefinition.ScriptUrls);
            WriteUrls("CssUrls: ", partDefinition.CssUrls);
        }

        public void Write__Properties()
        {
            Log.WriteProperties(partDefinition);
            Log.LineBreak();
            if (part !=null) Log.WriteProperties(part);
        }
        #endregion

        #region Internal
        private static void WriteUrls(string title, string urls)
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
