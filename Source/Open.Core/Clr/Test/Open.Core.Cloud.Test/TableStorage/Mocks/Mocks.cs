using System;
using Open.Core.Cloud.TableStorage;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    [PersistClass]
    public class MockEntityA
    {
        [PersistProperty]
        public string Text { get; set; }

        [PersistProperty]
        public int Number { get; private set; }

        public string NotPersisted { get; set; }
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
