using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Base_Classes
{
    [TestClass]
    public class ParserBaseTest
    {
        #region Head
        private string sampleCsv;


        [TestInitialize]
        public void TestSetup()
        {
            var reader = new StreamReader(GetStream());
            sampleCsv = reader.ReadToEnd();
        }

        private static Stream GetStream()
        {
            return "/Core/Base Classes/Sample Data/ParseSample.csv".LoadEmbeddedResource();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldLoadSampleCsv()
        {
            sampleCsv.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldExposeRawText()
        {
            var parser = new Mock(sampleCsv);
            parser.RawText.ShouldBe(sampleCsv);
            parser = new Mock("  ");
            parser.RawText.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldConstructFromStream()
        {
            var parser = new Mock(GetStream());
            parser.RawText.ShouldBe(sampleCsv);
        }

        [TestMethod]
        public void ShouldThrowWhenNullStreamIsPassedToConstructor()
        {
            Should.Throw<ArgumentNullException>(() => new Mock((Stream)null));
        }

        [TestMethod]
        public void ShouldGetLinesCollection()
        {
            var parser = new Mock(GetStream());
            parser.Lines.Count().ShouldBe(3);

            parser = new Mock("");
            parser.Lines.Count().ShouldBe(0);
        }


        [TestMethod]
        public void ShouldReturnEmptyJobCollectionWhenNoContentToParse()
        {
            var parser = new Mock("");
            parser.Models.Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHaveSameNumberOfModelsAsLines()
        {
            var parser = new Mock(GetStream());
            parser.Models.Count().ShouldBe(parser.Lines.Count());
        }

        [TestMethod]
        public void ShouldParseModelsFromFile()
        {
            var parser = new Mock(GetStream());
            parser.Models.ElementAt(0).Name.ShouldBe("Fred");
            parser.Models.ElementAt(1).Name.ShouldBe(null);
            parser.Models.ElementAt(2).Name.ShouldBe("Jane");

            parser.Models.ElementAt(0).Value.ShouldBe("One");
            parser.Models.ElementAt(1).Value.ShouldBe("Two");
            parser.Models.ElementAt(2).Value.ShouldBe(null);
        }
        #endregion

        public class Mock : ParserBase<MyModel>
        {
            public Mock(Stream stream) : base(stream){}
            public Mock(string rawText) : base(rawText){}

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
