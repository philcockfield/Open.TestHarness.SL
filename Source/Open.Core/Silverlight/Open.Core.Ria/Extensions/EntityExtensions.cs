﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel.DomainServices.Client;
using Open.Core.Common;

namespace Open.Core.Ria
{
    public static class EntityExtensions
    {
        /// <summary>Adds a validation-error to the given entity.</summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity to add to.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="properties">
        ///    An expression that represents the property(s)
        ///    to assign the error to (for example 'n => n.PropertyName'.)
        /// </param>
        public static void AddValidationError<T>(this Entity entity, string errorMessage, params Expression<Func<T, object>>[] properties)
        {
            // Setup initial conditions.
            if (entity == null) throw new ArgumentNullException("entity");
            if (properties == null || properties.Count() == 0) throw new ArgumentNullException("properties");

            // Prepare the errors.
            var propertyNames = properties.Select(expression => expression.GetPropertyName()).ToArray().Distinct();
            var error = new ValidationResult(errorMessage, propertyNames);

            // Add to the entity.
            entity.ValidationErrors.Add(error);
        }

        /// <summary>Removes a validation-error from the given entity.</summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity to remove from.</param>
        /// <param name="properties">
        ///    An expression that represents the property(s)
        ///    to assign the error to (for example 'n => n.PropertyName'.)
        /// </param>
        public static void RemoveValidationError<T>(this Entity entity, params Expression<Func<T, object>>[] properties)
        {
            // Setup initial conditions.
            if (entity == null) throw new ArgumentNullException("entity");
            if (properties == null || properties.Count() == 0) throw new ArgumentNullException("properties");
            var errors = entity.ValidationErrors;
            if (errors.Count == 0) return;

            // Prepare the errors.
            var propertyNames = properties.Select(expression => expression.GetPropertyName()).ToArray().Distinct();
            foreach (var error in entity.ValidationErrors.ToList())
            {
                if (error.MemberNames.All(m => propertyNames.Contains(m)))
                {
                    entity.ValidationErrors.Remove(error);
                }
            }
        }

        /// <summary>Adds or removes a validation error from the entity.</summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity to add or remove from.</param>
        /// <param name="hasError">Delegate that determines whether to add or remove.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="properties">
        ///    An expression that represents the property(s)
        ///    to assign the error to (for example 'n => n.PropertyName'.)
        /// </param>
        public static void AddOrRemoveValidationError<T>(this Entity entity, Func<bool> hasError, string errorMessage, params Expression<Func<T, object>>[] properties)
        {
            if (hasError == null) throw new ArgumentNullException("hasError");
            if (hasError())
            {
                AddValidationError(entity, errorMessage, properties);
            }
            else
            {
                RemoveValidationError(entity, properties);
            }
        }

        /// <summary>Fires the 'PropertyChanged' event for the specified properties on the Entity.</summary>
        /// <typeparam name="T">The tpe of the entity.</typeparam>
        /// <param name="entity">The entity to fire from.</param>
        /// <param name="onPropertyChangedMethod">Pointer to the 'OnPropertyChanged' method on the entity.</param>
        /// <param name="properties">
        ///    The collection of expressions that represent the properties 
        ///    that have changed (for example 'n => n.PropertyName'.)
        /// </param>
        public static void FirePropertyChanged<T>(
            this Entity entity, 
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
 