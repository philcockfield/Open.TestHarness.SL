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
using System.Windows;
using Open.Core.Common;
using T = Open.Core.UI.Controls.PromptButtonBarViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof (IPromptButtonBar))]
    public class PromptButtonBarViewModel : ViewModelBase, IPromptButtonBar
    {
        #region Head
        private const HorizontalEdge DefaultAlignment = HorizontalEdge.Right;
        private static readonly Thickness DefaultPadding = new Thickness(4);

        public PromptButtonBarViewModel()
        {
            Background = new BackgroundModel();
            Buttons = new PromptButtonsViewModel();
        }
        #endregion

        #region Properties : IPromptButtonBar
        public IBackground Background { get; private set; }
        public IPromptButtons Buttons{ get; private set; }
        
        public bool IsVisible
        {
            get { return Property.GetValue<T, bool>(m => m.IsVisible, true); }
            set { Property.SetValue<T, bool>(m => m.IsVisible, value, true); }
        }

        public HorizontalEdge Alignment
        {
            get { return Property.GetValue<T, HorizontalEdge>(m => m.Alignment, DefaultAlignment); }
            set { Property.SetValue<T, HorizontalEdge>(m => m.Alignment, value, DefaultAlignment, m => m.HorizontalAlignment); }
        }

        public Thickness Padding
        {
            get { return Property.GetValue<T, Thickness>(m => m.Padding, DefaultPadding); }
            set { Property.SetValue<T, Thickness>(m => m.Padding, value, DefaultPadding); }
        }
        #endregion

        #region Properties : ViewModel
        public HorizontalAlignment HorizontalAlignment
        {
            get { return Alignment == HorizontalEdge.Left ? HorizontalAlignment.Left : HorizontalAlignment.Right; }
        }
        #endregion

        #region Methods
        public FrameworkElement CreateView()
        {
            return new PromptButtonBar { ViewModel = this };
        }
        #endregion
    }
}
