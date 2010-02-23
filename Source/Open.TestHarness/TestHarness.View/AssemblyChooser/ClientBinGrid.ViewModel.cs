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
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.TestHarness.Model;

namespace Open.TestHarness.View.AssemblyChooser
{
    /// <summary>The logical representation of the ClientBinGrid.</summary>
    public class ClientBinGridViewModel : AcceptCancelPresenterViewModel
    {
        #region Head
        public const string PropIsShowing = "IsShowing";
        public const string PropSelectedFile = "SelectedFile";
        public const string PropAssemblyNameLabel = "AssemblyNameLabel";

        private readonly ObservableCollection<XapFile> files = new ObservableCollection<XapFile>();
        private XapFile selectedFile;
        private bool isShowing;


        public ClientBinGridViewModel()
        {
            // Setup initial conditions.
            ContentMargin = new Thickness(15);

            // Show the 'Add Modules' dialog if nothing is loaded.
            if (TestHarnessModel.Instance.Modules.Count == 0)
            {
                IsShowing = true;
                LoadAsync();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of XAP files.</summary>
        public ObservableCollection<XapFile> Files { get { return files; } }

        /// <summary>Gets or sets the selected file.</summary>
        public XapFile SelectedFile
        {
            get { return selectedFile; }
            set
            {
                selectedFile = value;
                UpdateState();
                OnPropertyChanged(PropSelectedFile);
            }
        }

        /// <summary>Gets or sets whether the grid is currently showing within a dialog-presenter.</summary>
        public bool IsShowing
        {
            get { return isShowing; }
            set
            {
                if (value == IsShowing) return;
                isShowing = value; 
                OnPropertyChanged(PropIsShowing);
            }
        }

        /// <summary>Gets whether the data is currently being loaded.summary>
        public bool IsLoading { get; private set; }
        #endregion

        #region Methods
        /// <summary>Starts the async load operation, reading the ClientBin from the server.</summary>
        /// <param name="force">Flag indicating if the load should be forced.</param>
        public void LoadAsync(bool force)
        {
            if (force || Files.Count == 0) LoadAsyncDelayed();
        }

        protected override void OnCancelClick()
        {
            base.OnCancelClick();
            IsShowing = false;
        }

        protected override void OnAcceptClick()
        {
            // Setup initial conditions.
            base.OnAcceptClick();
            IsShowing = false;
            if (SelectedFile == null) return;

            // Add the assembly to the list.
            TestHarnessModel.Instance.AddModule(SelectedFile.Name);
        }
        #endregion

        #region Internal
        private void UpdateState()
        {
            IsAcceptEnabled = SelectedFile != null;
        }

        private void LoadAsyncDelayed()
        {
            // Setup initial conditions.
            if (IsLoading) return;
            IsLoading = true;

            // Pause briefly to allow the Dialog-Present to finish animating.  Otherwise a visible bump occurs.
            new DelayedAction(0.2, LoadAsync).Start();
        }

        private void LoadAsync()
        {
            // Handle complete.
            Action<Exception> onError = ex => { throw ex; };
            Action<List<XapFile>> onSuccess = list =>
                                                  {
                                                      list = RemoveAlreadyLoadedFiles(list);
                                                      Files.RemoveAll();
                                                      Files.AddRange(list);
                                                      IsLoading = false;
                                                  };

            // Invoke web-service call.
            Network.GetClientBin(onSuccess, onError);
        }
        #endregion

        #region Internal
        private List<XapFile> RemoveAlreadyLoadedFiles(List<XapFile> files)
        {
            var modules = TestHarnessModel.Instance.Modules;
            var list = new List<XapFile>();

            foreach (var file in files)
            {
                if (modules.FirstOrDefault(item => item.DisplayName == file.Name) == null) list.Add(file);
            }

            return list;
            //foreach (var module in modules)
            //{
                
            //}

        }
        #endregion
    }
}
