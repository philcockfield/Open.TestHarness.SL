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

using System.IO;
using System.Windows.Controls;
using Open.Core.Composite.Command;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure.Editors
{
    public class StreamEditorViewModel : EditorViewModelBase
    {
        #region Head
        private DelegateCommand<Button> openFileClick;

        public StreamEditorViewModel(PropertyModel model) : base(model)
        {
        }
        #endregion

        #region Event Handlers
        private void OnOpenFileClick()
        {
            // Present the user with the open-file dialog.
            var dialog = new OpenFileDialog { Multiselect = false };
            if (dialog.ShowDialog() != true) return;

            // Pass the file to the Value property.
            Value = dialog.File.OpenRead();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the Stream value.</summary>
        public Stream Value
        {
            get { return (Stream)Model.Value; }
            set
            {
                if (value == Value) return;
                Model.Value = value;
                OnPropertyChanged(PropValue);
            }
        }

        /// <summary>Gets the command for the '...' button.</summary>
        public DelegateCommand<Button> OpenFileClick
        {
            get
            {
                if (openFileClick == null) openFileClick = new DelegateCommand<Button>(button => OnOpenFileClick(), button => true);
                return openFileClick;
            }
        }
        #endregion
    }
}
