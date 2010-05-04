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

namespace Open.TestHarness.View
{
    /// <summary>The root TestHarness view.</summary>
    public partial class Root : UserControl
    {
        #region Head
        public Root()
        {
            // Setup initial conditions.
            InitializeComponent();

            // Associate rows/columns with persistence behavior.
            leftColumnPersister.ColumnDefinition = columnLeft;
            rightColumnPersister.ColumnDefinition = columnRight;
            rowFooterPanelPersister.RowDefinition = rowFooterPanel;


            // Wire up events.
            //TEMP 
//            Loaded += delegate
//                          {
//                              var app = Application.Current;
//                              //TEMP 
//                              Output.Write("IsRunningOutOfBrowser" + Application.Current.IsRunningOutOfBrowser);

//                              if (app.IsRunningOutOfBrowser)
//                              {
//                                  var settings = new Model.OutOfBrowserSettings();

//                                  Output.Write("WindowSize: " + settings.WindowSize);

//                                  settings.InitializeWindow();

////                                  app.MainWindow.


//                                  app.MainWindow.Closing += delegate
//                                                      {
//                                                          MessageBox.Show("CLOSING");

//                                                          settings.SaveWindowState();
//                                                      };
                                  
//                              }
                              
//                          };
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the logical model for the control (passed to 'DataContext').</summary>
        public RootViewModel ViewModel
        {
            get { return DataContext as RootViewModel; }
            set { DataContext = value; }
        }
        #endregion
    }
}
