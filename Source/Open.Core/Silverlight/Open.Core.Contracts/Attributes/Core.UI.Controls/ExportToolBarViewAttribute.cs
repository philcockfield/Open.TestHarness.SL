using System;
using System.ComponentModel.Composition;

namespace Open.Core.UI.Controls
{
    /// <summary>Exports a renderer for a toolbar.</summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExportToolBarViewAttribute : ExportAttribute, IIdentifiable
    {
        /// <summary>Constructor.</summary>
        public ExportToolBarViewAttribute() : base(typeof(IToolBarView))
        {
        }

        /// <summary>Gets the key that uniquely identifies the ToolBar view.</summary>
        public object Key { get; set; }
    }
}
