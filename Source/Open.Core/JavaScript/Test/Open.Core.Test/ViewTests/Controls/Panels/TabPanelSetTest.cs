using Open.Core.Controls;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls.Panels
{
    public class TabPanelSetTest
    {
        #region Head
        private TabPanelSet panelSet;

        public void ClassInitialize()
        {
            panelSet = new TabPanelSet();
            panelSet.PanelAdded += delegate(object sender, TabPanelEventArgs args) { Log.Event("PanelAdded - " + args.Panel.Title); };

            TestHarness.DisplayMode = ControlDisplayMode.FillWithMargin;
            TestHarness.AddModel(panelSet);

            for (int i = 0; i < 3; i++)
            {
                Add_Panel();
            }

            Write_Properties();
        }
        #endregion

        #region Tests
        public void Add_Panel()
        {
            string name = "My Panel " + (panelSet.Count + 1);
            TabPanel panel = panelSet.AddPanel(name);
            panel.Div.Append(string.Format("<h2>{0}</h2>", name));
        }

        public void Select__First() { if (panelSet.FirstPanel != null) panelSet.FirstPanel.Select(); }
        public void Select__Last() { if (panelSet.LastPanel != null) panelSet.LastPanel.Select(); }

        public void Dispose__First() { if (panelSet.FirstPanel != null) panelSet.FirstPanel.Dispose(); }
        public void Dispose__Last() { if (panelSet.LastPanel != null) panelSet.LastPanel.Dispose(); }

        public void Toggle_Visibility__First() { if (panelSet.FirstPanel != null) panelSet.FirstPanel.IsVisible = !panelSet.FirstPanel.IsVisible; }
        public void Toggle_Visibility__Last() { if (panelSet.LastPanel != null) panelSet.LastPanel.IsVisible = !panelSet.LastPanel.IsVisible; }

        public void Write_Properties()
        {
            Log.WriteProperties(panelSet);
        }
        #endregion
    }
}
 