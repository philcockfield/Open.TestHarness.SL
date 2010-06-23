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
using System.Windows;
using Open.Core.Common;
using Open.Core.Composite;
using T = Open.Core.UI.Controls.ToolBase;

namespace Open.Core.UI.Controls
{
    /// <summary>The base class for tools.</summary>
    public abstract class ToolBase : ModelBase, ITool
    {
        #region Head
        private static IEventBus eventBus;
        private EventHandlers eventHandlers;

        /// <summary>Constructor.</summary>
        protected ToolBase()
        {
            eventHandlers = new EventHandlers(this);
        }
        #endregion

        #region Properties
        /// <summary>Gets the static reference to the global EventBux.</summary>
        protected IEventBus EventBus
        {
            get { return eventBus ?? (eventBus = new Importer().EventBus); }
        }
        #endregion

        #region Properties - ITool
        /// <summary>Gets or sets the unique identifier of the tool.</summary>
        public object Id
        {
            get { return GetPropertyValue<T, object>(m => m.Id); }
            set { SetPropertyValue<T, object>(m => m.Id, value); }
        }

        /// <summary>Gets the toolbar that this tool resides within (null if not added to a toolbar, or is a root element).</summary>
        public IToolBar Parent
        {
            get { return GetPropertyValue<T, IToolBar>(m => m.Parent); }
            set { SetPropertyValue<T, IToolBar>(m => m.Parent, value); }
        }

        /// <summary>Gets or sets whether the tool is enabled.</summary>
        public bool IsEnabled
        {
            get { return GetPropertyValue<T, bool>(m => m.IsEnabled, true); }
            set { SetPropertyValue<T, bool>(m => m.IsEnabled, value, true); }
        }

        /// <summary>Gets or sets whether the tool is visible.</summary>
        public bool IsVisible
        {
            get { return GetPropertyValue<T, bool>(m => m.IsVisible, true); }
            set { SetPropertyValue<T, bool>(m => m.IsVisible, value, true); }
        }

        /// <summary>Gets or sets the minimum width the tool can be.</summary>
        public double MinWidth
        {
            get { return GetPropertyValue<T, double>(m => m.MinWidth); }
            set { SetPropertyValue<T, double>(m => m.MinWidth, value); }
        }

        /// <summary>Gets or sets the horizontal-alignment of the tool.</summary>
        public HorizontalAlignment HorizontalAlignment
        {
            get { return GetPropertyValue<T, HorizontalAlignment>(m => m.HorizontalAlignment, HorizontalAlignment.Left); }
            set { SetPropertyValue<T, HorizontalAlignment>(m => m.HorizontalAlignment, value, HorizontalAlignment.Left); }
        }

        /// <summary>Gets or sets the vertical-alignment of the tool.</summary>
        public VerticalAlignment VerticalAlignment
        {
            get { return GetPropertyValue<T, VerticalAlignment>(m => m.VerticalAlignment, VerticalAlignment.Top); }
            set { SetPropertyValue<T, VerticalAlignment>(m => m.VerticalAlignment, value, VerticalAlignment.Top); }
        }
        #endregion

        #region Methods
        /// <summary>Creates a new instance of the tool's view.</summary>
        public virtual FrameworkElement CreateView()
        {
            return null;
        }

        /// <summary>Fires the executed event through the EventBus.</summary>
        /// <param name="force">
        ///     The event is not fired if the tool is not enabled.  
        ///     Setting force to true will cause the event to fire even if the button is disabled.
        /// </param>
        protected virtual void PublishToolEvent(bool force = false) 
        {
            if (!IsEnabled && !force) return;
            EventBus.Publish<IToolEvent>(new ToolEvent { ToolId = Id });
        }
        #endregion

        // NB: These event handlers are encapsulated in a child-class because the EventBus requires
        // that the callback methods are public.  This prevents these methods being exposed as public
        // on the root class.
        public class EventHandlers 
        { 
            #region Head
            private readonly ToolBase parent;
            public EventHandlers(ToolBase parent)
            {
                this.parent = parent;
                parent.EventBus.Subscribe<IToolStateEvent>(OnEventStateChanged);
            }
            #endregion

            #region Event Handlers
            public void OnEventStateChanged(IToolStateEvent e)
            {
                // Setup initial conditions.
                if (parent.IsDisposed) return;
                if (!e.IsMatch(parent.Id)) return;

                // Sync state.
                if (e.IsEnabled != null) parent.IsEnabled = e.IsEnabled.Value;
            }
            #endregion
        }

        public class Importer : ImporterBase
        {
            [Import(RequiredCreationPolicy = CreationPolicy.Shared)]
            public IEventBus EventBus { get; set; }
        }
    }
}