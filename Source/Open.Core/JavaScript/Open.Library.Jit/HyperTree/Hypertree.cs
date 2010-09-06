using System;
using System.Collections;
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
        #region Events
        /// <summary>Fires when the currently selected node changes.</summary>
        public event EventHandler SelectedNodeChanged;
        private void FireSelectedNodeChanged() { if (SelectedNodeChanged != null) SelectedNodeChanged(this, new EventArgs()); }
        #endregion

        #region Head
        internal object hyperTree;
        private readonly jQueryObject containerElement;
        private bool isInitialized;
        private HypertreeNode rootNode;
        private HypertreeNode selectedNode;
        private DelayedAction resizeDelay;
        private HypertreeNode clickedNode;
        private HypertreeInserter nodeInserter;

        /// <summary>Constructor.</summary>
        /// <param name="containerElement">The the element to inject the tree into.</param>
        public Hypertree(jQueryObject containerElement)
        {
            // Setup initial conditions.
            if (containerElement == null) throw new Exception("Container element not specified");
            this.containerElement = containerElement;

            // Insert CSS.
            Css.InsertLink(JitCss.HypertreeUrl);

            // Wire up events.
            GlobalEvents.WindowResize += delegate { OnWindowResize(); };
        }
        #endregion

        #region Event Handlers
        private void OnWindowResize()
        {
            if (resizeDelay == null) resizeDelay = new DelayedAction(0.2, delegate { UpdateSize(); });
            resizeDelay.Start();
        }

        public void OnNodeClick(HypertreeNode node)
        {
            clickedNode = node;
        }

        public void OnBeforeCompute(HypertreeNode node) { }
        public void OnAfterCompute()
        {
            SelectedNode = clickedNode;
        }

        public void OnAddComplete() { }
        #endregion

        #region Properties
        /// <summary>Gets the root node within the tree.</summary>
        public HypertreeNode RootNode { get { return rootNode; } }

        /// <summary>Gets the currently selected node.</summary>
        public HypertreeNode SelectedNode
        {
            get { return selectedNode; }
            private set
            {
                if (value == SelectedNode) return;
                selectedNode = value;
                FireSelectedNodeChanged();
            }
        }

        private HypertreeInserter NodeInserter { get { return nodeInserter ?? (nodeInserter = new HypertreeInserter(this)); } }
        #endregion

        #region Methods
        /// <summary>Inserts the hyper-tree into the DOM, initializes it and then loads the given node.</summary>
        /// <param name="callback">Action to invoke upon completion.</param>
        public void Initialize(Action callback)
        {
            // Ensure the controller script is loaded.
            // NB: Does not repeat download if scripts already loaded.
            Helper.ScriptLoader.Jit.LoadHypertree(delegate
                            {
                                // Setup initial conditions.
                                string containerId = containerElement.GetAttribute("id");

                                // Insert the tree.
                                hyperTree = Script.Literal("insertHyperTree({0}, {1})", this, containerId);
                                UpdateSize();

                                // Finish up.
                                isInitialized = true;
                                Helper.InvokeOrDefault(callback);
                            });
        }

        /// <summary>Loads the specified root node into the tree.</summary>
        /// <param name="rootNode">The root node to load.</param>
        public void Load(HypertreeNode rootNode)
        {
            if (!isInitialized) throw new Exception("HyperTree not initialized");
            Script.Literal("this._hyperTree.loadJSON(rootNode)");
            this.rootNode = rootNode;
            SelectedNode = rootNode;
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

        #region Methods : Node Insertion
        /// <summary>Inserts a node within the tree (see also 'CompleteInsertion').</summary>
        /// <param name="parent">The parent of the node to add.</param>
        /// <param name="child">The node to add</param>
        public void InsertChild(HypertreeNode parent, HypertreeNode child) 
        {
            // Update the logical model.
            if (!ContainsChild(parent, child))
            {
                if (Script.IsNullOrUndefined(parent.Children)) parent.Children = new ArrayList();
                parent.Children.Add(child);
            }

            // Queue up for insertion into the tree.
            NodeInserter.Add(parent, child);
        }

        /// <summary>Visually updates the tree after a series of child nodes have been added.</summary>
        public void CompleteInsertion()
        {
            NodeInserter.UpdateTree();
            nodeInserter = null; // Reset
        }

        internal void ExecuteNodeInsertion(object data)
        {
            Dictionary parameters = new Dictionary();
            parameters["type"] = "fade:con";
            parameters["duration"] = 1000;
            parameters["data"] = data;

            // Perform the insertion.
            Script.Literal("addHyperTreeNodes({0}, {1}, {2})", this, this.hyperTree, parameters);
        }
        #endregion

        #region Methods : Node Manipulation
        /// <summary>Looks for a macthing node within the RootNode.</summary>
        /// <param name="id">The id of the node to match.</param>
        public HypertreeNode Find(object id)
        {
            return FindWithin(id, RootNode);
        }

        /// <summary>Looks for a macthing node within the specified node.</summary>
        /// <param name="id">The id of the node to match.</param>
        /// <param name="node">The node to look within.</param>
        public static HypertreeNode FindWithin(object id, HypertreeNode node)
        {
            // Setup initial conditions.
            if (node == null || Script.IsNullOrUndefined(id)) return null;
            if (id == node.Id) return node;

            // Match from direct children.
            foreach (HypertreeNode child in node.Children)
            {
                if (child.Id == id) return child;
            }

            // Walk tree (recursion).
            foreach (HypertreeNode child in node.Children)
            {
                HypertreeNode desendent = FindWithin(id, child);
                if (desendent != null) return desendent;
            }

            // Finish up.
            return null;
        }

        /// <summary>Adds child nodes that doen't already exist within the tree.</summary>
        /// <param name="source">The source node to insert.</param>
        /// <param name="target">The target node to target node to merge into</param>
        public static void MergeChildrenInto(HypertreeNode source, HypertreeNode target)
        {
            // Setup initial conditions.
            if (source.Id != target.Id) throw new Exception("The source and target nodes are not the same.");

            foreach (HypertreeNode sourceChild in source.Children)
            {
                // Insert the child that does not exist in the target.
                HypertreeNode targetChild = GetChild(target, sourceChild.Id);
                if (targetChild == null)
                {
                    target.Children.Add(sourceChild);
                }
                else
                {
                    // Walk down the tree.
                    MergeChildrenInto(sourceChild, targetChild);
                }
            }
        }

        /// <summary>Determines whether the specified exists within the Children collection.</summary>
        /// <param name="parent">The parent node to look at.</param>
        /// <param name="childId">The ID of the child to retrieve</param>
        public static HypertreeNode GetChild(HypertreeNode parent, object childId)
        {
            if (Script.IsNull(parent)) return null;
            if (Script.IsNullOrUndefined(parent.Children)) return null;
            foreach (HypertreeNode child in parent.Children)
            {
                if (child.Id == childId) return child;
            }
            return null;
        }

        /// <summary>Determines whether the child exists directly within the parent.</summary>
        /// <param name="parent">The parent node to look at.</param>
        /// <param name="child">The child to look for.</param>
        public static bool ContainsChild(HypertreeNode parent, HypertreeNode child)
        {
            if (Script.IsNull(parent) || Script.IsNull(child)) return false;
            return GetChild(parent, child.Id) != null;
        }

        /// <summary>Selects the specified node, centering it on the tree.</summary>
        /// <param name="node">The node to select.</param>
        public void Select(HypertreeNode node)
        {
            if (node != null)
            {
                Script.Literal("this._hyperTree.onClick({0})", node.Id);
            }
            SelectedNode = node;
        }
        #endregion

        #region Internal
        private void UpdateSize()
        {
            if (!isInitialized) return;
            Size size = GetSize();
            Script.Literal("this._hyperTree.canvas.resize({0}, {1})", size.Width, size.Height);
            Refresh();
            Select(SelectedNode);
        }

        private Size GetSize()
        {
            return new Size(containerElement.GetWidth(), containerElement.GetHeight());
        }
        #endregion
    }
}
