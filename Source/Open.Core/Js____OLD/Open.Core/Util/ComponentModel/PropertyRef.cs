using System;
using System.Runtime.CompilerServices;

namespace Open.Core
{
    /// <summary>A reference to a property on an object.</summary>
    public class PropertyRef : PropertyDef
    {
        #region Head
        /// <summary>Fires when the property value changes.</summary>
        public event EventHandler Changed;
        private void FireChanged(){if (Changed != null) Changed(this, new EventArgs());}

        private readonly object instance;
        private readonly INotifyPropertyChanged observable;
        private PropertyBinding propertyBinding;

        /// <summary>Constructor.</summary>
        /// <param name="instance">The instance of the object that exposes the property.</param>
        /// <param name="name">The name of the property.</param>
        public PropertyRef(object instance, string name) : base(instance.GetType(), name)
        {
            // Setup initial conditions.
            this.instance = instance;

            // Wire up events.
            observable = instance as INotifyPropertyChanged;
            if (observable != null) observable.PropertyChanged += OnPropertyChanged;
        }

        /// <summary>Disposes of the object.</summary>
        public void Dispose()
        {
            if (observable != null) observable.PropertyChanged -= OnPropertyChanged;
            if (propertyBinding != null) propertyBinding.Dispose();
        }
        #endregion

        #region Event Handlers
        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.Property.Name != Name) return;
            FireChanged();
        }
        #endregion

        #region Properties
        /// <summary>Gets the instance of the object that exposes the property.</summary>
        public object Instance { get { return instance; } }

        /// <summary>Gets or sets the value of the property.</summary>
        public object Value
        {
            get { return Type.GetProperty(Instance, JavaScriptName); }
            set{ Type.SetProperty(Instance, JavaScriptName, value); }
        }
        #endregion

        #region Methods
        /// <summary>Retrieves the PropertyRef from an IModel.</summary>
        /// <param name="obj">The model object.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The PropertyRef, or Null if the property does not exist, of the given object was not an IModel.</returns>
        public static PropertyRef GetFromModel(object obj, string propertyName)
        {
            IModel model = obj as IModel;
            return model == null ? null : model.GetPropertyRef(propertyName);
        }

        /// <summary>Binds this property (the target) to the given property (the source).</summary>
        /// <param name="source">The source property to bind to.</param>
        [AlternateSignature]
        public extern bool BindTo(PropertyRef source);

        /// <summary>Binds this property (the target) to the given property (the source).</summary>
        /// <param name="source">The source property to bind to.</param>
        /// <param name="mode">The binding mode.</param>
        /// <remarks>
        ///     The property can only be bound to one source.  
        ///     Calling this method with another source disposes of the original binding.
        /// </remarks>
        public void BindTo(PropertyRef source, BindingMode mode)
        {
            ClearBinding();
            propertyBinding = new PropertyBinding(source, this, mode);
        }

        /// <summary>Removes all property bindings.</summary>
        public void ClearBinding()
        {
            if (propertyBinding != null) propertyBinding.Dispose();
        }
        #endregion
    }
}
