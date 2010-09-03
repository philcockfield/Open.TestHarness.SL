using System;
using Open.Core;
using Open.TestHarness.Models;

namespace Open.TestHarness.Controllers
{
    /// <summary>Controller for a single test package.</summary>
    public class TestPackageController : ControllerBase
    {
        #region Events
        /// <summary>Fires when the package has laoded.</summary>
        public event EventHandler Loaded;
        private void FireLoaded(){if (Loaded != null) Loaded(this, new EventArgs());}
        #endregion

        #region Head
        private const double loadTimeout = 5; // secs.
        private readonly TestPackageListItem rootNode;

        /// <summary>Constructor.</summary>
        /// <param name="rootNode">The root list-item node.</param>
        public TestPackageController(TestPackageListItem rootNode)
        {
            // Store values.
            this.rootNode = rootNode;

            // Wire up events.
            rootNode.SelectionChanged += OnSelectionChanged;
        }
        #endregion

        #region Event Handlers
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (RootNode.IsSelected) Load();
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package that is under control.</summary>
        public TestPackageDef TestPackage { get { return rootNode.TestPackage; } }

        /// <summary>Gets the root list-item node.</summary>
        public TestPackageListItem RootNode { get { return rootNode; } }
        #endregion

        #region Internal
        private void Load()
        {
            // Setup initial conditions.
            if (TestPackage.IsLoaded) return;
            TestPackageLoader loader = TestPackage.Loader;
            string link = Html.ToHyperlink(loader.ScriptUrl, null, LinkTarget.Blank);

            // Create time-out handler.
            DelayedAction timeout = new DelayedAction(loadTimeout, delegate
                            {
                                Log.Error(string.Format("Failed to download the test-package at '{0}'.  Please ensure the file exists.", link));
                                Log.LineBreak();
                            });

            // Start downloading the package.
            Log.Info(string.Format("Downloading test-package: {0} ...", link));
            loader.Load(delegate
                                        {
                                            timeout.Stop();
                                            if (loader.Succeeded)
                                            {
                                                Log.Success("Test-package loaded successfully.");
                                                AddChildNodes();
                                                FireLoaded();
                                            }
                                            Log.LineBreak();
                                        });
            timeout.Start();
        }

        private void AddChildNodes()
        {
            foreach (TestClassDef testClass in TestPackage)
            {
                TestClassListItem node = new TestClassListItem(testClass);
                RootNode.AddChild(node);
            }
        }
        #endregion
    }
}