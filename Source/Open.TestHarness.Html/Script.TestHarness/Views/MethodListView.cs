using System;
using jQueryApi;
using Open.Core;
using Open.Core.Controls.Buttons;
using Open.Core.Lists;
using Open.Testing.Models;

namespace Open.Testing.Views
{
    /// <summary>The list of tests.</summary>
    public class MethodListView : TestHarnessViewBase
    {
        #region Events
        /// <summary>Fires when the 'Run' button is clicked.</summary>
        public event EventHandler RunClick;
        private void FireRunClick() { if (RunClick != null) RunClick(this, new EventArgs()); }

        /// <summary>Fires when the 'Refresh' button is clicked.</summary>
        public event EventHandler RefreshClick;
        private void FireRefreshClick(){if (RefreshClick != null) RefreshClick(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropClassInfo = "ClassInfo";
        public const string PropSelectedMethod = "SelectedMethod";
        private const int ButtonHeight = 33;

        private readonly ListTreeView listView;
        private readonly ListItem rootNode;
        private readonly TestHarnessEvents events;

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing div.</param>
        public MethodListView(jQueryObject container) : base(container)
        {
            // Setup initial conditions.
            events = Common.Events;

            // Create the list-tree.
            listView = new ListTreeView(jQuery.Select(CssSelectors.MethodListContent));
            listView.Slide.Duration = SidebarView.SlideDuration;

            // Construct the data-model root.
            rootNode = new ListItem();
            listView.RootNode = rootNode;

            // Construct buttons.
            InsertButtons();
        }
       #endregion

        #region Event Handlers
        private void OnItemClick(object sender, EventArgs e)
        {
            SelectedMethod = ((MethodListItem)sender).Method;
            events.FireMethodClicked(SelectedMethod);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the test class the view is listing methods for.</summary>
        public ClassInfo ClassInfo
        {
            get { return (ClassInfo) Get(PropClassInfo, null); }
            set
            {
                if (Set(PropClassInfo, value, null))
                {
                    PopulateList(value);
                }
            }
        }

        /// <summary>Gets or sets the currently selected method..</summary>
        public MethodInfo SelectedMethod
        {
            get { return (MethodInfo) Get(PropSelectedMethod, null); }
            set { Set(PropSelectedMethod, value, null); } 
        }

        /// <summary>Gets the offset height of the items within the list and the title bar.</summary>
        public int OffsetHeight { get { return listView.ContentHeight + DivTitleBar.GetHeight() + 1; } }

        private jQueryObject DivTitleBar { get { return Container.Children(CssSelectors.MethodListTitlebar); } }
        #endregion

        #region Methods
        /// <summary>Updates the visual state of the control.</summary>
        public void UpdateLayout()
        {
            listView.UpdateLayout();
        }
        #endregion

        #region Internal
        private void PopulateList(ClassInfo classInfo)
        {
            ClearChildren();
            if (classInfo == null) return;
            foreach (MethodInfo method in classInfo)
            {
                rootNode.AddChild(CreateListItem(method));
            }
        }

        private MethodListItem CreateListItem(MethodInfo methodInfo)
        {
            MethodListItem item = new MethodListItem(methodInfo);
            item.Click += OnItemClick;
            return item;
        }

        private void ClearChildren()
        {
            foreach (MethodListItem child in rootNode.Children)
            {
                child.Click -= OnItemClick;
            }
            rootNode.ClearChildren();
            SelectedMethod = null;
        }
        #endregion

        #region Internal : Buttons
        private void InsertButtons()
        {
            // Run button.
            ButtonHelper.InsertButton(
                            ImageButtons.PlayDark, 
                            CssSelectors.MethodListRunButton, 
                            ButtonHeight,
                            delegate { FireRunClick(); });

            // Refresh button.
            ButtonHelper.InsertButton(
                            ImageButtons.RefreshDark, 
                            CssSelectors.MethodListRefreshButton,
                            ButtonHeight,
                            delegate { FireRefreshClick(); });
        }
        #endregion
    }
}
