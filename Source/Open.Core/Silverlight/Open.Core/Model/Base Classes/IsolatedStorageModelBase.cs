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
using System.IO.IsolatedStorage;
using T = Open.Core.Common.IsolatedStorageModelBase;

namespace Open.Core.Common
{
    /// <summary>Flags indicating the kind of settings store.</summary>
    public enum IsolatedStorageType
    {
        /// <summary>Settings scoped at the application level.</summary>
        Application,

        /// <summary>Settings scoped at the domain level.</summary>
        Site
    }

    /// <summary>Base class for settings that are persisted to isolated storage on the client.</summary>
    public abstract class IsolatedStorageModelBase : ModelBase
    {
        #region Events
        /// <summary>Fires when immediately before the model settings are saved.</summary>
        public event EventHandler<CancelEventArgs> Saving;
        private void OnSaving(CancelEventArgs e) { if (Saving != null) Saving(this, e); }

        /// <summary>Fires when the values in the Store has been saved.</summary>
        public event EventHandler Saved;
        private void OnSaved() { if (Saved != null) Saved(this, new EventArgs()); }

        /// <summary>Fires when immediately before the model settings are cleared</summary>
        public event EventHandler<CancelEventArgs> Clearing;
        private void OnClearing(CancelEventArgs e){if (Clearing != null) Clearing(this, e);}

        /// <summary>Fires when the model settings have been cleared</summary>
        public event EventHandler Cleared;
        private void OnCleared(){if (Cleared != null) Cleared(this, new EventArgs());}
        #endregion

        #region Head
        private const int bytesInMegabyte = 1048576;
        private DelayedAction saveDelayedAction;

        /// <summary>Constructor.</summary>
        /// <param name="storeType">The type of persistence store.</param>
        /// <param name="id">The unique identifier of the settings (used as a prefix).</param>
        protected IsolatedStorageModelBase(IsolatedStorageType storeType, string id)
        {
            // Setup initial conditions.
            if (id.AsNullWhenEmpty() == null) throw new ArgumentNullException("id");

            // Store values.
            Id = id;
            StoreType = storeType;
            Store = GetStore(storeType);

            // Set default values.
            AutoSave = true;
            AutoIncrementQuotaBy = 2;

            // Populate property values from store (if they exist).
            var propertyManager = Property;
            string serializedValues;
            if (Store.TryGetValue(id, out serializedValues))
            {
                if (!serializedValues.IsNullOrEmpty(true)) propertyManager.Populate(serializedValues);
            }

            // Wire up events.
            propertyManager.PropertySet += delegate { ProcessAutoSave(); };
        }
        #endregion

        #region Properties
        /// <summary>Gets the unique identifier of the settings (used as a prefix)</summary>
        public string Id { get; private set; }

        /// <summary>Gets the type of persistence store this model is using.</summary>
        public IsolatedStorageType StoreType { get; private set; }

        /// <summary>Gets the persistence store.</summary>
        public IsolatedStorageSettings Store { get; private set; }

        /// <summary>Gets or sets whether values written to the Store are automatically saved.</summary>
        public bool AutoSave { get; set; }

        /// <summary>Gets or sets whether the data is serialized to a string before being stored.</summary>
        public bool StoreAsSerializedString { get; set; }

        /// <summary>Gets whether the client-side isolated storage service is enabled.</summary>
        /// <remarks>This can be disabled from the 'Silverlight Settings' Application Storage tab.</remarks>
        public static bool IsApplicationStorageEnabled
        {
            get
            {
                try
                {
                    // Attempt to retrieve the store.  Will throw if disabled.
                    var store = GetStore(IsolatedStorageType.Application);
                    store.Save();
                    return true;
                }
                catch (IsolatedStorageException)
                {
                    return false; // Store not accessable.
                }
            }
        }

        /// <summary>Gets whether this is the first time the storage model has been loaded (no content exists).</summary>
        public bool IsFirstLoad { get { return !Store.Contains(Id); } }
        #endregion

        #region Properties : Quota
        /// <summary>Gets the available free space within the store (in Bytes).</summary>
        public long AvailableFreeBytes
        {
            get
            {
                using (var store = GetStoreFile(StoreType))
                {
                    return store.AvailableFreeSpace;
                }
            }
        }

        /// <summary>Gets the available free space within the store (in MB).</summary>
        public double AvailableFreeMegabytes
        {
            get { return (double)AvailableFreeBytes / bytesInMegabyte; }
        }

        /// <summary>Gets the storage quota (in Bytes).</summary>
        public long QuotaBytes
        {
            get
            {
                using (var store = GetStoreFile(StoreType))
                {
                    return store.Quota;
                }
            }
        }

        /// <summary>Gets the storage quota (in MB).</summary>
        public double QuotaMegabytes
        {
            get { return (double)QuotaBytes / bytesInMegabyte; }
        }

        /// <summary>
        ///    Gets or sets the amount of MB to attempt to increase the storage quota 
        ///    by if a Save operation cannot be completed due to insufficient space.
        ///    Null if auto-increase is not required.
        /// </summary>
        public double? AutoIncrementQuotaBy { get; set; }
        #endregion

