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
using System.Windows.Browser;

namespace Open.Core.Common
{
    /// <summary>Event arguments for handling the window close event.</summary>
    public class HtmlWindowCloseEventArgs : CancelEventArgs
    {
        /// <summary>Gets or sets the message to display to the user asking them if they want to continue the window close.</summary>
        public string DialogMessage { get; set; }
    }


    /// <summary>Monitors the closing of the HTML window.</summary>
    public class HtmlWindowCloseMonitor
    {
        #region Events
        /// <summary>Fires when immediately before the window closes.</summary>
        public static event EventHandler<HtmlWindowCloseEventArgs> WindowClosing;
        private static void OnWindowClosing(object sender, HtmlWindowCloseEventArgs e) { if (WindowClosing != null) WindowClosing(sender, e); }
        #endregion

        #region Head
        private const string ScriptableObjectName = "HtmlWindowCloseMonitorBridge";
        private const string DefaultDialogMessage = "Are you sure you want to close the application?";
        private static readonly HtmlWindowCloseMonitor instance;

        /// <summary>Constructor.</summary>
        static HtmlWindowCloseMonitor()
        {
            if (instance == null) instance = new HtmlWindowCloseMonitor();
        }
        private HtmlWindowCloseMonitor()
        {
            // Register the scriptable callback member.
            HtmlPage.RegisterScriptableObject(ScriptableObjectName, this);

            // Retrieve the name of the plugin.
            var pluginName = GetOrCreatePluginId();

            // Wire up event.
            var eventFunction = string.Format(
                @"window.onbeforeunload = function () {{
                                    var slApp = document.getElementById('{0}');
                                    var result = slApp.Content.{1}.OnBeforeUnload();
                                    if(result != null && result.length > 0)
                                        return result;
                                }}", pluginName, ScriptableObjectName);
            HtmlPage.Window.Eval(eventFunction);
        }
        #endregion

        #region Methods
        [ScriptableMember]
        public string OnBeforeUnload()
        {
            // Setup initial conditions.
            if (WindowClosing == null) return null;

            // Check with event-listeners to see if any of them want to cancel the window-close operation.
            var args = new HtmlWindowCloseEventArgs();
            OnWindowClosing(this, args);
            if (!args.Cancel) return null; // No one wanted to stop the window from closing.

            // Present the 'Are you sure' dialog to the use (via the browser).
            var message = args.DialogMessage.AsNullWhenEmpty() != null 
                        ? args.DialogMessage 
                        : DefaultDialogMessage;
            return message;
        }
        #endregion

        #region Internal
        private static string GetOrCreatePluginId()
        {
            var id = HtmlPage.Plugin.Id.AsNullWhenEmpty();
            if (id == null)
            {
                // If the <object> has not been assigned an ID create one for it now.
                id = Guid.NewGuid().ToString();
                HtmlPage.Plugin.Id = id;
            }
            return id;
        }
        #endregion
    }
}
