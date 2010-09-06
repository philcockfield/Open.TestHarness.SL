using System;
using Open.Core;
using Open.Core.Lists;
using Open.Testing.Models;
using Open.Testing.Views;

namespace Open.Testing.Controllers
{
    /// <summary>Controller for a single test package.</summary>
    public class PackageController : ControllerBase
    {
        #region Events
        /// <summary>Fires when the package has laoded.</summary>
        public event EventHandler Loaded;
        private void FireLoaded(){if (Loaded != null) Loaded(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropSelectedClass = "SelectedClass";
        private const double loadTimeout = 10; // secs.

        private readonly PackageListItem rootNode;
        private readonly SidebarView sidebarView;
        private ClassController selectedClassController;

        /// <summary>Constructor.</summary>
        /// <param name="rootNode">The root list-item node.</param>
        /// <param name="sidebarView">The Sidebar control.</param>
        public PackageController(PackageListItem rootNode, SidebarView sidebarView)
        {
            // Store values.
            this.rootNode = rootNode;
            this.sidebarView = sidebarView;

            // Wire up events.
            rootNode.SelectionChanged += OnSelectionChanged;
            rootNode.ChildSelectionChanged += OnChildSelectionChanged;
            RootList.SelectedParentChanged += OnRootListSelectedParentChanged;
        }

        protected override void OnDisposed()
        {
            // Unwire event.
            rootNode.SelectionChanged -= OnSelectionChanged;
            rootNode.ChildSelectionChanged -= OnChildSelectionChanged;
            RootList.SelectedParentChanged -= OnRootListSelectedParentChanged;

            // Dispose of child resources.
            if (selectedClassController != null) selectedClassController.Dispose();

            // Finish up.
            base.OnDisposed();
        }
        #endregion

        #region Event Handlers
        private void OnRootListSelectedParentChanged(object sender, EventArgs e)
        {
            UpdateMethodListVisibility();
        }

        private void OnSelectionChanged(object sender, EventArgs e)
        {
            if (RootNode.IsSelected) Download();
       }

        private void OnChildSelectionChanged(object sender, EventArgs e)
        {
            ClassListItem item = Helper.Tree.FirstSelectedChild(RootNode) as ClassListItem;
            SelectedClass = item == null ? null : item.ClassInfo;
            UpdateMethodListVisibility();
        }
        #endregion

        #region Properties
        /// <summary>Gets whether this package is currently selected within the tree..</summary>
        public bool IsSelected { get { return sidebarView.RootList.SelectedParent == RootNode; } }

        /// <summary>Gets the test-package that is under control.</summary>
        public PackageInfo TestPackage { get { return rootNode.TestPackage; } }

        /// <summary>Gets the root list-item node.</summary>
        public PackageListItem RootNode { get { return rootNode; } }

        /// <summary>Gets or sets the currently selected test class.</summary>
        public ClassInfo SelectedClass
        {
            get { return (ClassInfo) Get(PropSelectedClass, null); }
            set
            {
                if (Set(PropSelectedClass, value, null))
                {
                    if (selectedClassController != null) selectedClassController.Dispose();
                    if (value != null) selectedClassController = new ClassController(value, sidebarView);
                    sidebarView.MethodList.ClassInfo = value;
                }
            }
        }

        private ListTreeView RootList { get { return sidebarView.RootList; } }
        #endregion

        #region Internal
        private void Download()
        {
            // Setup initial conditions.
            if (TestPackage.IsLoaded) return;
            PackageLoader loader = TestPackage.Loader;
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
            foreach (ClassInfo testClass in TestPackage)
            {
                ClassListItem node = new ClassListItem(testClass);
                RootNode.AddChild(node);
            }
        }

        private void UpdateMethodListVisibility()
        {
            // Show or hide the MethodList based on the kind of root-tree-node that is currently selected.
            PackageListItem node = RootList.SelectedParent as PackageListItem;
            sidebarView.IsMethodListVisible = (node != null) && Helper.Tree.HasSelectedChild(node);
        }
        #endregion
    }
}