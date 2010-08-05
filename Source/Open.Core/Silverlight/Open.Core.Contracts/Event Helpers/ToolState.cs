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

using System.ComponentModel.Composition;
using Open.Core.Composite;

namespace Open.Core.UI.Controls
{
    /// <summary>Helper class that provides a convenient way to fire tool-state change events (via the event-bus).</summary>
    public static class ToolState
    {
        #region Head
        private static ImporterClass importer;
        #endregion

        #region Properties
        private static ImporterClass Importer { get { return importer ?? (importer= new ImporterClass()); } }
        #endregion

        #region Methods
        /// <summary>Causes the specified tool to change state by firing an IToolStateEvent via the event-bus.</summary>
        /// <param name="toolId">The unique identifier of the tool to change state on.</param>
        /// <param name="isEnabled">The enabled state (null if current state remains unchanged).</param>
        /// <param name="isAsynchronous">Flag indicating if the update should happen asynchronously.</param>
        public static void Change(object toolId, bool? isEnabled = null, bool isAsynchronous = false)
        {
            // Prepare the event args.
            var args = Importer.ToolStateEventFactory.CreateExport().Value;
            args.ToolId = toolId;
            args.IsEnabled = isEnabled;

            // Fire event.
            Importer.EventBus.Publish(args, isAsynchronous);
        }
        #endregion

        public class ImporterClass
        {
            public ImporterClass() { CompositionInitializer.SatisfyImports(this); }

            [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
            public IEventBus EventBus { get; set; }

            [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
            public ExportFactory<IToolStateEvent> ToolStateEventFactory { get; set; }
        }
    }
}
