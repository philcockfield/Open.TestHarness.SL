﻿//------------------------------------------------------
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
using System.Reflection;
using System.Windows;
using Open.Core.Common;
using T = Open.Core.UI.Controls.CustomTool;

namespace Open.Core.UI.Controls
{
    /// <summary>A tool that can take any kind of custom UI.</summary>
    public class CustomTool : ToolBase, ICustomTool
    {
        #region Head

        private PropertyInfo isEnabledProperty;
        private const string PropIsEnabled = "IsEnabled";

        /// <summary>Constructor.</summary>
        /// <param name="viewModel">The view-model for the custom tool UI (is a ViewFactory)</param>
        public CustomTool(IViewFactory viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException("viewModel");
            ViewModel = viewModel;
            isEnabledProperty = viewModel.GetType().GetProperty(PropIsEnabled);
        }

        #endregion

        #region Properties
        /// <summary>Gets or sets the view-model for the custom tool UI (is a ViewFactory).</summary>
        public IViewFactory ViewModel { get; private set; }
        #endregion

        #region Methods
        public override FrameworkElement CreateView()
        {
            var view = ViewModel.CreateView();
            view.DataContext = ViewModel;
            return view;
        }

        protected override void OnIsEnabledChanged()
        {
            base.OnIsEnabledChanged();
            if (isEnabledProperty == null) return;
            isEnabledProperty.SetValue(ViewModel, IsEnabled, null);
        }
        #endregion
    }
}
