using System;
using Open.Core.Cloud.TableStorage;

namespace Open.Core.Cloud.Test.TableStorage.CodeGeneration
{
    public class MockEntityA : TableModelBase
    {
        [Persist]
        public string Text { get; set; }

        [Persist]
        public int Number { get; private set; }

        public string NotPersisted { get; set; }
    }

    public class MockEntityB : TableModelBase
    {
        [Persist]
        public DateTime Date { get; set; }
    }

}
