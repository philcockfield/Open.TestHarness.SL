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
using System.ComponentModel;
using System.Windows.Controls;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors
{
    /// <summary>The logical presentation model for the 'EditorContainer' view.</summary>
    public class EditorContainerViewModel : ViewModelBase
    {
        #region Head
        public const string PropPopupIsOpen = "PopupIsOpen";

        private readonly EditorViewModelBase editorViewModel;
        private bool popupIsOpen;

        public EditorContainerViewModel(PropertyViewModel propertyViewModel, Control editorControl)
        {
            // Setup initial conditions.
            PropertyViewModel = propertyViewModel;
            EditorControl = editorControl;
            
            // Wire up events.
            editorViewModel = editorControl.DataContext as EditorViewModelBase;
            if (editorViewModel == null) throw new ArgumentOutOfRangeException("editorControl", "The editor control does not have a view-model");
            editorViewModel.PropertyChanged += HandleEditorModelPropertyChanged;

            // Store the control reference in the appropriate property.
            if (editorViewModel.IsPopup)
            {
                PopupControl = editorControl;
                PopupIsOpen = true;
            }
            else
            {
                InlineControl = editorControl;
            }
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (isDisposing)
            {
                editorViewModel.PropertyChanged -= HandleEditorModelPropertyChanged;
            }
        }
        #endregion

        #region Event Handlers
        void HandleEditorModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == EditorViewModelBase.PropValue) PropertyViewModel.UpdateValueText();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the control that performs the value editing (this is also exposed in either the InlineControl or PopupControl property).</summary>
        public Control EditorControl { get; private set; }

        /// <summary>Gets or sets the control that performs the value editing (Null if this is a popup-up).  Same as 'EditorControl'.</summary>
        public Control InlineControl { get; private set; }

        /// <summary>Gets or sets the control that performs the value editing (Null if this is not a popup-up).  Same as 'EditorControl'.</summary>
        public Control PopupControl { get; private set; }

        /// <summary>Gets or sets the view-model of the property (Parent).</summary>
        public PropertyViewModel PropertyViewModel { get; private set; }

        /// <summary>Gets or sets whether the popup is currently open.</summary>
        public bool PopupIsOpen
        {
            get { return popupIsOpen; }
            set { popupIsOpen = value; OnPropertyChanged(PropPopupIsOpen); }
        }
        #endregion
    }
}
