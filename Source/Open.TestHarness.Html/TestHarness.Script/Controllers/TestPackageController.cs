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
        public const string PropSelectedTestClass = "SelectedTestClass";
        private const double loadTimeout = 10; // secs.

        private readonly TestPackageListItem rootNode;


        /// <summary>Constructor.</summary>
        /// <param name="rootNode">The root list-item node.</param>
        public TestPackageController(TestPackageListItem rootNode)
        {
            // Store values.
            this.rootNode = rootNode;

            // Wire up events.
            rootNode.SelectionChanged += OnSelectionChanged;
            rootNode.ChildSelectionChanged += OnChildSelectionChanged;
        }

        protected override void OnDisposed()
        {
            rootNode.SelectionChanged -= OnSelectionChanged;
            rootNode.ChildSelectionChanged -= OnChildSelectionChanged;
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (RootNode.IsSelected) Download();
        }

        private void OnChildSelectionChanged(object sender, EventArgs e)
        {
            TestClassListItem item = Helper.Tree.FirstSelectedChild(RootNode) as TestClassListItem;
            SelectedTestClass = item == null ? null : item.TestClass;
        }
        #endregion

        #region Properties
        /// <summary>Gets the test-package that is under control.</summary>
        public TestPackageInfo TestPackage { get { return rootNode.TestPackage; } }

        /// <summary>Gets the root list-item node.</summary>
        public TestPackageListItem RootNode { get { return rootNode; } }

        /// <summary>Gets or sets the currently selected test class.</summary>
        public TestClassInfo SelectedTestClass
        {
            get { return (TestClassInfo) Get(PropSelectedTestClass, null); }
            set
            {
                if (Set(PropSelectedTestClass, value, null))
                {
                    Application.Shell.Sidebar.TestList.TestClass = value;
                }
            }
        }
        #endregion

        #region Internal
        private void Download()
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
            foreach (TestClassInfo testClass in TestPackage)
            {
                TestClassListItem node = new TestClassListItem(testClass);
                RootNode.AddChild(node);
            }
        }
        #endregion
    }
}