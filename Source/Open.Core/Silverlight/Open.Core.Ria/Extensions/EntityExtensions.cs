﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Client;
using System.Linq;
using System.Linq.Expressions;
using Open.Core.Common;

namespace Open.Core.Ria
{
    public static partial class EntityExtensions
    {
        #region Methods : Validation
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

        /// <summary>Determines whether the entity has any validation errors for the specified property.</summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="entity">The entity to examine.</param>
        /// <param name="property">
        ///    An expression that represents the property
        ///    to examine(for example 'n => n.PropertyName'.)
        /// </param>
        public static bool HasValidationError<T>(this Entity entity, Expression<Func<T, object>> property)
        {
            // Setup initial conditions.
            if (entity == null) throw new ArgumentNullException("entity");
            if (property == null) throw new ArgumentNullException("property");

            // Check if any errors have been assigned to the property.
            var propertyName = property.GetPropertyName();
            return entity.ValidationErrors.FirstOrDefault(m => m.MemberNames.Contains(propertyName)) != null;
        }        
        #endregion

        #region Methods : Collection
        /// <summary>Removes all items within the entity set.</summary>
        /// <typeparam name="T">The type of entity.</typeparam>
        /// <param name="collection">The collection to clear.</param>
        public static void RemoveAll<T>(this EntitySet<T> collection) where T : Entity
        {
            if (collection == null) throw new ArgumentNullException("collection");
            foreach (var entity in collection.ToArray())
            {
                collection.Remove(entity);
            }
        }
        #endregion
    }
}
 