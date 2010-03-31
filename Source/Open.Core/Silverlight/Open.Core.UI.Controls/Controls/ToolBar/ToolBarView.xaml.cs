//------------------------------------------------------
//    Copyright (c) 2010 TestHarness.org
//
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the 'Software'), to deal
//    in the Software without restriction, including without limitation the rights
//    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//    copies of the Software, and to permit persons to whom the Software is
//    furnished to do so, subject to the following conditions:
//
//    The above copyright notice and this permission notice shall be included in
//    all copies or substantial portions of the Software.
//
//    THE SOFTWARE IS PROVIDED 'AS IS', WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//    THE SOFTWARE.
//------------------------------------------------------

using System;
using System.ComponentModel.Composition;
using System.Windows.Controls;
using Open.Core.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Renders a toolbar.</summary>
    [ExportToolBarView(Id = ToolBarModel.DefaultViewExportKey)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public partial class ToolBarView : UserControl, IToolBarView
    {
        #region Head
        private readonly DataContextObserver dataContextObserver;
        private IToolBar viewModel;
        private ToolLayoutController toolLayoutController;

        /// <summary>Constructor.</summary>
        public ToolBarView()
        {
            InitializeComponent();
            dataContextObserver = new DataContextObserver(this, OnDataContextChanged);
        }
        #endregion

        #region Event Handlers
        private void OnDataContextChanged()
        {
            // Setup initial conditions.
            if (toolLayoutController != null) toolLayoutController.Dispose();

            // Unwire old view-model.
            if (viewModel != null) viewModel.UpdateLayoutRequest -= OnUpdateLayout;

            // Wire up events.
            viewModel = ViewModel;
            if (viewModel != null) viewModel.UpdateLayoutRequest += OnUpdateLayout;
        }

        private void OnUpdateLayout(object sender, EventArgs e)
        {
            if (ViewModel == null) return;
            if (toolLayoutController == null) toolLayoutController = new ToolLayoutController(toolContainer);
            toolLayoutController.LayupTools(ViewModel);
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public IToolBar ViewModel
        {
            get { return DataContext as IToolBar; }
            set { DataContext = value; }
        }
        #endregion
    }
} 