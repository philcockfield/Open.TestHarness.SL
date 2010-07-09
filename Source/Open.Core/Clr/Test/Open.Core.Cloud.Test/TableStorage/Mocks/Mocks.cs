using System;
using Open.Core.Cloud.TableStorage;
using Open.Core.Cloud.Test.TableStorage.CodeGeneration.Generated;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [PersistClass]
    public class MockEntityA
    {
        #region Head
        public MockEntityA() { }
        public MockEntityA(MockEntityATableEntity backingEntity)
        {
            Property = new TableEntityPropertyManager<MockEntityA, MockEntityATableEntity>(backingEntity);
        }
        #endregion

        #region Properties
        public TableEntityPropertyManager<MockEntityA, MockEntityATableEntity> Property { get; private set; }

        // TODO - Don't generate backing field for this.  Rather generate field for "map to" value.
        //[PersistProperty(IsRowKey = true, MapTo = "RowKey")]
        //public string Id
        //{
        //    get { return Property.GetValue<string>(m => m.Id); }
        //    set { Property.SetValue(m => m.Id, value); }
        //}

        [PersistProperty]
        public string Text
        {
            get { return Property.GetValue<string>(m => m.Text); }
            set { Property.SetValue(m => m.Text, value); }
        }

        [PersistProperty]
        public int Number
        {
            get { return Property.GetValue<int>(m => m.Number); }
            set { Property.SetValue(m => m.Number, value); }
        }

        public string NotPersisted { get; set; }
        #endregion
    }

    [PersistClass]
    public class MockEntityB
    {
        [PersistProperty]
        public DateTime Date { get; set; }
    }

    [PersistClass]
    public class NoPersistableValues
    {
        public string Text { get; set; }
    }

    public class NonPersistableClass
    {
        [PersistProperty]
        public string Text { get; set; }
    }
}
