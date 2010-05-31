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
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Open.Core.Common
{
    public static partial class LinqExtensions
    {
        /// <summary>Extracts the PropertyInfo that is referenced in the given from lambda expression.</summary>
        /// <typeparam name="T">The type of object that exposes the property.</typeparam>
        /// <param name="expression">The property expression to evaluate (for example 'n => n.PropertyName').</param>
        /// <returns>The name of the property, or null if a property could not be derived from the expression.</returns>
        /// <exception cref="ArgumentException">Is thrown if an expression that does not take the property form is passed.</exception>
        public static PropertyInfo GetPropertyInfo<T>(this Expression<Func<T, object>> expression)
        {
            // Setup initial conditions.
            if (expression == null) throw new ArgumentNullException("expression");

            // Retrieve the expression.
            var lambda = expression as LambdaExpression;
            MemberExpression memberExpression;
            if (lambda.Body is UnaryExpression)
            {
                var unaryExpression = lambda.Body as UnaryExpression;
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = lambda.Body as MemberExpression;
            }
            if (memberExpression == null) throw new ArgumentException("Please provide a lambda expression like 'n => n.PropertyName'", "expression");

            // Extract the name from the expression.
            return memberExpression.Member as PropertyInfo;
        }


        /// <summary>Extracts the property name that is referenced in the given from lambda expression.</summary>
        /// <typeparam name="T">The type of object that exposes the property.</typeparam>
        /// <param name="expression">The property expression to evaluate (for example 'n => n.PropertyName').</param>
        /// <returns>The name of the property, or null if a property could not be derived from the expression.</returns>
        /// <exception cref="ArgumentException">Is thrown if an expression that does not take the property form is passed.</exception>
        public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            var propertyInfo = expression.GetPropertyInfo();
            return propertyInfo == null ? null : propertyInfo.Name;
        }

        /// <summary>Determines whether the specified property-changed event args match the given property.</summary>
        /// <typeparam name="T">The type of object that exposes the property.</typeparam>
        /// <param name="e">The event args to examine.</param>
        /// <param name="expression">The property expression to evaluate (for example 'n => n.PropertyName').</param>
        public static bool IsProperty<T>(this PropertyChangedEventArgs e, Expression<Func<T, object>> expression)
        {
            // Setup initial conditions.
            if (e == null) throw new ArgumentNullException("e");
            if (expression == null) throw new ArgumentNullException("expression");

            // Perform the comparison.
            return expression.GetPropertyName() == e.PropertyName;
        }


        /// <summary>Converts a collection of property lamda expressions to a list of property name strings.</summary>
        /// <typeparam name="T">The type of object that exposes the property.</typeparam>
        /// <param name="properties">The collection of expressions to convert (for example 'n => n.PropertyName').</param>
        public static IEnumerable<string> ToList<T>(this IEnumerable<Expression<Func<T, object>>> properties)
        {
            if (properties == null) return new string[] {};
            var list = new List<string>();
            foreach (var property in properties)
            {
                list.Add(property.GetPropertyName());
            }
            return list;
        }

        /// <summary>Invokes the given action if the value matches the given property expression.</summary>
        /// <typeparam name="T">The type of object that exposes the property.</typeparam>
        /// <param name="propertyName">The name of the property to match.</param>
        /// <param name="propertyToMatch">The property expression to evaluate (for example 'n => n.PropertyName').</param>
        /// <param name="action">The action to invoke.</param>
        /// <returns>True if the action was invoked (property matched), otherwise False.</returns>
        /// <remarks>Useful on the OnPropertyChanged override in an EntityFramework Entity.</remarks>
        public static bool InvokeIf<T>(this string propertyName, Expression<Func<T, object>> propertyToMatch, Action action)
        {
            if (action == null) return false;
            if (propertyToMatch == null) return false;
            if (propertyToMatch.GetPropertyName() != propertyName) return false;
            action();
            return true;
        }

        /// <summary>Invokes the given action if the value matches the given property expression.</summary>
        /// <typeparam name="T">The type of object that exposes the property.</typeparam>
        /// <param name="propertyName">The name of the property to match.</param>
        /// <param name="propertiesToMatch">The property expression to evaluate (for example 'n => n.PropertyName').</param>
        /// <param name="action">The action to invoke.</param>
        /// <returns>True if the action was invoked (property matched), otherwise False.</returns>
        /// <remarks>Useful on the OnPropertyChanged override in an EntityFramework Entity.</remarks>
        public static bool InvokeIf<T>(this string propertyName, Action action, params Expression<Func<T, object>>[] propertiesToMatch)
        {
            foreach (var propertyToMatch in propertiesToMatch)
            {
                if (propertyName.InvokeIf(propertyToMatch, action)) return true;
            }
            return false;
        }

        /// <summary>Applys a Skip and Take operation on a query (ensuring the skip/take values are within bounds).</summary>
        /// <typeparam name="T">The type of object within the query.</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="skip">The number of items to skip.</param>
        /// <param name="take">The number of items to take (page size).</param>
        /// <returns></returns>
        public static IQueryable<T> TakePage<T>(this IQueryable<T> query, int skip, int take)
        {
            if (query == null) throw new ArgumentNullException("query");
            skip = skip.WithinBounds(0, Int32.MaxValue);
            take = take.WithinBounds(0, Int32.MaxValue);
            return query.Skip(skip).Take(take);
        }
    }
}
