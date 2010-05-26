using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Open.Core.Common
{
    public static class EventExtensions
    {
        /// <summary>Fires the 'PropertyChanged' event for the specified properties on the Entity.</summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="model">The model to fire from.</param>
        /// <param name="onPropertyChangedMethod">Pointer to the 'OnPropertyChanged' method on the model.</param>
        /// <param name="properties">
        ///    The collection of expressions that represent the properties 
        ///    that have changed (for example 'n => n.PropertyName'.)
        /// </param>
        public static void FirePropertyChanged<T>(
                                this object model,
                                Action<PropertyChangedEventArgs> onPropertyChangedMethod,
                                params Expression<Func<T, object>>[] properties)
        {
            foreach (var expression in properties)
            {
                onPropertyChangedMethod(new PropertyChangedEventArgs(expression.GetPropertyName()));
            }
        }
    }
}
