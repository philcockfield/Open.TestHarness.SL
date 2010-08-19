using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

// Source:  http://blogs.captechconsulting.com/blog/kevin-hazzard/fluent-xml-parsing-using-cs-dynamic-type-part-1
//              http://blogs.captechconsulting.com/blog/kevin-hazzard/fluent-xml-parsing-using-cs-dynamic-type-part-2

namespace Open.Core.Common.Test.Core.Helper_Classes
{
    [TestClass]
    public class DynamicXmlTest
    {
        #region Head
        private XDocument document;
        private DynamicXml wrapper;
        private dynamic DynamicRoot { get { return wrapper as dynamic; } }

        [TestInitialize]
        public void TestSetup()
        {
            document= XDocument.Parse(SampleXml);
            wrapper = new DynamicXml(SampleXml);
        }


        [TestCleanup]
        public void TestCleanup()
        {
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldParseSampleXml()
        {
            document.Root.ShouldNotBe(null);
            document.Root.Elements().Count().ShouldBe(3);
        }

        [TestMethod]
        public void ShouldHaveSingleRootNode()
        {
            var count = wrapper.Cast<object>().Count();
            count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldEnumerateBooks()
        {
            var count = 0;
            foreach (dynamic item in DynamicRoot.book)
            {
                count++;
            }
            count.ShouldBe(3);
        }

        [TestMethod]
        public void ShouldReturnCountOfBooks()
        {
            int count = DynamicRoot.book.Count;
            count.ShouldBe(3);
        }

        [TestMethod]
        public void ShouldGetSecondBook()
        {
            var book = DynamicRoot.book[1];
            string title = book.title.Value;
            title.ShouldBe("Skydiving on a Budget");
        }


        [TestMethod]
        public void ShouldTraverseHierarchy()
        {
            string firstName = DynamicRoot.book[0].authors[0].author.name.first.Value;
            firstName.ShouldBe("Mortimer");
        }

        [TestMethod]
        public void ShouldReadAttribute()
        {
            var value = DynamicRoot.pubdate.Value as string;
            value.ShouldBe("2009-05-20");
        }

        [TestMethod]
        public void ShouldReturnEmptyStringWhenNodeNotFound()
        {
            var node = DynamicRoot.noNode;
            string value= DynamicRoot.noNode.Value;
            value.ShouldBe(string.Empty);
        }

        [TestMethod]
        public void ShouldWriteValue()
        {
            DynamicRoot.noNode.Value = "Foo";
            string value = DynamicRoot.noNode.Value;
            value.ShouldBe("Foo");
        }

        [TestMethod]
        public void ShouldAllowWritingOfNullValue()
        {
            DynamicRoot.noNode.Value = null;
            string value = DynamicRoot.noNode.Value;
            value.ShouldBe(string.Empty);
        }
        #endregion

        #region Sample XML
        private const string SampleXml = @"
                     <books pubdate='2009-05-20'>
                          <book price='45.99' title='Open Heart Surgery for Dummies'>
                            <id isbn10='4389880339'/>
                            <authors>
                              <author>
                                <name>
                                  <first>Mortimer</first>
                                  <middle>Q.</middle>
                                  <last>Snerdly</last>
                                </name>
                                <email address='mort@surgery.com'/>
                              </author>
                            </authors>
                          </book>
                          <book price='32.75' title='Skydiving on a Budget'>
                            <id isbn='2129034454'/>
                            <authors>
                              <author>
                                <name>
                                  <first>Trudy</first>
                                  <middle>L.</middle>
                                  <last>Freefall</last>
                                </name>
                                <email address='tfreefall@jump.com'/>
                              </author>
                              <author>
                                <name>
                                  <first>Bernard</first>
                                  <middle>M.</middle>
                                  <last>Fallson</last>
                                </name>
                                <email address='bernie@airborne.com'/>
                              </author>
                            </authors>
                          </book>
                          <book price='22.40' title='How to Dismantle a Bomb'>
                            <authors>
                              <author>
                                <name>
                                  <first>Bono</first>
                                  <middle/>
                                  <last>Vox</last>
                                </name>
                                <email address='bono@u2.com'/>
                              </author>
                            </authors>
                          </book>
                    </books>";        
        #endregion
    }
}
