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

            Assert.That(Helper.Collection.Last(null)).IsNull();
            Assert.That(Helper.Collection.Last(array)).IsNull();
            Assert.That(Helper.Collection.Last(list)).IsNull();
        }

        public void ShouldReturnLastItemFromArray()
        {
            Array array = new string[] { "one", "two", "three" };
            Assert.That(Helper.Collection.Last(array)).Is("three");
        }

        public void ShouldReturnLastItemFromArrayList()
        {
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add(3);
            Assert.That(Helper.Collection.Last(list)).Is(3);
        }

        public void ShouldReturnLastItemFromEnumerable()
        {
            SampleEnumerator enumerable = new SampleEnumerator();
            Assert.That(Helper.Collection.Last(enumerable)).Is("c");
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
