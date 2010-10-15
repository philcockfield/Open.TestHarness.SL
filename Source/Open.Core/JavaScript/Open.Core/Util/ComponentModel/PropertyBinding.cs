using System;

namespace Open.Core
{
    /// <summary>Flags indicating the directionality of a proeprty binding.</summary>
    /// <remarks>See the [PropertyBinding] class.</remarks>
    public enum BindingMode
    {
        /// <summary>Updates only flow from the source to the target.</summary>
        OneWay = 0,

        /// <summary>Updates on the source update the target, and vice versa.</summary>
        TwoWay = 1,
    }

    /// <summary>Manages synchronization between a source property to a target property.</summary>
    public class PropertyBinding : IDisposable
    {
        #region Head
        private readonly PropertyRef source;
        private readonly PropertyRef target;
        private bool suppressSync;

        /// <summary>Constructor.</summary>
        /// <param name="source">The source property being bound to.</param>
        /// <param name="target">The target property that values are being written to (from the source).</param>
        /// <param name="mode">The binding directionality.</param>
        public PropertyBinding(PropertyRef source, PropertyRef target, BindingMode mode)
        {
            // Setup initial conditions.
            if (Script.IsNullOrUndefined(mode)) mode = BindingMode.OneWay;

            // Store values.
            this.source = source;
            this.target = target;

            // Wire up events.
            source.Changed += OnSourceChanged;
            if (mode == BindingMode.TwoWay) target.Changed += OnTargetChanged;

            // Finish up.
            SyncTarget();
        }
        #endregion

        #region Event Handlers
        private void OnSourceChanged(object sender, EventArgs e) { SyncTarget(); }
        private void OnTargetChanged(object sender, EventArgs e) { SyncSource(); }
        #endregion

        #region Methods
        /// <summary>Disposes of the object.</summary>
        public void Dispose()
        {
            source.Changed -= OnSourceChanged;
            target.Changed -= OnTargetChanged;
        }
        #endregion

        #region Internal
        private void SyncTarget() { Sync(delegate { target.Value = source.Value; }); }
        private void SyncSource() { Sync(delegate { source.Value = target.Value; }); }
        private void Sync(Action action)
        {
            // Setup initial conditions.
            if (suppressSync) return;
            suppressSync = true;

            // Synchronize value.
            action();

            // Finish up.
            suppressSync = false;
        }
        #endregion
    }
}
