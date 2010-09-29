using System.Collections;
using System.Html;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    internal class ButtonContentLayer
    {
        #region Head
        public ButtonContentLayer(ButtonContentController parent, int layer)
        {
            Parent = parent;
            Layer = layer;
        }
        #endregion

        #region Properties : Public
        public readonly ButtonContentController Parent;
        public readonly int Layer;

        public readonly ArrayList Templates = new ArrayList();
        public readonly ArrayList CssClasses = new ArrayList();
        #endregion

        #region Properties : Internal
        private ButtonView View { get { return Parent.Button; } }
        private IButton Model { get { return View.Model; } }
        #endregion

        #region Methods
        public void Render(jQueryObject into)
        {
            // Generate HTML content for the layer.
            jQueryObject div = Html.CreateDiv();
            foreach (ButtonStateTemplate item in GetCurrentTemplates())
            {
                item.Template.AppendTo(div, Model.TemplateData);
            }

            // Apply CSS classes to each layer.
            string css = GetClassText();
            if (!string.IsNullOrEmpty(css))
            {
                div.Children().Each(delegate(int index, Element element)
                                        {
                                            jQuery.FromElement(element).AddClass(css);
                                        });
            }

            // Insert into the DOM.
            into.Append(div.GetHtml());
        }
        #endregion

        #region Internal
        private string GetClassText()
        {
            string text = "";
            foreach (ButtonStateCss item in GetCurrentCssClasses())
            {
                text += " " + item.CssClasses;
            }
            return text;
        }

        private IEnumerable GetCurrentTemplates() { return GetCurrentItems(Templates); }
        private IEnumerable GetCurrentCssClasses() { return GetCurrentItems(CssClasses); }
        private IEnumerable GetCurrentItems(IEnumerable collection)
        {
            return Helper.Collection.Filter(collection, delegate(object o)
                                    {
                                        return ((ButtonStateContent)o).States.Contains(View.State);
                                    });
        }
        #endregion
    }
}