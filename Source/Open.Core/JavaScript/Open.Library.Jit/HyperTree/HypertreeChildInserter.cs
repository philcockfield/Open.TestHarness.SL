using System;
using System.Collections;

namespace Open.Library.Jit
{
    internal class HypertreeChildInserter
    {
        #region Head
        private readonly Hypertree control;
        private readonly HypertreeNode parent;
        private readonly ArrayList children = new ArrayList();

        public HypertreeChildInserter(Hypertree control, HypertreeNode parent)
        {
            if (control == null) throw new Exception("Null control");
            if (parent == null) throw new Exception("Null parent");

            this.control = control;
            this.parent = parent;
        }
        #endregion

        #region Properties
        public HypertreeNode Parent { get { return parent; } }
        #endregion

        #region Methods
        public void Add(HypertreeNode child)
        {
            children.Add(child);
        }

        public void UpdateTree()
        {
            control.ExecuteNodeInsertion(ToData());
        }

        public ArrayList ToData()
        {
            ArrayList adjacentIds = new ArrayList();
            foreach (HypertreeNode child in children)
            {
                adjacentIds.Add(child.Id);
            }

            Dictionary dicAdjacencies = new Dictionary();
            dicAdjacencies["id"] = Parent.Id;
            dicAdjacencies["adjacencies"] = adjacentIds;

            // Prepare the instructions.
            ArrayList data = new ArrayList();
            data.Add(dicAdjacencies);
            foreach (HypertreeNode child in children)
            {
                data.Add(child);
            }

            return data;
        }
        #endregion
    }
}