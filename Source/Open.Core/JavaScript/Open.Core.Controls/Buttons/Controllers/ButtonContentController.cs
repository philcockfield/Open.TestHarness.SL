using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Manages the display content of a button.</summary>
    internal class ButtonContentController : ControllerBase
    {
        #region Head
        private readonly ArrayList contentList = new ArrayList();

        public ButtonContentController(ButtonView button, jQueryObject divContent)
        {
            Button = button;
            DivContent = divContent;
        }
        #endregion

        #region Properties
        public readonly ButtonView Button;
        public readonly jQueryObject DivContent;
        #endregion

        #region Methods
        public void Add(int layer, ButtonStateContent content)
        {
            ButtonLayerContent layerContent = new ButtonLayerContent(this, layer, content);
            contentList.Add(layerContent);
        }

        public void UpdateLayout()
        {
            // Setup initial conditions.
            DivContent.Empty();
            DivContent.RemoveClass();

            // Insert HTML for each layer and add the CSS class.
            IEnumerable layers = GetCurrentStateContent();
            foreach (ButtonLayerContent layer in layers)
            {
                DivContent.Append(layer.Html);
                Css.AddClasses(DivContent, layer.Content.CssClasses);

                Log.Warning(">> CSS: " + layer.Content.CssClasses); //TEMP 

            }

            Log.Success("class=" + DivContent.GetAttribute("class")); //TEMP 

        }


        public void InvalidateCache()
        {
            foreach (ButtonLayerContent item in contentList)
            {
                item.Invalidate();
            }
        }

        #endregion

        #region Internal
        private IEnumerable GetCurrentStateContent()
        {
            // Setup initial conditions.
            ButtonState currentState = Button.State;

            // Filter on content that is for the current state.
            ArrayList items = Helper.Collection.Filter(contentList, delegate(object o)
                                    {
                                        return ((ButtonLayerContent)o).Content.States.Contains(currentState);
                                    }) as ArrayList;

            // Sort on layer.
            items.Sort(delegate(object o1, object o2)
                           {
                               int layer1 = ((ButtonLayerContent)o1).Layer;
                               int layer2 = ((ButtonLayerContent)o2).Layer;
                               if (layer1 == layer2) return 0;
                               if (layer1 < layer2) return -1;
                               return 1;
                           });

            // Finish up.)
            return items;
        }
        #endregion
    }

    internal class ButtonLayerContent
    {
        #region Head
        private Dictionary cache;
        private readonly string states;

        public ButtonLayerContent(ButtonContentController parent, int layer, ButtonStateContent content)
        {
            Parent = parent;
            Layer = layer;
            Content = content;
            foreach (ButtonState state in content.States)
            {
                states += "-" + state;
            }
        }
        #endregion

        #region Properties : Public
        public readonly ButtonContentController Parent;
        public readonly int Layer;
        public readonly ButtonStateContent Content;

        public string Html
        {
            get
            {
                // Setup initial conditions.
                if (Content.Template == null) return null;

                // Check for cached version.
                string key = GetKey();
                if (Cache.ContainsKey(key)) return Cache[key] as string;

                // Create the HTML.
                string html = Content.Template.ToHtml(Model);
                Cache[key] = html;

                // Finish up.
                return html;
            }
        }
        #endregion

        #region Properties : Internal
        private ButtonView View { get { return Parent.Button; } }
        private IButton Model { get { return View.Model; } }
        private Dictionary Cache { get { return cache ?? (cache = new Dictionary()); } }
        #endregion

        #region Methods
        public void Invalidate()
        {
            cache = null;
        }
        #endregion

        #region Internal
        private string GetKey()
        {

            // TODO - work out exclusiong if corresonding state has explicit disabled and focused values.

            return string.Format("{0}:{1}:{2}", states, Model.IsEnabled, View.IsFocused);
        }
        #endregion
    }
}
