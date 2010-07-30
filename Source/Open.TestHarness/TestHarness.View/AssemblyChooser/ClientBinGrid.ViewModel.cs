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
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using Open.Core;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.TestHarness.Model;

namespace Open.TestHarness.View.AssemblyChooser
{
    /// <summary>The logical representation of the ClientBinGrid.</summary>
    public class ClientBinGridViewModel : AcceptCancelPresenterViewModel, IViewFactory
    {
        #region Head
        public const string PropSelectedFile = "SelectedFile";
        public const string PropAssemblyNameLabel = "AssemblyNameLabel";

        private readonly ObservableCollection<XapFile> files = new ObservableCollection<XapFile>();
        private readonly IDropdownDialog modalDialog;
        private XapFile selectedFile;

        public ClientBinGridViewModel(IDropdownDialog modalDialog)
        {
            // Setup initial conditions.
            this.modalDialog = modalDialog;
            ContentMargin = new Thickness(15);

            // Show the 'Add Modules' dialog if nothing is loaded.
            if (TestHarnessModel.Instance.AssemblyModules.Count() == 0)
            {
                // Ensure that a XAP was not auto loaded.
                CompositionInitializer.SatisfyImports(this);
                if (QueryString.XapFiles.IsEmpty())
                {
                    ShowInDialog();
                    LoadAsync();
                }
            }
        }
        #endregion

        #region Event Handlers
        internal void OnDoubleClick()
        {
            modalDialog.IsShowing = false;
            OnDialogAccept();
        }

        private void OnDialogAccept()
        {
            // Add the assembly to the list.
            if (SelectedFile != null) TestHarnessModel.Instance.AddModule(SelectedFile.Name);
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

        /// <summary>Gets whether the data is currently being loaded.</summary>
        public bool IsLoading { get; private set; }

        /// <summary>Gets or sets QueryString singleton (requires MEF to initialize).</summary>
        [Import]
        public QueryString QueryString { get; set; }
        #endregion

        #region Methods
        /// <summary>Starts the async load operation, reading the ClientBin from the server.</summary>
        /// <param name="force">Flag indicating if the load should be forced.</param>
        public void LoadAsync(bool force)
        {
            if (force || Files.Count == 0) LoadAsyncDelayed();
        }

        /// <summary>Reveals the grid in the modal dialog.</summary>
        public void ShowInDialog()
        {
            modalDialog.Padding = new Thickness(30, 20, 30, 20);
            modalDialog.Mask.Opacity = 0.7;
            modalDialog.Mask.Color = Colors.White.ToBrush();
            modalDialog.Background.Opacity = 0.7;
            modalDialog.Show(this, result =>
                                            {
                                                if (result == PromptResult.Accept)
                                                {
                                                    OnDialogAccept();
                                                }
                                            }, 
                                            DialogSize.Fixed, 
                                            PromptButtonConfiguration.OkCancel);
        }

        public FrameworkElement CreateView()
        {
            return new ClientBinGrid
                       {
                           DataContext = this,
                           Width = 500,
                           Height = 200,
                       };
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

        private static List<XapFile> RemoveAlreadyLoadedFiles(List<XapFile> files)
        {
            var modules = TestHarnessModel.Instance.Modules;
            var list = new List<XapFile>();

            foreach (var file in files)
            {
                if (modules.FirstOrDefault(item => item.DisplayName == file.Name) == null) list.Add(file);
            }

            return list;
        }
        #endregion
    }
}
