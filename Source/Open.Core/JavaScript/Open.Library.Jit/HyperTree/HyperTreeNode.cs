using System;
using System.Collections;
using Open.Core;

namespace Open.Library.Jit
{
    /// <summary>Represends a node within a HyperTree.</summary>
    public sealed class HypertreeNode : Record
    {
        #region Head
        /// <summary>Constructor.</summary>
        /// <param name="id">The unique identifier of the node.</param>
        /// <param name="name">The display name of the node.</param>
        public HypertreeNode(object id, string name)
        {
            Id = id;
            Name = name;
            Children =new ArrayList();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the unique identifier of the node.</summary>
        public object Id;

        /// <summary>Gets or sets the display name of the node.</summary>
        public string Name;

        /// <summary>Gets or sets the collection of children.</summary>
        public ArrayList Children;

        /// <summary>Gets or sets arbitrary data associated with the node.</summary>
        public object Data;
        #endregion
    }
}
