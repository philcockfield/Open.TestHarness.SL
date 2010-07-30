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
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Silverlight.Testing;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.TestHarness.Model;
using Open.TestHarness.View.AssemblyChooser;

namespace Open.TestHarness.Test.ViewTests
{
    [ViewTestClass]
    public class ClientBinGridViewTest
    {
        #region Head
        private ClientBinGridViewModel viewModel;

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(ClientBinGrid control)
        {
            control.Width = 500;
            control.Height = 250;

            viewModel = new ClientBinGridViewModel(new DropdownDialogViewModel());
            control.DataContext = viewModel;
            viewModel.LoadAsync(false);

            viewModel.PropertyChanged += (sender, e) =>
                                             {
                                                 Debug.WriteLine("Property Changed: " + e.PropertyName);
                                                 if (viewModel.SelectedFile != null) Debug.WriteLine("    SelectedFile: " + viewModel.SelectedFile.Name);
                                                 Debug.WriteLine("");
                                             };
        }
        #endregion

        #region Tests
        [ViewTest]
        public void ViewModel_LoadAsync(ClientBinGrid control)
        {
            viewModel.LoadAsync(true);
        }

        [ViewTest]
        public void Load_XML_From_WebService(ClientBinGrid control)
        {
            Action<List<XapFile>> onSuccess = list =>
                                              {
                                                  Debug.WriteLine("SUCCESS Callback");
                                                  Debug.WriteLine("Count: " + list.Count);
                                                  foreach (var file in list)
                                                  {
                                                      Debug.WriteLine("> Name: " + file.Name + " | Kilobytes: " + file.Kilobytes);
                                                  }
                                                  Debug.WriteLine("");
                                              };

            Action<Exception> onError = ex =>
                                            {
                                                Debug.WriteLine("ERROR Callback");
                                                Debug.WriteLine(ex);
                                                Debug.WriteLine("");
                                            };

            Debug.WriteLine("------ START: GetClientBin ------");
            Network.GetClientBin(onSuccess, onError);
        }

        [ViewTest]
        public void Toggle_Width(ClientBinGrid control)
        {
            control.Width = control.Width == 500 ? 600 : 500;
        }

        [ViewTest]
        public void AcceptCancelPresenter(AcceptCancelPresenter control)
        {
            control.Content = new ClientBinGrid { Width = 550, Height = 300 };
            control.DataContext = viewModel;
        }

        [ViewTest]
        public void AcceptCancelPresenter_Load_Collection(AcceptCancelPresenter control)
        {
            if (control.Content == null) AcceptCancelPresenter(control);
            viewModel.LoadAsync(true);
        }

        [ViewTest]
        public void AcceptCancelPresenter_Clear_Collection(AcceptCancelPresenter control)
        {
            if (control.Content == null) AcceptCancelPresenter(control);
            viewModel.Files.RemoveAll();
        }
        #endregion
    }
}
