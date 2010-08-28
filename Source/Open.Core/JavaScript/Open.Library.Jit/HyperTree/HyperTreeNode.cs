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
            Children = new ArrayList();
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

    public static class HypertreeNodeFactory
    {
        #region Head
        public const string PropId = "Id";
        public const string PropName = "Name";
        public const string PropChildren = "Children";
        public const string PropData = "Data";
        #endregion

        #region Methods
        /// <summary>Constructs the node from a JSON object.</summary>
        /// <param name="json">The JSON to construct from.</param>
        public static HypertreeNode Create(Dictionary json)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(json)) throw new Exception("[Null] Cannot create from factory. JSON object not provided.");

            // Create the node.
            HypertreeNode node = new HypertreeNode(json[PropId], json[PropName] as string);
            node.Data = json[PropData];

            // Insert children.
            Array children = json[PropChildren] as Array;
            if (!Script.IsNullOrUndefined(children))
            {
                foreach (Dictionary child in children)
                {
                    node.Children.Add(Create(child));
                }
            }

            // Finish up.
            return node;
        }
        #endregion
    }

}
