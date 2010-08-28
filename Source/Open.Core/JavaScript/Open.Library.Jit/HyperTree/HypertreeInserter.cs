using System;
using System.Collections;
using Open.Core;

namespace Open.Library.Jit
{
    internal class HypertreeInserter
    {
        #region Head
        public readonly ArrayList Inserters = new ArrayList();
        private readonly Hypertree control;

        public HypertreeInserter(Hypertree control)
        {
            this.control = control;
        }
        #endregion

        #region Methods
        public void Add(HypertreeNode parent, HypertreeNode child)
        {
            HypertreeChildInserter relationship = GetRelationship(parent);
            relationship.Add(child);
        }

        public void UpdateTree()
        {
            foreach (HypertreeInserter item in Inserters)
            {
                item.UpdateTree();
            }
        }
        #endregion

        #region Internal
        private HypertreeChildInserter GetRelationship(HypertreeNode parent)
        {
            // Check for existing inserter.
            foreach (HypertreeChildInserter item in Inserters)
            {
                if (item.Parent.Id == parent.Id) return item;
            }

            // Create and store the inserter.
            HypertreeChildInserter inserter = new HypertreeChildInserter(control, parent);
            Inserters.Add(inserter);

            // Finish up.
            return inserter;
        }
        #endregion
    }
}
