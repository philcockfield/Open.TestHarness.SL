using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.Test.UnitTests.Core.Base_Classes
{
    [TestClass]
    public class ParserBaseTest
    {

        [TestMethod]
        public void ShouldParseModels()
        {
            var text = string.Format("Fred\tOne\rGorden\tTwo");
            var parser = new MockParser(text);

            parser.Models.ElementAt(0).Name.ShouldBe("Fred");
            parser.Models.ElementAt(1).Name.ShouldBe("Gorden");

            parser.Models.ElementAt(0).Value.ShouldBe("One");
            parser.Models.ElementAt(1).Value.ShouldBe("Two");
        }

        public class MockParser : ParserBase<MyModel>
        {
            public MockParser(Stream stream) : base(stream) { }
            public MockParser(string rawText) : base(rawText) { }

            protected override MyModel CreateModel(string[] fields)
            {
                return new MyModel
                                    {
                                        Name = fields.ElementAtOrDefault(0).AsNullWhenEmpty(),
                                        Value = fields.ElementAtOrDefault(1).AsNullWhenEmpty(),
                                    };
            }
        }

        public class MyModel
        {
            public string Name { get; set; }
            public string Value { get; set; }
        }


    }
}
