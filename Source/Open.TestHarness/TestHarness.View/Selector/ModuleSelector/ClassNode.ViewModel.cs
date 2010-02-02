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
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Reflection;
using Open.Core.Composite.Command;
using Open.TestHarness.Model;

[assembly: InternalsVisibleTo("Open.TestHarness.Test")]

namespace Open.TestHarness.View.Selector
{
    /// <summary>Represents a [ViewTestClass] node within the SelectorPanel tree.</summary>
    public class ClassNodeViewModel : NodeViewModelBase
    {
        #region Head
        public const string PropDisplayName = "DisplayName";
        public const string PropIsCurrent = "IsCurrent";
        public const string PropTextColor = "TextColor";

        /// <summary>Fires when the class is selected.</summary>
        public event EventHandler Selected;

        private DelegateCommand<Button> clickCommand;

        public ClassNodeViewModel(ViewTestClass model)
        {
            Model = model;
            model.PropertyChanged += (sender, e) =>
                                         {
                                             if (e.PropertyName == ViewTestClass.PropDisplayName) OnPropertyChanged(PropDisplayName);
                                             if (e.PropertyName == ViewTestClass.PropIsCurrent) FireCurrentChanged();
                                         };
            TestHarnessModel.Instance.PropertyChanged += (sender, e) =>
                                           {
                                               if (e.PropertyName == TestHarnessModel.PropCurrentClass) FireCurrentChanged();
                                           };
        }
        #endregion

        #region Event Handlers
        private void Handle_Click( )
        {
            if (! IsCurrent) OnSelected();
            OnPropertyChanged(PropIsCurrent, PropTextColor);
        }
        #endregion

        #region Properties
        /// <summary>Gets the data model (internal use only).</summary>
        internal ViewTestClass Model { get; private set; }

        /// <summary>Gets the display name of the class.</summary>
        public string DisplayName { get { return Model.DisplayName; } } 

        /// <summary>Gets whether the assembly is currently loaded for this class.</summary>
        public bool IsAssemblyLoaded { get { return Model.IsAssemblyLoaded; } }

        /// <summary>Gets whether the class-node is currently selected.</summary>
        public bool IsCurrent { get { return Model.IsCurrent; } }

        /// <summary>Gets the command for reacting to click events.</summary>
        public ICommand Click
        {
            get
            {
                if (clickCommand == null) clickCommand = new DelegateCommand<Button>(button => Handle_Click(), button => true);
                return clickCommand;
            }
        }

        /// <summary>Gets the text color of the class (changes based on 'IsCurrent' selection status).</summary>
        public Brush TextColor
        {
            get { return IsCurrent ? new SolidColorBrush(Colors.White) : new SolidColorBrush(Colors.Black); }
        }
        #endregion

        #region Methods
        public override void OnSelected()
        {
            // Setup initial conditions.
            base.OnSelected();
            Action onComplete = delegate
                                    {
                                        // Store state.
                                        if (!Model.Activate())
                                        {
                                            // This was an error loading the type - it no longer exists within the assembly.
                                            RemoveFromRecentSelections();
                                            return;
                                        }
                                        Model.IsCurrent = true;

                                        // Finish up.
                                        if (Selected != null) Selected(this, new EventArgs());
                                    };

            // Load the assembly if it is not already loaded.
            if (!Model.IsAssemblyLoaded)
            {
                LoadAssembly(onComplete);
            }
            else
            {
                onComplete();
            }
        }
        #endregion

        #region Internal
        /// <summary>Only to be used by the testing framework.</summary>
        internal Assembly TestAssembly;
        private void LoadAssembly(Action onComplete)
        {
            // Setup initial conditions.
            var module = Model.ParentModule;

            // Load the assembly.
            if (TestAssembly != null)
            {
                module.LoadAssembly(TestAssembly);
                onComplete();
            }
            else
            {
                module.LoadAssembly(onComplete);
            }
        }

        private void FireCurrentChanged()
        {
            OnPropertyChanged(PropIsCurrent, PropTextColor);
        }

        private void RemoveFromRecentSelections()
        {
            var testHarness = TestHarnessModel.Instance;
            testHarness.RecentSelectionsModule.Classes.Remove(Model);
            testHarness.Settings.RemoveRecentSelection(Model, true);
            testHarness.Settings.Save();
        }
        #endregion
    }
}
