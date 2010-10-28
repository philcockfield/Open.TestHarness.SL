using System;
using System.Collections;

namespace Open.Core
{
    /// <summary>A simple DI container.</summary>
    public class DiContainer : IDisposable
    {
        #region Head
        private readonly ArrayList singletons = new ArrayList();
        private static DiContainer defaultContainer;

        /// <summary>Destructor.</summary>
        public void Dispose()
        {
            Helper.Collection.DisposeAndClear(singletons);
        }
        #endregion

        #region Properties
        /// <summary>Gets the default DI container.</summary>
        public static DiContainer DefaultContainer { get { return defaultContainer ?? (defaultContainer = new DiContainer()); } }
        #endregion

        #region Methods : Singleton
        /// <summary>Retrieves the singleton that matches the given type.</summary>
        /// <param name="key">The type-key (either the Type of the singleton, or an interface).</param>
        /// <returns>The singleton if it's been registered, otherwise Null.</returns>
        public object GetSingleton(Type key)
        {
            DiInstanceWrapper wrapper = FromKey(key);
            return wrapper == null ? null : wrapper.Instance;
        }

        /// <summary>Retrieves the singleton that matches the given type, and if not found creates and registers an instance using the given factory.</summary>
        /// <param name="key">The type-key (either the Type of the singleton, or an interface).</param>
        /// <param name="create">Factory used to create the new instance if the singleton has not yet been registered.</param>
        public object GetOrCreateSingleton(Type key, Func create)
        {
            // Return the singleton if it's already been registered.
            object instance = GetSingleton(key);
            if (instance != null) return instance;

            // Create an instance of the singleton and register it.
            instance = create();
            RegisterSingleton(key, instance);

            // Finish up.
            return instance;
        }

        /// <summary>Registers the given object as a singleton within the container (replacing any existing instance).</summary>
        /// <param name="key">The type-key (either the Type of the singleton, or an interface).</param>
        /// <param name="instance">The instance.</param>
        public void RegisterSingleton(Type key, object instance)
        {
            // Setup initial conditions.
            if (key == null) throw new Exception("Singleton key cannot be null");
            if (instance == null) throw new Exception("Singleton instance cannot be null");

            // Don't register the same instance twice.
            UnregisterSingleton(key);

            // Create the wrapper and store it.
            DiInstanceWrapper wrapper = new DiInstanceWrapper(key, instance);
            singletons.Add(wrapper);
        }

        /// <summary>Removes the specified singleton from the container.</summary>
        /// <param name="key">The type-key (either the Type of the singleton, or an interface).</param>
        /// <returns>True if an existing singleton was unregistered, or False if there was not matching singleton to unregister.</returns>
        public bool UnregisterSingleton(Type key)
        {
            DiInstanceWrapper wrapper = FromKey(key);
            if (wrapper == null) return false;
            wrapper.Dispose();
            singletons.Remove(wrapper);
            return true;
        }

        /// <summary>Determines whether the a singleton with the given key exists within the container.</summary>
        /// <param name="key">The type-key (either the Type of the singleton, or an interface).</param>
        public bool ContainsSingleton(Type key)
        {
            return FromKey(key) != null;
        }
        #endregion

        #region Internal
        private DiInstanceWrapper FromKey(Type key)
        {
            if (key == null) return null;
            return Helper.Collection.First(singletons, delegate(object o)
                                                           {
                                                               return ((DiInstanceWrapper) o).Key == key;
                                                           }) as DiInstanceWrapper;
        }
        #endregion
    }

    internal class DiInstanceWrapper : IDisposable
    {
        #region Head
        private Type key;
        private object instance;

        public DiInstanceWrapper(Type key, object instance)
        {
            this.key = key;
            this.instance = instance;
        }

        public void Dispose()
        {
            key = null;
            instance = null;
        }
        #endregion

        #region Properties
        public Type Key { get { return key; } }
        public object Instance { get { return instance; } }
        #endregion
    }
}
