using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Open.Core.Common;

namespace Open.TestHarness
{
    public static class ReflectionExtensions
    {
        /// <summary>Gets the collection of Types decorated with the [ViewTestClass] method.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        public static IEnumerable<Type> GetViewTestClasses(this Assembly assembly)
        {
            return from type in assembly.GetTypes()
                   where type.IsPublic && type.GetCustomAttributes(typeof(ViewTestClassAttribute), false).FirstOrDefault() != null
                   orderby type.Name
                   select type;
        }

        /// <summary>Gets the collection of Types decorated with the [ViewTestClass] method.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        /// <param name="className">
        ///     The class-name filter.  If a single class name, all classes that match (in any namespace) will be returned.
        ///     If a fully qualified class-name (with namespace) is provided only that class will be retrieved.
        /// </param>
        /// <returns></returns>
        public static IEnumerable<Type> GetViewTestClasses(this Assembly assembly, string className)
        {
            // Setup initial conditions.
            if (className.IsNullOrEmpty(true)) return assembly.GetViewTestClasses();
            className = className.ToLower();
            var isFullyQualified = className.Contains(".");
            
            // Perform query.
            return from type in assembly.GetViewTestClasses()
                           where isFullyQualified 
                                               ? type.FullName.ToLower() == className
                                               : type.Name.ToLower() == className
                           orderby type.Name
                           select type;
        }
    }
}
