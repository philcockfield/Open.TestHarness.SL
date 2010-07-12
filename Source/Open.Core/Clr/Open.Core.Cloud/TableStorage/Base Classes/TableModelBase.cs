using System;

namespace Open.Core.Cloud.TableStorage
{
    public abstract class TableModelBase<TModel, TBackingEntity> where TBackingEntity : ITableServiceEntity
    {
        #region Head
        /// <summary>Constructor.</summary>
        protected TableModelBase(TBackingEntity entity) : this(new TablePropertyManager<TModel, TBackingEntity>(entity)) { }

        /// <summary>Constructor.</summary>
        /// <param name="propertyManager">The property manager to use.</param>
        protected TableModelBase(TablePropertyManager<TModel, TBackingEntity> propertyManager)
        {
            if (propertyManager == null) throw new ArgumentNullException("propertyManager");
            Property = propertyManager;
        }
        #endregion

        #region Properties
        /// <summary>Gets the Property manager for the entity.</summary>
        protected TablePropertyManager<TModel, TBackingEntity> Property { get; private set; }
        #endregion

    }
}
