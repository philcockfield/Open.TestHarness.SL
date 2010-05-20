using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    public static class ToolBarExtensions
    {
        #region Head
        private static Importer factoryImporter;
        #endregion

        #region Properties
        private static Importer FactoryImporter
        {
            get { return factoryImporter ?? (factoryImporter = new Importer()); }
        }
        #endregion

        #region Methods
        /// <summary>Adds a button tool to the toolbar.</summary>
        /// <param name="toolbar">The toolbar to add to.</param>
        /// <param name="id">The unique identifier of the tool.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="text">The text label.</param>
        /// <param name="orientation">The orientation of the label relative to the icon.</param>
        /// <param name="showDefaultBackground">Flag indicating whether the default background is rendered when the mouse is not over the tool.</param>
        /// <param name="toolTip">The tooltip</param>
        /// <param name="minWidth">The minimum width of the tool.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public static IButtonTool AddButton(
                    this IToolBar toolbar,
                    object id = null,
                    IconImage icon = IconImage.SilkAccept,
                    String text = null,
                    Orientation orientation = Orientation.Horizontal,
                    bool showDefaultBackground = false,
                    string toolTip = null,
                    int minWidth = 0,
                    int? column = null,
                    int? row = null,
                    int? columnSpan = 1,
                    int? rowSpan = 1)
        {
            return toolbar.AddButton(
                                    id,                    
                                    icon.ToImage(), text, 
                                    orientation, showDefaultBackground, toolTip, minWidth,
                                    column, row, columnSpan, rowSpan);
        }

        /// <summary>Adds a button tool to the toolbar.</summary>
        /// <param name="toolbar">The toolbar to add to.</param>
        /// <param name="id">The unique identifier of the tool.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="text">The text label.</param>
        /// <param name="orientation">The orientation of the label relative to the icon.</param>
        /// <param name="showDefaultBackground">Flag indicating whether the default background is rendered when the mouse is not over the tool.</param>
        /// <param name="toolTip">The tooltip</param>
        /// <param name="minWidth">The minimum width of the tool.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public static IButtonTool AddButton(
                    this IToolBar toolbar,
                    object id = null,
                    Image icon = null,
                    String text = null,
                    Orientation orientation = Orientation.Horizontal,
                    bool showDefaultBackground = false,
                    string toolTip = null,
                    int minWidth = 0,
                    int? column = null,
                    int? row = null,
                    int? columnSpan = 1,
                    int? rowSpan = 1)
        {
            // Setup initial conditions.
            if (toolbar == null) throw new ArgumentNullException("toolbar");

            // Create and configure the button.
            var tool = FactoryImporter.ButtonFactory.CreateExport().Value;
            tool.Id = id;
            tool.Icon = icon;
            tool.Text = text;
            tool.ToolTip = toolTip;
            tool.MinWidth = minWidth;
            tool.IsDefaultBackgroundVisible = showDefaultBackground;
            tool.Orientation = orientation;

            // Add to the toolbar.
            toolbar.Add(tool, column, row, columnSpan, rowSpan);

            // Finish up.
            return tool;
        }

        /// <summary>Adds a divider to the toolbar.</summary>
        /// <param name="toolbar">The toolbar to add to.</param>
        /// <param name="id">The unique identifier of the tool.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public static IToolDivider AddDivider(
                    this IToolBar toolbar, object id = null,
                    int? column = null,
                    int? row = null,
                    int? columnSpan = 1,
                    int? rowSpan = 1)
        {
            // Setup initial conditions.
            if (toolbar == null) throw new ArgumentNullException("toolbar");

            // Create the divider.
            var divider = FactoryImporter.DividerFactory.CreateExport().Value;
            toolbar.Add(divider, column, row, columnSpan, rowSpan);

            // Finish up.);
            return divider;
        }
        #endregion

        public class Importer : ImporterBase
        {
            [Import(typeof(IButtonTool))]
            public ExportFactory<IButtonTool> ButtonFactory { get; set; }

            [Import(typeof(IToolDivider))]
            public ExportFactory<IToolDivider> DividerFactory { get; set; }
        }
    }
}
