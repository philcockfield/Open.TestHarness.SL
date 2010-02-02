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
using System.Windows.Media;
using System.ComponentModel;
using Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors;
using Open.Core.Composite.Command;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure
{
    /// <summary>The logical model for the 'PropertyModel'.</summary>
    public class PropertyViewModel : ViewModelBase
    {
        #region Head
        public const string PropValueTextOpacity = "ValueTextOpacity";
        public const string PropLabelTextOpacity = "LabelTextOpacity";
        public const string PropValueText = "ValueText";
        public const string PropEditorControl = "EditorControl";
        public const string PropEditorControlVisibility = "EditorControlVisibility";
        public const string PropLabelColor = "LabelColor";

        private readonly PropertyModel model;
        private readonly string typeFullName;
        private string valueText;
        private DelegateCommand<Button> valueClick;
        private EditorContainer editorControl;

        public PropertyViewModel(PropertyModel model)
        {
            // Setup initial conditions.
            this.model = model;
            typeFullName = model.Definition.PropertyType.FullName;
            IsEditable = model.Definition.CanWrite;

            // Wire up events.
            if (model.ParentInstance is INotifyPropertyChanged) ((INotifyPropertyChanged)model.ParentInstance).PropertyChanged += HandleInstancePropertyChanged;
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (!isDisposing) return;

            if (model.ParentInstance is INotifyPropertyChanged) ((INotifyPropertyChanged)model.ParentInstance).PropertyChanged -= HandleInstancePropertyChanged;
            DestroyEditorControl();
            IsDestroyed = true;
        }
        #endregion

        #region Event Handlers
        void HandleInstancePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateValueText();
        }

        private void OnValueClick( )
        {
            if (EditorControl == null) CreateEditor();
            if (EditorControl != null)
            {
                EditorControl.Loaded += delegate { if (EditorControl != null) EditorControl.Focus(); };
                EditorControl.Focus();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the embedded model.</summary>
        public PropertyModel Model { get { return model; } }

        /// <summary>Gets whether the model has been destroyed.</summary>
        public bool IsDestroyed { get; private set; }

        /// <summary>Gets whether the value can be edited.</summary>
        public bool IsEditable { get; private set; }
        
        /// <summary>Gets the color of the property name label.</summary>
        public Brush LabelColor
        {
            get
            {
                var color = EditorControl != null ? Colors.White : Colors.Black;
                return new SolidColorBrush(color);
            }
        }
        #endregion

        #region Properties - Delegates
        /// <summary>Gets the command that monitors clicks on the Value.</summary>
        public DelegateCommand<Button> ValueClick
        {
            get
            {
                if (valueClick == null) valueClick = new DelegateCommand<Button>(button => OnValueClick(), button => IsEditable);
                return valueClick;
            }
        }
        #endregion

        #region Properties - Text Labels
        /// <summary>Gets the property name.</summary>
        public string PropertyName { get { return model.DisplayName; } }

        /// <summary>Gets the text display value of the property.</summary>
        public string ValueText
        {
            get
            {
                if (valueText == null) UpdateValueText();
                return valueText;
            }
        }

        /// <summary>Gets the opacity fo the value text.</summary>
        public double LabelTextOpacity { get { return IsEditable ? 1 : 0.6; } }

        /// <summary>Gets the opacity fo the value text.</summary>
        public double ValueTextOpacity { get { return IsEditable && ValueText != ValueParser.NullLabel ? 1 : 0.6; } }

        /// <summary>Gets the tooltip that is displayed over the value.</summary>
        public string ValueToolTip { get { return typeFullName; } }        
        #endregion

        #region Properties - Editing
        /// <summary>Gets the control used to edit the property.</summary>
        public EditorContainer EditorControl
        {
            get { return editorControl; }
            private set
            {
                // Setup initial conditions.
                if (value == EditorControl) return;

                // Dispose of existing control.
                DestroyEditorControl();

                // Store new value, and wire up events.
                editorControl = value;

                // Finish up.
                OnPropertyChanged(PropEditorControl, PropLabelColor, PropEditorControlVisibility);
            }
        }

        /// <summary>Gets or sets the visibility of the editor.</summary>
        public Visibility EditorControlVisibility
        {
            get { return EditorControl == null ? Visibility.Collapsed : Visibility.Visible; }
        }
        #endregion

        #region Methods
        /// <summary>Updates the 'ValueText' properties value reading from the current property value.</summary>
        public void UpdateValueText()
        {
            // Retrieve the new value, and check if it's changed.
            var text = model.ToValueString(true);
            var fireEvents = text != valueText;

            // Update value.
            valueText = text;

            // Alert listeners.
            if (fireEvents) OnPropertyChanged(PropValueText, PropValueTextOpacity);
        }

        /// <summary>Alerts the view-model that the editor has lost focus.</summary> 
        public void EditorLostFocus()
        {
            // Setup initial conditions.
            if (EditorControl == null) return;

            // If the editor has lost focus then remove it.
            if (!EditorControl.ContainsFocus()) EditorControl = null;
        }
        #endregion

        #region Internal
        private void CreateEditor()
        {
            // Create the editor control.
            var editor = PropertyEditorFactory.GetEditor(Model);

            // Back out if the factory did not return an editor for the property's type.
            if (editor == null)
            {
                IsEditable = false;
                ValueClick.RaiseCanExecuteChanged();
                return;
            }

            // Construct the editor-container and embed the editor within it.
            var viewModel = new EditorContainerViewModel(this, editor);
            var container = new EditorContainer {ViewModel = viewModel};

            // Finish up.
            EditorControl = container;
        }

        private void DestroyEditorControl()
        {
            if (EditorControl == null) return;
            EditorControl.ViewModel.Dispose();
        }
        #endregion
    }
}
