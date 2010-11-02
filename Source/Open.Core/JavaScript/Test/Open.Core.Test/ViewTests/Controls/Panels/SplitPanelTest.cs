using System;
using System.Collections;
using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Panels
{
    public class SplitPanelTest
    {
        #region Head
        private SamplePane divRoot;
        private readonly ArrayList panes = new ArrayList();

        public void ClassInitialize()
        {
            TestHarness.DisplayMode = ControlDisplayMode.FillWithMargin;
            divRoot = CreateContent(null, null);
            divRoot.IsSelected = true;
            TestHarness.AddControl(divRoot);
        }
        #endregion

        #region Properties
        private SamplePane SelectedPane
        {
            get
            {
                return Helper.Collection.First(panes, delegate(object o)
                                                          {
                                                              return ((SamplePane) o).IsSelected;
                                                          }) as SamplePane;
            }
        }
        #endregion

        #region Tests
        public void Create__HorizontalSplitPanel()
        {
            if (SelectedPane == null) return;
            Log.Info("Creating Horizontal Split Panel");

            HorizontalSplitPanel panel = SplitPanel.CreateHorizontal(SelectedPane.Container, HorizontalEdge.Right);

            //panel.DivLeft.CSS(Css.Width, 120 + Css.Px);
            //panel.DivLeft.CSS(Css.Background, Color.Green(0.1));
            //panel.DivRight.CSS(Css.Background, Color.Orange(0.1));
            panel.UpdateLayout();

            SamplePane left = CreateContent(Color.Green(0.1), panel);
            SamplePane right = CreateContent(Color.Orange(0.1), panel);
            left.Width = 200;

            panel.DivLeft.Append(left.Container);
            panel.DivRight.Append(right.Container);

            panel.UpdateLayout();
        }

        public void Write_Properties()
        {
            SamplePane selectedPane = SelectedPane;
            if (selectedPane == null)
            {
                Log.Info("No pane currently selected");
            }
            else
            {
                Log.WriteProperties(selectedPane);
            }
        }
        #endregion

        #region Internal
        private SamplePane CreateContent(string bgColor, SplitPanel parent)
        {
            // Setup initial conditions.
            SamplePane pane = new SamplePane(parent);
            pane.Background = bgColor;

            // Wire up events.
            pane.Selected += delegate { Select(pane); };

            // Finish up.
            panes.Add(pane);
            return pane;
        }

        private void Select(SamplePane pane)
        {
            // Delect all panes.
            foreach (SamplePane item in panes)
            {
                if (item != pane) item.IsSelected = false;
            }
            pane.IsSelected = true;
        }
        #endregion
    }

    public class SamplePane : ViewBase
    {

        #region Head
        private readonly SplitPanel parent;

        /// <summary>Fires when the pane is selected</summary>
        public event EventHandler Selected;
        private void FireSelected(){if (Selected != null) Selected(this, new EventArgs());}

        public const string PropIsSelected = "IsSelected";

        /// <summary>Constructor.</summary>
        public SamplePane(SplitPanel parent)
        {
            this.parent = parent;
            Container.Click(delegate
                                {
                                    IsSelected = true;
                                });
        }

        #endregion

        #region Properties
        public SplitPanel Parent { get { return parent; } }

        public bool IsSelected
        {
            get { return (bool) Get(PropIsSelected, false); }
            set
            {
                if(Set(PropIsSelected, value, false))
                {
                    Container.Empty();
                    Container.Append(value ? "Selected" : "");
                    FireSelected();
                }
            }
        }
        #endregion
    }
}
