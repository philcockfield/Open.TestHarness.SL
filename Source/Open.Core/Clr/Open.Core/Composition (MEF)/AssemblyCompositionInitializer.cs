using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Reflection;

namespace Open.Core.Composition
{
    /// <summary>Provides 'SatisfyImports' functionality for an assembly catalog.</summary>
    public static class AssemblyCompositionInitializer
    {
        #region Head
        private readonly static List<Assembly> assemblies = new List<Assembly>();
        private static CompositionContainer container;
        private static AggregateCatalog aggregateCatalog;

        #endregion

        #region Properties
        /// <summary>Gets the composition container (null if not initialized).</summary>
        public static CompositionContainer Container
        {
            get { return container ?? (container = new CompositionContainer(AggregateCatalog)); }
        }

        private static AggregateCatalog AggregateCatalog
        {
            get { return aggregateCatalog ?? (aggregateCatalog = new AggregateCatalog()); }
        }

        /// <summary>Gets the collection of assemblies that have been added to the container.</summary>
        public static IEnumerable<Assembly> Assemblies { get { return assemblies; } }
        #endregion

        #region Methods
        /// <summary>Registers the the assembly of the given Type within the composition container.</summary>
        /// <typeparam name="T">The representative Type within the assembly to register.</typeparam>
        public static void RegisterAssembly<T>()
        {
            RegisterAssembly(typeof(T).Assembly);
        }

        /// <summary>Registers the given assembly(s) within the composition container.</summary>
        /// <param name="assembly">One or more assemblies to register.</param>
        public static void RegisterAssembly(params Assembly[] assembly)
        {
            if (assembly == null) return;
            foreach (var item in assembly)
            {
                ProcessAssembly(item);
            }
        }

        /// <summary>Satifies imports on the given instance.</summary>
        /// <param name="instance">The instance to satisfy.</param>
        /// <param name="referencedAssemblies">Collection of referenced assemblies that contain the parts required to resolve.</param>
        public static void SatisfyImports(object instance, params Assembly[] referencedAssemblies)
        {
            // Setup initial conditions.
            if (instance == null) return;

            lock (Container)
            {
                // Ensure the calling assembly has been added to the catalog.
                var assembly = Assembly.GetCallingAssembly();
                ProcessAssembly(assembly);

                // Ensure referenced assemblies have been added to the catalog.
                RegisterAssembly(referencedAssemblies);

                // Compose.
                Container.ComposeParts(instance);
            }
        }

        /// <summary>Clears the composition container and resets it to it's initial state.</summary>
        /// <remarks>Used primarily for testing.</remarks>
        public static void Reset()
        {
            if (container != null)
            {
                container.Dispose();
                container = null;
            }

            if (aggregateCatalog != null)
            {
                aggregateCatalog.Dispose();
                aggregateCatalog = null;
            }

            assemblies.Clear();
        }
        #endregion

        #region Internal
        private static void ProcessAssembly(Assembly assembly)
        {
            // Setup initial conditions.
            if (assembly == null) return;
            if (Assemblies.Contains(assembly)) return;

            // Add the given assembly.
            var assemblyCatalog = new AssemblyCatalog(assembly);
            lock (AggregateCatalog)
            {
                lock (assemblies)
                {
                    AggregateCatalog.Catalogs.Add(assemblyCatalog);
                    assemblies.Add(assembly);
                }
            }
        }
        #endregion
    }
}
