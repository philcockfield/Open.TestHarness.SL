using System;
using System.Collections;

namespace Open.Core.Test.UnitTests
{
    public class CollectionHelperUnitTest
    {

        public void ShouldNotReturnLastItemFromNullOrEmpty()
        {
            Array array = new string[] { };
            ArrayList list = new ArrayList();

            Should.BeNull(Helper.Collection.Last(null));
            Should.BeNull(Helper.Collection.Last(array));
            Should.BeNull(Helper.Collection.Last(list));
        }

        public void ShouldReturnLastItemFromArray()
        {
            Array array = new string[] { "one", "two", "three" };
            Should.Equal(Helper.Collection.Last(array), "three");
        }

        public void ShouldReturnLastItemFromArrayList()
        {
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Should.Equal(Helper.Collection.Last(list), 3);
        }

        public void ShouldReturnLastItemFromEnumerable()
        {
            SampleEnumerator enumerable = new SampleEnumerator();
            Should.Equal(Helper.Collection.Last(enumerable), "c");
        }
    }

    public class SampleEnumerator : IEnumerable
    {
        private readonly ArrayList list = new ArrayList();
        public SampleEnumerator()
        {
            list.Add("a");
            list.Add("b");
            list.Add("c");
        }
        public IEnumerator GetEnumerator() { return list.GetEnumerator(); }
    }
}
