using System;
using Open.Core.Helpers;

namespace Open.Core
{
    /// <summary>Static index of helpers.</summary>
    public static class Helper
    {
        #region Head
        private static DelegateHelper DelegateHelper;
        private static JsonHelper JsonHelper;
        private static ReflectionHelper ReflectionHelper;
        private static ScriptLoadHelper ScriptLoadHelper;
        private static CollectionHelper CollectionHelper;
        private static StringHelper StringHelper;
        private static TimeHelper TimeHelper;
        private static ScrollHelper ScrollHelper;
        private static JQueryHelper JQueryHelper;
        private static TreeHelper TreeHelper;
        private static EventHelper EventHelper;
        private static DoubleHelper DoubleHelper;
        private static UrlHelper UrlHelper;
        private static ExceptionHelper ExceptionHelper;
        private static TemplateHelper TemplateHelper;
        private static IconHelper IconHelper;

        private static int idCounter;
        #endregion

        #region Properties
        /// <summary>Gets the helper for working with Delegates.</summary>
        public static DelegateHelper Delegate { get { return DelegateHelper ?? (DelegateHelper = new DelegateHelper()); } }

        /// <summary>Gets the helper for working with Delegates.</summary>
        public static JsonHelper Json { get { return JsonHelper ?? (JsonHelper = new JsonHelper()); } }

        /// <summary>Gets the helper for working with reflection.</summary>
        public static ReflectionHelper Reflection { get { return ReflectionHelper ?? (ReflectionHelper = new ReflectionHelper()); } }

        /// <summary>Gets the helper for downloading scripts.</summary>
        public static ScriptLoadHelper ScriptLoader { get { return ScriptLoadHelper ?? (ScriptLoadHelper = new ScriptLoadHelper()); } }

        /// <summary>Gets the helper for working with collections.</summary>
        public static CollectionHelper Collection { get { return CollectionHelper ?? (CollectionHelper = new CollectionHelper()); } }

        /// <summary>Gets the helper for working with strings.</summary>
        public static StringHelper String { get { return StringHelper ?? (StringHelper = new StringHelper()); } }

        /// <summary>Gets the helper for working with numbers.</summary>
        public static TimeHelper Time { get { return TimeHelper ?? (TimeHelper = new TimeHelper()); } }

        /// <summary>Gets the helper for working with scrolling.</summary>
        public static ScrollHelper Scroll { get { return ScrollHelper ?? (ScrollHelper = new ScrollHelper()); } }

        /// <summary>Gets the helper for working with JQuery.</summary>
        public static JQueryHelper JQuery { get { return JQueryHelper ?? (JQueryHelper = new JQueryHelper()); } }

        /// <summary>Gets the helper for working with Tree data-structures.</summary>
        public static TreeHelper Tree { get { return TreeHelper ?? (TreeHelper = new TreeHelper()); } }

        /// <summary>Gets the helper for working with events.</summary>
        public static EventHelper Event { get { return EventHelper ?? (EventHelper = new EventHelper()); } }

        /// <summary>Gets the helper for working with doubles.</summary>
        public static DoubleHelper NumberDouble { get { return DoubleHelper ?? (DoubleHelper = new DoubleHelper()); } }

        /// <summary>Gets the helper for working with URLs.</summary>
        public static UrlHelper Url { get { return UrlHelper ?? (UrlHelper = new UrlHelper()); } }

        /// <summary>Gets the helper for working with Exceptions.</summary>
        public static ExceptionHelper Exception { get { return ExceptionHelper ?? (ExceptionHelper = new ExceptionHelper()); } }

        /// <summary>Gets the helper for working with Templates.</summary>
        public static TemplateHelper Template { get { return TemplateHelper ?? (TemplateHelper = new TemplateHelper()); } }

        /// <summary>Gets the helper for working with icons.</summary>
        public static IconHelper Icon { get { return IconHelper ?? (IconHelper = new IconHelper()); } }
        #endregion

        #region Methods
        /// <summary>Invokes the given action if it's not Null/Undefined.</summary>
        /// <param name="action">The action to invoke.</param>
        public static void Invoke(Action action)
        {
            if (!Script.IsNullOrUndefined(action)) action();
        }

        /// <summary>Creates a unique identifier.</summary>
        public static string CreateId()
        {
            idCounter++;
            return string.Format("gid{0}", idCounter);
        }

        /// <summary>Disposes of the object (if it's not null and is an IDisposable).</summary>
        /// <param name="obj">The object to dispose of.</param>
        public static void Dispose(object obj)
        {
            if (Script.IsNullOrUndefined(obj)) return;
            IDisposable disposable = obj as IDisposable;
            if (disposable != null) disposable.Dispose();
        }

        /// <summary>Casts the given object to an 'INotifyPropertyChanged' and if it is observable wires up the given event handler to the 'PropertyChanged' event.</summary>
        /// <param name="model">The model to wire up.</param>
        /// <param name="handler">The handler to attach.</param>
        public static void ListenPropertyChanged(object model, PropertyChangedHandler handler)
        {
            INotifyPropertyChanged observable = model as INotifyPropertyChanged;
            if (observable != null) observable.PropertyChanged += handler;
        }

        /// <summary>Casts the given object to an 'INotifyPropertyChanged' and if it is observable un-wires the given event handler from the 'PropertyChanged' event.</summary>
        /// <param name="model">The model to un-wire.</param>
        /// <param name="handler">The handler to remove.</param>
        public static void UnlistenPropertyChanged(object model, PropertyChangedHandler handler)
        {
            INotifyPropertyChanged observable = model as INotifyPropertyChanged;
            if (observable != null) observable.PropertyChanged -= handler;
        }
        #endregion
    }
}
