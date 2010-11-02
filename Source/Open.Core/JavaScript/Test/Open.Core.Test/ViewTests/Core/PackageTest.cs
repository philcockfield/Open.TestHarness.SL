using Open.Testing;

namespace Open.Core.Test.ViewTests.Core
{
    public class PackageTest
    {
        #region Head
        private Package partDefinition;

        public void ClassInitialize()
        {
            // Create the part-definition.
            partDefinition = new Package(
                        "Open.Core.Test.Samples.SamplePart.PackageInitialize();",
                        "/Content/Scripts/Open.Core.Test.debug.js;  /Content/Scripts/Open.Core.Controls.debug.js;/Content/Scripts/Open.Core.debug.js;",
                        "/Content/Css/Sample.css;    /Open.Core/Css/Core.css"
                        );
            partDefinition.DownloadTimeout = 2;

            // Finish up.
            Write__Properties();
        }
        #endregion

        #region Methods
        public void Load()
        {
            TestHarness.Reset();
            LoadPackage();
        }

        public void Load_Twice()
        {
            TestHarness.Reset();
            LoadPackage();
            LoadPackage();
        }

        private void LoadPackage()
        {
            Log.Info("Downloading package...");
            partDefinition.Load(delegate
                                        {
                                            if (partDefinition.HasError) { Log.Error("Download callback (failed)"); } else { Log.Success("Download callback (succeeded)"); }
                                        });
        }

        public void Clear__ScriptUrls() { partDefinition.ScriptUrls = null; }
        public void Clear__CssUrls() { partDefinition.ResourceUrls = null; }
        public void Clear__EntryPoint() { partDefinition.EntryPoint = null; }
        public void Failure_ScriptUrls() { partDefinition.ScriptUrls = "NotAFile.js"; }

        public void Write__Urls()
        {
            PartTest.WriteUrls("ScriptUrls: ", partDefinition.ScriptUrls);
            PartTest.WriteUrls("CssUrls: ", partDefinition.ResourceUrls);
        }

        public void Write__Properties()
        {
            Log.WriteProperties(partDefinition);
        }
        #endregion
    }
}
