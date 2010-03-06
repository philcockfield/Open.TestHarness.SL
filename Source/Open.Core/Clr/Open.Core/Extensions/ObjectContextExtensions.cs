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
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Diagnostics.Contracts;
using System.ServiceModel.DomainServices.Hosting;

namespace Open.Core.Common
{
    public static class ObjectContextExtensions
    {

        /// <summary>Inserts an entity into the context.</summary>
        /// <param name="context">The object-context.</param>
        /// <param name="entity">The entity to insert.</param>
        /// <param name="addToCollection">Action adds the entity into the corresponding collection on the context.</param>
        public static void Insert(this ObjectContext context, EntityObject entity, Action addToCollection)
        {
            // Setup initial conditions.
            Contract.Requires(context != null);
            Contract.Requires(entity != null);
            Contract.Requires(addToCollection != null);
            if ((entity.EntityState == EntityState.Added)) return;

            // Perform the insertion.
            if ((entity.EntityState != EntityState.Detached))
            {
                context.ObjectStateManager.ChangeObjectState(entity, EntityState.Added);
            }
            else
            {
                addToCollection();
            }
        }

        /// <summary>Updates an entity within the context.</summary>
        /// <param name="context">The object-context.</param>
        /// <param name="currentEntity">The current entity.</param>
        /// <param name="getOriginalEntity">The original entity (in Ria Service use 'ChangeSet.GetOriginal(currentEntity)').</param>
        public static void Update<T>(this ObjectContext context, ObjectSet<T> collection, EntityObject currentEntity, Func<EntityObject> getOriginalEntity) where T:class
        {
            // Setup initial conditions.
            Contract.Requires(context != null);
            Contract.Requires(currentEntity != null);
            Contract.Requires(collection != null);
            Contract.Requires(getOriginalEntity != null);

            collection.AttachAsModified(currentEntity, getOriginalEntity());

            //// Perform the change.
            //if ((currentEntity.EntityState == EntityState.Detached))
            //{

            //    context.
            //    context.AttachAsModified(currentEntity, getOriginalEntity());
            //}
        }

        /// <summary>Deltes an entity from the context.</summary>
        /// <param name="context">The object-context.</param>
        /// <param name="entity">The entity to delete.</param>
        public static void Delete(this ObjectContext context, EntityObject entity)
        {
            // Setup initial conditions.
            Contract.Requires(context != null);
            Contract.Requires(entity != null);

            // Perform the delete.
            if (entity.EntityState == EntityState.Detached) context.Attach(entity);
            context.DeleteObject(entity);
        }             
    }
}
