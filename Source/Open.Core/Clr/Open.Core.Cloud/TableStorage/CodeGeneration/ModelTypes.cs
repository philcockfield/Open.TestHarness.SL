using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Open.Core.Cloud.TableStorage.CodeGeneration
{
    /// <summary>Represents a collection of models.</summary>
    public class ModelTypes
    {
        #region Head
        private readonly List<Type> types = new List<Type>();
        #endregion

        #region Properties
        /// <summary>Gets the collection of models the template is building code for.</summary>
        public IEnumerable<Type> Types { get { return types; } }
        #endregion

        #region Methods
        /// <summary>Adds a model type to include in code generation.</summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        public void Add<TModel>() where TModel : TableModelBase
        {
            Add(typeof(TModel));
        }
        private void Add(Type type)
        {
            if (!types.Contains(type)) types.Add(type);
        }

        /// <summary>Adds all TableModelBase types from the given assembly.</summary>
        /// <param name="assembly">The assembly to look within.</param>
        public void Add(Assembly assembly)
        {
            if (assembly == null) throw new ArgumentNullException("assembly");
            foreach (var type in assembly.GetTableModelTypes())
            {
                Add(type);
            }
        }

        /// <summary>Removes a model type from code generation.</summary>
        /// <typeparam name="TModel">The type of the model</typeparam>
        public void Remove<TModel>() where TModel : TableModelBase
        {
            var type = typeof(TModel);
            types.Remove(type);
        }        
        #endregion
    }
}
