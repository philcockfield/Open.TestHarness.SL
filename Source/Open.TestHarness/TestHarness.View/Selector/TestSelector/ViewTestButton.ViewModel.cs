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

using System.Windows;
using System.Windows.Controls;
using Open.Core.Common;
using Open.Core.Composite.Command;
using Open.TestHarness.Model;

using T = Open.TestHarness.View.Selector.ViewTestButtonViewModel;

namespace Open.TestHarness.View.Selector
{
    /// <summary>View-model for an individual [ViewTest].</summary>
    public class ViewTestButtonViewModel : ViewModelBase
    {
        #region Head
        private readonly ViewTest model;
        private readonly PropertyObserver<ViewTest> modelObserver;

        public ViewTestButtonViewModel(ViewTest model)
        {
            // Setup initial conditions.
            this.model = model;
            Click = new DelegateCommand<Button>(button => OnClick());
            ParametersViewModel = new ViewTestParametersViewModel(model);

            // Wire up events.
            modelObserver = new PropertyObserver<ViewTest>(model)
                .RegisterHandler(m => m.ExecuteCount, m => OnPropertyChanged<T>(o => o.ExecuteCount));
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            modelObserver.Dispose();
        }
        #endregion

        #region Event Handlers

        private void OnClick()
        {
            model.Execute();
        }

        #endregion


        #region Properties
        /// <summary>Gets the number of times the command has been executed.</summary>
        public string ExecuteCount
        {
            get { return model.ExecuteCount == 0 ? null : model.ExecuteCount.ToString(); }
        }

        /// <summary>Gets the display name of the item.</summary>
        public string DisplayName
        {
            get
            {
                var custom = model.Attribute.DisplayName.AsNullWhenEmpty();
                return custom ?? model.MethodInfo.Name.FormatUnderscores();
            }
        }

        /// <summary>Gets whether the method is shown in the list (see 'IsVisible' property on the [ViewTest] attribute).</summary>
        public Visibility Visibility
        {
            get{return model.Attribute.IsVisible ? Visibility.Visible : Visibility.Collapsed;}
        }

        /// <summary>Gets the click command.</summary>
        public DelegateCommand<Button> Click { get; private set; }

        /// <summary>Gets the view-model for the parameters editor.</summary>
        public ViewTestParametersViewModel ParametersViewModel { get; private set; }
        #endregion
    }
}
