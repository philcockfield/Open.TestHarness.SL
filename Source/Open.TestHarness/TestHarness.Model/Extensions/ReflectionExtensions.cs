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
        /// <returns>The collection of matching classes (or empty list if no matches found).</returns>
        public static IEnumerable<Type> GetViewTestClasses(this Assembly assembly)
        {
            return from type in assembly.GetTypes()
                   where type.IsPublic && type.HasAttribute<ViewTestClassAttribute>()
                   orderby type.Name
                   select type;
        }

        /// <summary>Gets the collection of Types decorated with the [ViewTestClass] method.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        /// <param name="className">
        ///     The class-name filter.  If a simple class name, all classes that match (in any namespace) will be returned.
        ///     If a fully qualified class-name (with namespace) is provided only that class will be retrieved.
        /// </param>
        /// <returns>The collection of matching classes (or empty list if no matches found).</returns>
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

        /// <summary>Gets the collection of Method's (decorated with the [ViewTest] attribute) with the corresponding method name.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        /// <param name="methodName">
        ///     The name of the method (not case sensitive).<BR/>
        ///     If only the method name is specified, all matching methods (from any class in any namespace) will be returned.<BR/>
        ///     If a fully-qualified method name (with namespace) is provided only that method will be retrieved.<BR/>
        ///     Passing null returns all [ViewTest] methods within the assembly.
        /// </param>
        /// <returns>The collection of matching methods (or empty list if no matches found).</returns>
        public static IEnumerable<MethodInfo> GetViewTestMethods(this Assembly assembly, string methodName = null)
        {
            // Setup initial conditions.
            if (assembly == null) return new List<MethodInfo>();

            // Process method name.
            methodName = methodName.IsNullOrEmpty(true) 
                                        ? null 
                                        : methodName.ToLower();
            var isFullyQualified = methodName == null ? false :  methodName.Contains(".");

            // Filter on matching [ViewTest] methods.
            Func<MethodInfo, bool> isMatch = method =>
                             {
                                 if (!method.HasAttribute<ViewTestAttribute>()) return false;
                                 if (methodName == null) return true;
                                 return isFullyQualified
                                            ? methodName == string.Format("{0}.{1}", method.DeclaringType.FullName, method.Name).ToLower()
                                            : methodName == method.Name.ToLower();
                             };
            return (from c in assembly.GetViewTestClasses()
                    from m in c.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public)
                   where isMatch(m)
                   select m).Distinct();
        }


        //TEMP 
        ///// <summary>Get test-methods that have either been specifically tagged, or are in a class that has been tagged with the given tag.</summary>
        ///// <param name="tags"></param>
        ///// <returns></returns>
        //public static IEnumerable<MethodInfo> GetTaggedViewTestMethods(params string[] tags)
        //{
            
        //}

    }
}
