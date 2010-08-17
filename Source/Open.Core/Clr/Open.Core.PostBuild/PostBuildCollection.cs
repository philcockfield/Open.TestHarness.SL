using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Open.Core.PostBuild
{
    /// <summary>Represents the collection of PostBuild classes to execute.</summary>
    internal class PostBuildCollection
    {

        #region Head
        private readonly string tag;
        private readonly DirectoryInfo folder;

        /// <summary>Constructor.</summary>
        /// <param name="tag">The tag to match.  If not specified all decorated classes are matched (not case sensitive).</param>
        /// <param name="path">The path to the folder containing the DLL's to look for.</param>
        public PostBuildCollection(string tag, string path)
        {
            // Setup initial conditions.
            this.tag = TrimQuotes(tag);
            if (this.tag != null) this.tag = this.tag.ToLower();
            path = TrimQuotes(path);
            Classes = new List<Type>();

            // Retrieve the folder.
            if (path == null) path = Directory.GetCurrentDirectory();
            folder = new DirectoryInfo(path);
            if (!folder.Exists) return;

            // Finish up.
            Classes = GetClasses();
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of classes decorated with the [OnPostBuild] attribute that have parameterless constructors.</summary>
        public IEnumerable<Type> Classes { get; private set; }
        #endregion

        #region Internal
        private IEnumerable<Type> GetClasses()
        {
            // Setup initial conditions.
            var list = new List<Type>();

            // Enumerate the assemblies.
            foreach (var assembly in GetAssemblies())
            {
                var postBuildTypes = from n in assembly.GetTypes()
                                     where IsPostBuildType(n)
                                     select n;
                list.AddRange(postBuildTypes);
            }

            // Finish up.
            return list;
        }

        private bool IsPostBuildType(Type type)
        {
            // Ensure there is a parameterless constructor.
            var constructor = type.GetConstructor(new Type[]{});
            if (constructor == null) return false;

            // Ensure the class is decorated with the [OnPostBuild] attribute.
            var attribute = type.GetCustomAttributes(typeof (OnPostBuildAttribute), true).FirstOrDefault() as OnPostBuildAttribute;
            if (attribute == null) return false;

            // Ensure it's tag matches.
            if (tag == null) return true;
            var typeHasTag = !string.IsNullOrEmpty(attribute.Tag) && !string.IsNullOrWhiteSpace(attribute.Tag);
            if (!typeHasTag)
            {
                return false;
            }
            else
            {
                if (attribute.Tag.ToLower() != tag) return false;
            }

            // Finish up.
            return true;
        }

        private IEnumerable<Assembly> GetAssemblies()
        {
            var list = new List<Assembly>();
            foreach (var file in folder.GetFiles("*.dll"))
            {
                if (file.Name.StartsWith("System.")) continue;
                if (file.Name.StartsWith("Microsoft.")) continue;
                list.Add(Assembly.LoadFrom(file.FullName));
            }
            return list;
        }

        private static string TrimQuotes(string value)
        {
            if (string.IsNullOrEmpty(value)) value = null;
            if (string.IsNullOrWhiteSpace(value)) value = null;
            return value == null ? null : value.Trim("\"".ToCharArray());
        }
        #endregion
    }
}
