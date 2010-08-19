using System;
using jQueryApi;
using Open.Core;

namespace Open.Library.Jit
{
    /// <summary>Inserts and manages a hypertree.</summary>
    /// <remarks>
    ///     Requires the script file: /Scripts/Jit/HyperTreeInitialize.js
    /// </remarks>
    public class Hypertree
    {
        #region Head
        private const string ScriptUrl = "/Scripts/Jit/HyperTreeInitialize.js";
        private object hyperTree;
        private readonly jQueryObject containerElement;
        private bool isInitialized;

        /// <summary>Constructor.</summary>
        /// <param name="containerElement">The the element to inject the tree into.</param>
        public Hypertree(jQueryObject containerElement)
        {
            // Setup initial conditions.
            if (containerElement == null) throw new Exception("Container element not specified");
            this.containerElement = containerElement;

            // Wire up events.
            jQuery.Window.Bind(Events.Resize, delegate(jQueryEvent e) { UpdateSize(); });
        }
        #endregion

        #region Methods
        /// <summary>Inserts the hyper-tree into the DOM and initializes it.</summary>
        /// <param name="callback">Action to invoke upon completion.</param>
        public void Initialize(Action callback)
        {
            // Ensure the controller script is loaded.
            jQuery.GetScript(ScriptUrl, delegate(object data)
                                            {
                                                // Setup initial conditions.
                                                Size size = GetSize();
                                                string containerId = containerElement.GetAttribute("id");

                                                // Insert the tree.
                                                hyperTree = Script.Literal("insertTree({0})", containerId);
                                                UpdateSize();

                                                // Finish up.
                                                isInitialized = true;
                                                if (!Script.IsNullOrUndefined(callback)) callback();
                                            });
        }

        /// <summary>Loads the specified root node into the tree.</summary>
        /// <param name="rootNode">The root node to load.</param>
        public void Load(HypertreeNode rootNode)
        {
            if (!isInitialized) throw new Exception("Not initialized");
            Script.Literal("this._hyperTree.loadJSON(rootNode)");
            Refresh();
        }

        /// <summary>Refreshes the tree causing all points to be recalculated.</summary>
        public void Refresh()
        {
            if (!isInitialized) return;
            Script.Literal("this._hyperTree.refresh()");
            Script.Literal("this._hyperTree.controller.onAfterCompute()");
        }
        #endregion

        #region Internal
        private void UpdateSize()
        {
            if (!isInitialized) return;
            Size size = GetSize();
            Script.Literal("this._hyperTree.canvas.resize({0}, {1})", size.Width, size.Height);
            Refresh();
        }

        private Size GetSize()
        {
            return new Size(containerElement.GetWidth(), containerElement.GetHeight());
        }
        #endregion
    }
}
