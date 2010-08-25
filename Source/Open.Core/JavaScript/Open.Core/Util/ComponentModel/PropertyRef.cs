using System;

namespace Open.Core
{
    /// <summary>A reference to a property on an object.</summary>
    public class PropertyRef
    {
        #region Head
        /// <summary>Fires when the property value changes.</summary>
        public event EventHandler Changed;
        private void FireChanged(){if (Changed != null) Changed(this, new EventArgs());}

        private readonly object instance;
        private readonly INotifyPropertyChanged observable;
        private readonly string name;
        private string formattedName;
        private PropertyRef bindTo;
        private PropertyBinding propertyBinding;

        /// <summary>Constructor.</summary>
        /// <param name="instance">The instance of the object that exposes the property.</param>
        /// <param name="name">The name of the property.</param>
        public PropertyRef(object instance, string name)
        {
            // Setup initial conditions.
            this.instance = instance;
            this.name = name;

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

        /// <summary>Gets the name of the property.</summary>
        public string Name { get { return name; } }
        private string FormattedName { get { return formattedName ?? (formattedName = Helper.String.ToCamelCase(Name)); } }

        /// <summary>Gets the fully qualified name of the property..</summary>
        public string FullName { get { return Instance.GetType().FullName + ":" + Name; } }

        /// <summary>Gets or sets the value of the property.</summary>
        public object Value
        {
            get { return Type.GetProperty(Instance, FormattedName); }
            set{ Type.SetProperty(Instance, FormattedName, value); }
        }

        /// <summary>Gets or sets the source property to bind this property to.</summary>
        public PropertyRef BindTo
        {
            get { return bindTo; }
            set
            {
                if (value == BindTo) return;
                bindTo = value;
                propertyBinding = new PropertyBinding(value, this);
            }
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
        #endregion
    }
}
