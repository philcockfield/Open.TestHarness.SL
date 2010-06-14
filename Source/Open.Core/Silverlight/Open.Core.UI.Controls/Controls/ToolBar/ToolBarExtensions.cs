using System;
using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Extensions for working with the Toolbar.</summary>
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
            this IToolBar toolbar, 
            object id = null,
            int? column = null,
            int? row = null,
            int? columnSpan = 1,
            int? rowSpan = 1)
        {
            // Setup initial conditions.
            if (toolbar == null) throw new ArgumentNullException("toolbar");

            // Create the divider.
            var divider = FactoryImporter.DividerFactory.CreateExport().Value;
            divider.Id = id;
            toolbar.Add(divider, column, row, columnSpan, rowSpan);

            // Finish up.);
            return divider;
        }

        /// <summary>Adds a custom tool to the toolbar.</summary>
        /// <param name="toolbar">The toolbar to add to.</param>
        /// <param name="viewModel">The view-model for the custom tool UI.</param>
        /// <param name="id">The unique identifier of the tool.</param>
        /// <param name="minWidth">The minimum width of the tool.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public static ICustomTool AddCustomTool(
            this IToolBar toolbar,
            IViewFactory viewModel,
            object id = null,
            int minWidth = 0,
            int? column = null,
            int? row = null,
            int? columnSpan = 1,
            int? rowSpan = 1)
        {
            // Setup initial conditions.
            if (toolbar == null) throw new ArgumentNullException("toolbar");
            if (viewModel == null) throw new ArgumentNullException("viewModel");

            // Create the tool and add it to the toolbar.
            var tool = new CustomTool(viewModel)
                           {
                               Id = id, 
                               MinWidth = minWidth
                           };
            toolbar.Add(tool, column, row, columnSpan, rowSpan);

            // Finish up.
            return tool;
        }

        /// <summary>Adds a spacer to the toolbar.</summary>
        /// <param name="toolbar">The toolbar to add to.</param>
        /// <param name="id">The unique identifier of the tool.</param>
        /// <param name="starAmount">The amount of star multiplications (*) to apply.</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public static ISpacerTool AddSpacer(
            this IToolBar toolbar,
            object id = null,
            int starAmount = 1,
            int? column = null,
            int? row = null,
            int? columnSpan = 1,
            int? rowSpan = 1)
        {
            // Setup initial conditions.
            if (toolbar == null) throw new ArgumentNullException("toolbar");

            // Create the spacer.
            var tool = FactoryImporter.SpacerFactory.CreateExport().Value;
            tool.Id = id;
            tool.ColumnWidth = new GridLength(starAmount.WithinBounds(1, int.MaxValue), GridUnitType.Star);
            toolbar.Add(tool, column, row, columnSpan, rowSpan);

            // Finish up.
            return tool;
        }

        /// <summary>Adds a tool-group (a named child-toolbar).</summary>
        /// <param name="toolbar">The toolbar to add to.</param>
        /// <param name="id">The unique identifier of the tool.</param>
        /// <param name="title">The display title of the tool-group (displayed under the toolbar).</param>
        /// <param name="column">The index of the column the tool is in (0-based, zero by default).</param>
        /// <param name="row">The index of the row the tool is in (0-based, zero by default).</param>
        /// <param name="columnSpan">The number of rows the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        /// <param name="rowSpan">The number of columns the tool spans (1-based, one by default.  Must be 1 or greater).</param>
        public static IToolBar AddToolGroup(
            this IToolBar toolbar,
            object id = null,
            string title = "Name",
            int? column = null,
            int? row = null,
            int? columnSpan = 1,
            int? rowSpan = 1)
        {
            // Setup initial conditions.
            if (toolbar == null) throw new ArgumentNullException("toolbar");

            // Create the toolbar.
            var toolGroup = FactoryImporter.ToolBarFactory.CreateExport().Value;
            toolGroup.Id = id;
            toolGroup.Title.Name = title;
            toolGroup.Title.IsVisible = true;
            toolGroup.Dividers = RectEdgeFlag.Right;
            toolbar.Add(toolGroup, column, row, columnSpan, rowSpan);

            // Finish up.);
            return toolGroup;
        }
        #endregion

        public class Importer : ImporterBase
        {
            [Import] public ExportFactory<IToolBar> ToolBarFactory { get; set; }
            [Import] public ExportFactory<IButtonTool> ButtonFactory { get; set; }
            [Import] public ExportFactory<IToolDivider> DividerFactory { get; set; }
            [Import] public ExportFactory<ISpacerTool> SpacerFactory { get; set; }
        }
    }
}
