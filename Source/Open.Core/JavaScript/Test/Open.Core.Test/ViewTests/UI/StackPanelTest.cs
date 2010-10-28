using System.Html;
using jQueryApi;
using Open.Testing;

namespace Open.Core.Test.ViewTests.UI
{
    public class StackPanelTest
    {
        #region Head
        private jQueryObject container;
        private StackPanel stackPanel;

        public void ClassInitialize()
        {
            // Setup initial conditions.
            container = TestHarness.AddElement();
            container.CSS(Css.Background, Color.Red(0.1));
            Css.SetSize(container, 400, 400);

            // Insert the children.
            for (int i = 0; i < 4; i++)
            {
                jQueryObject div = CreateChild();
                container.Append(div);
            }

            // Initialize a default stack panel.
            stackPanel = new StackPanel(container);

            // Finish up.
            TestHarness.UpdateLayout();
        }
        #endregion

        #region Tests
        public void UpdateLayout() { stackPanel.UpdateLayout(); }

        public void Toggle__StackHorizontal() { stackPanel.StackHorizontal = !stackPanel.StackHorizontal; Write_Properties(); }
        public void Toggle__StackVertical() { stackPanel.StackVertical = !stackPanel.StackVertical; Write_Properties(); }

        public void Horizontal__Left() { stackPanel.Horizontal = HorizontalAlign.Left; Write_Properties(); }
        public void Horizontal__Center() { stackPanel.Horizontal = HorizontalAlign.Center; Write_Properties(); }
        public void Horizontal__Right() { stackPanel.Horizontal = HorizontalAlign.Right; Write_Properties(); }

        public void Vertical__Top() { stackPanel.Vertical = VerticalAlign.Top; Write_Properties(); }
        public void Vertical__Center() { stackPanel.Vertical = VerticalAlign.Center; Write_Properties(); }
        public void Vertical__Bottom() { stackPanel.Vertical = VerticalAlign.Bottom; Write_Properties(); }

        public void ChildMargin__None() { stackPanel.ChildMargin.Uniform(0); Write_Properties(); }
        public void ChildMargin__1() { stackPanel.ChildMargin.Uniform(1); Write_Properties(); }
        public void ChildMargin__2() { stackPanel.ChildMargin.Uniform(2); Write_Properties(); }
        public void ChildMargin__5() { stackPanel.ChildMargin.Uniform(5); Write_Properties(); }

        public void Remove_Border()
        {
            container.Children().Each(delegate(int index, Element element)
                                          {
                                              jQueryObject obj = jQuery.FromElement(element);
                                              obj.CSS("border", "none");
                                          });
            stackPanel.UpdateLayout();
        }

        public void Write_Properties()
        {
            Log.Info("Horizontal: " + stackPanel.Horizontal.ToString());
            Log.Info("Vertical: " + stackPanel.Vertical.ToString());
            Log.Info("StackHorizontal: " + stackPanel.StackHorizontal);
            Log.Info("StackVertical: " + stackPanel.StackVertical);
            Log.Info("ChildMargin: " + stackPanel.ChildMargin.ToString());
        }
        #endregion

        #region Internal
        private static jQueryObject CreateChild()
        {
            jQueryObject div = Html.CreateDiv();
            Css.SetSize(div, 50, 50);
            div.CSS(Css.Background, Color.Orange);
            div.CSS("border-left", "solid 1px " + Color.White(0.4));
            div.CSS("border-top", "solid 1px " + Color.White(0.4));
            div.CSS("border-right", "solid 1px " + Color.Black(0.3));
            div.CSS("border-bottom", "solid 1px " + Color.Black(0.3));
            return div;
        }
        #endregion
    }
}
