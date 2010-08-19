using System;

namespace Open.Library.Jit
{
    public sealed class HypertreeController : Record
    {
        #region Head
        /// <summary>Constructor.</summary>
        public HypertreeController()
        {
            OnBeforeCompute = HypertreeControllerUtil.NodeHandlerFunction("handleBeforeCompute");
            OnAfterCompute = new Function("if (!ss.isNullOrUndefined(this.handleAfterCompute)) { this.handleAfterCompute(); }");
            
            OnCreateLabel = HypertreeControllerUtil.ElementHandlerFunction("handleCreateLabel");
            OnPlaceLabel = HypertreeControllerUtil.ElementHandlerFunction("handlePlaceLabel");
        }
        #endregion

        #region Properties
        // Display properties.
        public string InjectInto;
        public int Width;
        public int Height;

        // JIT Callback properties (set internally).
        public Function OnBeforeCompute;
        public Function OnAfterCompute;
        public Function OnCreateLabel;
        public Function OnPlaceLabel;

        // Handler properties (use these).
        public NodeHandler HandleBeforeCompute;
        public Action HandleAfterCompute;
        public ElementChangedHandler HandleCreateLabel;
        public ElementChangedHandler HandlePlaceLabel;
        #endregion
    }

    // Delegates
    public delegate void NodeHandler(object node);
    public delegate void ElementChangedHandler(object element, object node);

    // Helpers
    internal static class HypertreeControllerUtil
    {
        public static Function ElementHandlerFunction(string handlerName)
        {
            string script = string.Format("if (!ss.isNullOrUndefined(this.{0})) {{ this.{0}(domElement, node); }}", handlerName);

            script = "alert('Node.name; ' + node.name);";

            return new Function(
                            script, 
                            "domElement", 
                            "node");
        }

        public static Function NodeHandlerFunction(string handlerName)
        {
            return new Function(
                            string.Format("if (!ss.isNullOrUndefined(this.{0})) {{ this.{0}(node); }}", handlerName),
                            "node");
        }
    }
}