        #region Properties : Internal
        private DelayedAction SaveDelayedAction
        {
            get { return saveDelayedAction ?? (saveDelayedAction = new DelayedAction(0.1, () => Save())); }
        }
        #endregion

        #region Methods
        /// <summary>Removes all the model's items from the Store.</summary>
        /// <remarks>This does not clear the entire store, just the items associated with this model.</remarks>
        public virtual void Clear()
        {
            // Setup initial conditions.
            var e = new CancelEventArgs();
            OnClearing(e);
            if (e.Cancel) return;

            // Invoke pre-save operation on deriving class.
            OnBeforeClear();

            // Remove items.
            lock(Store)
            {
                Store.Remove(Id);
            }
            lock (Property)
            {
                Property.Clear();
            }

            // Finish up.
            ProcessAutoSave();
            OnCleared();
        }

        /// <summary>Invoked immediately before the Clear operation.  Use this to perform pre-clear operations.</summary>
        /// <remarks>This method is not called if a listener to the 'Clearing' event cancelled the operation.</remarks>
        protected virtual void OnBeforeClear() { }

        /// <summary>Attempts to increase the quota by the specified number of megabytes.</summary>
        /// <returns>True if the user accepted the incrase, otherwise False.</returns>
        public bool IncreaseQuotaBy(double megabytes)
        {
            var bytes = (long)(megabytes * bytesInMegabyte);
            return IncreaseQuotaTo(bytes + AvailableFreeBytes);
        }

        /// <summary>Attempts to increase the quota by the specified number of megabytes.</summary>
        /// <returns>True if the user accepted the incrase, otherwise False.</returns>
        public bool IncreaseQuotaTo(double megabytes)
        {
            return IncreaseQuotaTo((long)(megabytes * bytesInMegabyte));
        }
        #endregion

        #region Method : Save
        /// <summary>Starts the delayed Save action.</summary>
        public void DelaySave()
        {
            SaveDelayedAction.Start();
        }

        /// <summary>Saves the settings to disk.</summary>
        public virtual bool Save() { return Save(AutoIncrementQuotaBy); }

        /// <summary>Saves the settings to disk.</summary>
        /// <param name="autoIncrementQuotaBy">The value to attempt to auto increment the quota by if there is not enough space to save.</param>
        /// <returns>
        ///    True if the model was saved, 
        ///    or False if there was not enough space to save (and either an 
        ///    auto-increment amount wasn't specified, or the user refused to increase the quota).
        /// </returns>
        /// <exception cref="IsolatedStorageException">
        ///    Is thrown if (after increasing the quota) the file could still not be saved.
        ///    Make sure when saving that a suitable quota increase amount is specified.
        /// </exception>
        public bool Save(double? autoIncrementQuotaBy)
        {
            // Setup initial conditions.
            var e = new CancelEventArgs();
            OnSaving(e);
            if (e.Cancel) return false;

            // Invoke pre-save operation on deriving class.
            OnBeforeSave();

            // Attempt to save the value.
            var error = SaveInternal();
            if (error == null) return true;

            // A save error occured.
            // Check ot see if auto-incrementing was requested.
            if (autoIncrementQuotaBy == null) return false;

            // Attempt to auto-increment the quota.
            var wasIncremented = IncreaseQuotaBy(autoIncrementQuotaBy.Value);
            if (!wasIncremented) return false;

            // Attempt to re-save the model now the quota has been increased.
            error = SaveInternal();
            if (error != null) throw error;

            // Finish up.
            return true;
        }

        /// <summary>Invoked immediately before the Save operation.  Use this to perform pre-save operations.</summary>
        /// <remarks>This method is not called if a listener to the 'Saving' event cancelled the operation.</remarks>
        protected virtual void OnBeforeSave() { }

        private IsolatedStorageException SaveInternal()
        {
            lock (Store)
            {
                try
                {
                    Store[Id] = Property.GetSerializedValues();
                    Store.Save();
                }
                catch (IsolatedStorageException e)
                {
                    return e;
                }
            }
            OnSaved();
            return null;
        }
        #endregion

        #region Internal
        private bool IncreaseQuotaTo(long bytes)
        {
            using (var store = GetStoreFile(StoreType))
            {
                return store.IncreaseQuotaTo(bytes);
            }
        }

        private void ProcessAutoSave()
        {
            if (AutoSave) DelaySave();
        }

        private static IsolatedStorageSettings GetStore(IsolatedStorageType storeType)
        {
            switch (storeType)
            {
                case IsolatedStorageType.Application: return IsolatedStorageSettings.ApplicationSettings;
                case IsolatedStorageType.Site: return IsolatedStorageSettings.SiteSettings;
                
                default: throw new NotSupportedException(storeType.ToString());
            }
        }

        // Use the return value from this method within a 'Using' statement to release resources when done.
        private static IsolatedStorageFile GetStoreFile(IsolatedStorageType storeType)
        {
            switch (storeType)
            {
                case IsolatedStorageType.Application: return IsolatedStorageFile.GetUserStoreForApplication();
                case IsolatedStorageType.Site: return IsolatedStorageFile.GetUserStoreForSite();

                default: throw new NotSupportedException(storeType.ToString());
            }
        }
        #endregion
    }
}
