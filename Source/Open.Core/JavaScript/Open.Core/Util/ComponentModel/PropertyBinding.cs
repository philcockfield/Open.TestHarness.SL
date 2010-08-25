using System;

namespace Open.Core
{
    /// <summary>Manages synchronization between a source property to a target property.</summary>
    public class PropertyBinding : IDisposable
    {
        #region Head
        private readonly PropertyRef source;
        private readonly PropertyRef target;

        public PropertyBinding(PropertyRef source, PropertyRef target)
        {
            // Store values.
            this.source = source;
            this.target = target;

            // Wire up events.
            source.Changed += OnSourceChanged;

            // Finish up.
            Sync();
        }
        #endregion

        #region Event Handlers
        private void OnSourceChanged(object sender, EventArgs e)
        {
            Sync();
        }
        #endregion

        #region Methods
        /// <summary>Disposes of the object.</summary>
        public void Dispose()
        {
            source.Changed -= OnSourceChanged;
        }
        #endregion

        #region Internal
        private void Sync()
        {
            target.Value = source.Value;
        }
        #endregion
    }
}
