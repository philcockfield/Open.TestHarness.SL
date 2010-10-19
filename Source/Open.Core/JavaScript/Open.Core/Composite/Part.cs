using System;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Represents a part of an application (which may or may not have UI associated with it).</summary>
    public abstract class Part : ModelBase
    {
        #region Event Handlers
        /// <summary>Fires when the part has completed initializing.</summary>
        public event EventHandler Initialized;
        private void FireInitialized(){if (Initialized != null) Initialized(this, new EventArgs());}

        /// <summary>Fires when the UpdateLayout method is invoked.</summary>
        public event EventHandler LayoutRequired;
        private void FireLayoutRequired(){if (LayoutRequired != null) LayoutRequired(this, new EventArgs());}
        #endregion

        #region Head
        public const string PropIsInitialized = "IsInitialized";
        private PartDefinition definition;
        private jQueryObject container;
        private bool isInitializing;
        #endregion

        #region Properties
        /// <summary>Gets the definition of the part.</summary>
        public PartDefinition Definition
        {
            get { return definition; }
            internal set { definition = value; }
        }

        /// <summary>Gets the visual container that the part occupies.</summary>
        public jQueryObject Container
        {
            get { return container; }
            internal set { container = value; }
        }

        /// <summary>Gets or sets whether the part has been initialized.</summary>
        public bool IsInitialized
        {
            get { return (bool) Get(PropIsInitialized, false); }
            set { Set(PropIsInitialized, value, false); }
        }
        #endregion

        #region Methods
        /// <summary>Initializes the part.</summary>
        /// <param name="callback">Action to invoke upon completion.</param>
        public void Initialize(Action callback)
        {
            // Setup initial conditions.
            if (IsInitialized || isInitializing) return;
            isInitializing = true;
            
            // Pass execution to deriving class.
            OnInitialize(delegate
                             {
                                 // Finish up.
                                 isInitializing = false;
                                 IsInitialized = true;
                                 FireInitialized();
                                 Helper.Invoke(callback);
                             });
        }

        /// <summary>Implemented in the deriving class to initialize the part.</summary>
        /// <param name="callback">Action to invoke upon completion of initialization.</param>
        /// <remarks>
        ///     All requires scripts will be loaded before this method is invoked, and the 'Container'
        ///     if available, will be set.
        /// </remarks>
        protected abstract void OnInitialize(Action callback);

        /// <summary>Forces an update to the Part's layout.</summary>
        public void UpdateLayout()
        {
            OnUpdateLayout();
            FireLayoutRequired();
        }

        /// <summary>Implemented in deriving class to update the Part's layout.</summary>
        protected virtual void OnUpdateLayout() { }
        #endregion
    }
}
