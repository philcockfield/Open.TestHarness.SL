using System;
using System.Collections;
using jQueryApi;

namespace Open.Core.Controls.Buttons
{
    /// <summary>Manages the display content of a button.</summary>
    internal class ButtonContentController : ControllerBase
    {
        #region Head
        private readonly ArrayList layerList = new ArrayList();

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
        public void AddTemplate(int layer, ButtonStateTemplate template)
        {
            GetLayerContent(layer).Templates.Add(template);
        }

        public void AddCss(int layer, ButtonStateCss css)
        {
            GetLayerContent(layer).CssClasses.Add(css);
        }

        public void UpdateLayout()
        {
            // Setup initial conditions.
            DivContent.Empty();
            DivContent.RemoveClass();

            // Insert HTML for each layer and add the CSS class.
            foreach (ButtonContentLayer layer in layerList)
            {
                layer.Render(DivContent);
            }
        }

        public void Clear() { layerList.Clear(); }
        #endregion

        #region Internal
        private ButtonContentLayer GetLayerContent(int layer)
        {
            // Retrieve the layer if it exists.
            ButtonContentLayer layerContent = Helper.Collection.First(layerList, delegate(object o)
                                                            {
                                                                return ((ButtonContentLayer) o).Layer == layer;
                                                            }) as ButtonContentLayer;
            if (layerContent != null) return layerContent;

            // Doesn't exist, create and store it.
            layerContent = new ButtonContentLayer(this, layer);
            layerList.Add(layerContent);

            // Ensure the collection is sored by layer.
            SortLayers();

            // Finish up.
            return layerContent;
        }

        private void SortLayers()
        {
            layerList.Sort(delegate(object o1, object o2)
                                    {
                                        int layer1 = ((ButtonContentLayer)o1).Layer;
                                        int layer2 = ((ButtonContentLayer)o2).Layer;
                                        if (layer1 == layer2) return 0;
                                        if (layer1 < layer2) return -1;
                                        return 1;
                                    });
        }
        #endregion
    }
}
