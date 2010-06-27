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
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Extensions
{
    [TestClass]
    public class StringExtensionsTest
    {
        [TestMethod]
        public void ShouldDetermineWhenStringIsNullWhenEmpty()
        {
            Assert.IsNull("  ".AsNullWhenEmpty());
            Assert.IsNull(" ".AsNullWhenEmpty());
            Assert.IsNull("".AsNullWhenEmpty());

            Assert.AreEqual("a", "a".AsNullWhenEmpty());
            Assert.AreEqual("   a   ", "   a   ".AsNullWhenEmpty());
        }

        [TestMethod]
        public void ShouldRemoveExtensionFromString()
        {
            "Filename.doc".StripExtension("xls").ShouldBe("Filename.doc");
            "Filename.doc".StripExtension("doc").ShouldBe("Filename");
            "Filename.doc".StripExtension(".doc").ShouldBe("Filename");
            "Filename.doc".StripExtension("....doc").ShouldBe("Filename");

            "Filename.doc".StripExtension(".DOC").ShouldBe("Filename");
            "Filename.doc".StripExtension("DOC").ShouldBe("Filename");
            "Filename.doc".StripExtension("Doc").ShouldBe("Filename");

            "Filename.doc".StripExtension("  ").ShouldBe("Filename.doc");
            "Filename.doc".StripExtension(null as string).ShouldBe("Filename.doc");
        }

        [TestMethod]
        public void ShouldRemoveExtensionFromStringWhenCollectionOfExtensionsPassed()
        {
            "Filename.doc".StripExtension("xls", "psd").ShouldBe("Filename.doc");
            "Filename.doc".StripExtension("doc", "psd").ShouldBe("Filename");
            "Filename.doc".StripExtension(".psd", ".doc").ShouldBe("Filename");
            "Filename.doc".StripExtension("...psd", "....doc").ShouldBe("Filename");

            "Filename.doc".StripExtension("PSd",".DOC").ShouldBe("Filename");
            "Filename.doc".StripExtension("PSD", "DOC").ShouldBe("Filename");
            "Filename.doc".StripExtension("Doc", "Psd").ShouldBe("Filename");

            "Filename.doc".StripExtension("  ", null).ShouldBe("Filename.doc");
            "Filename.doc".StripExtension(null, null).ShouldBe("Filename.doc");
            "Filename.doc".StripExtension("   ", "").ShouldBe("Filename.doc");
            "Filename.doc".StripExtension("   ", " ").ShouldBe("Filename.doc");
        }

        [TestMethod]
        public void ShouldFindAnyUsingContainsAny()
        {
            const string nullValue = null;
            nullValue.ContainsAny("a b", " ").ShouldBe(false);
            nullValue.ContainsAny(null, " ").ShouldBe(false);
            nullValue.ContainsAny(" ", " ").ShouldBe(false);
            "  ".ContainsAny("a b", " ").ShouldBe(false);

            "cat dog".ContainsAny("roger at", " ").ShouldBe(true);
            "cat dog".ContainsAny("roger AT", " ").ShouldBe(true);
            "cat dog".ContainsAny("roger drat", " ").ShouldBe(false);
        }

        [TestMethod]
        public void ShouldFindAllUsingContainsAll()
        {
            const string nullValue = null;
            nullValue.ContainsAll("a b", " ").ShouldBe(false);
            nullValue.ContainsAll(null, " ").ShouldBe(false);
            nullValue.ContainsAll(" ", " ").ShouldBe(false);
            "  ".ContainsAll("a b", " ").ShouldBe(false);

            "cat dog".ContainsAll("roger AT", " ").ShouldBe(false);
            "cat dog".ContainsAll("OG AT", " ").ShouldBe(true);
            "cat dog".ContainsAll("KOG AT", " ").ShouldBe(false);
        }


        [TestMethod]
        public void ShouldFormatWithArguments()
        {
            "one {0} {1}".FormatWith("two", "three").ShouldBe("one two three");
        }

        [TestMethod]
        public void ShouldDetermineIfStringIsNullOrEmpty()
        {
            "abc".IsNullOrEmpty().ShouldBe(false);

            ((string)null).IsNullOrEmpty().ShouldBe(true);
            "".IsNullOrEmpty().ShouldBe(true);
            " ".IsNullOrEmpty().ShouldBe(false);

            "  ".IsNullOrEmpty(true).ShouldBe(true);
            "  ".IsNullOrEmpty(false).ShouldBe(false);
        }

        [TestMethod]
        public void ShouldRemoveStart()
        {
            "HotHouse".RemoveStart("Cat").ShouldBe("HotHouse"); // No change

            "HotHouse".RemoveStart("Hot").ShouldBe("House");
            "HotHouse".RemoveStart("hot").ShouldBe("House");
            "HotHouse".RemoveStart("HOT").ShouldBe("House");

            "HotHouse".RemoveStart("HOT", true).ShouldBe("HotHouse");
        }

        [TestMethod]
        public void ShouldRemoveEnd()
        {
            "HotHouse".RemoveEnd("Cat").ShouldBe("HotHouse"); // No change

            "HotHouse".RemoveEnd("House").ShouldBe("Hot");
            "HotHouse".RemoveEnd("house").ShouldBe("Hot");
            "HotHouse".RemoveEnd("HOUSE").ShouldBe("Hot");

            "HotHouse".RemoveEnd("HOUSE", true).ShouldBe("HotHouse"); 
        }

        [TestMethod]
        public void ShouldConvertAnEnumerableToString()
        {
            new[] { "One", "Two", "Three" }.ToString("; ", e => e.ToLower()).ShouldBe("one; two; three");
            new[] { "One"}.ToString("; ", e => e.ToLower()).ShouldBe("one");
            new[] { "  " }.ToString("; ", e => e).ShouldBe("  ");
            new[] { "" }.ToString("; ", e => e).ShouldBe(string.Empty);
            new string[0].ToString("; ", e => e).ShouldBe(string.Empty);

            new[] { "One", "Two", "Three" }.ToString(e => e.ToLower()).ShouldBe("one, two, three");

            Should.Throw<ArgumentNullException>(() => new[] { "One"}.ToString("; ", null));
        }

        [TestMethod]
        public void ShouldConvertToLinesOfText()
        {
            ((IEnumerable<string> )null).ToLines().ShouldBe(null);
            (new string[] { }).ToLines().ShouldBe(null);

            new[] { "one" }.ToLines().ShouldBe("one");
            new[] { "one", "two" }.ToLines().ShouldBe(string.Format("one\r\ntwo"));
            new[] { "one", "two" }.ToLines().ShouldBe("one\r\ntwo");
        }

        [TestMethod]
        public void ShouldLoadEmbeddedResourceFile()
        {
            "Core/Extensions/Resource File/File.txt".LoadEmbeddedResource(GetType().Assembly).ShouldNotBe(null);
            "Core/Extensions/Resource File/File.txt".LoadEmbeddedResource().ShouldNotBe(null);

            "/Core/Extensions/Resource File/File.txt".LoadEmbeddedResource(GetType().Assembly).ShouldNotBe(null);
            "/Core/Extensions/Resource File/File.txt".LoadEmbeddedResource().ShouldNotBe(null);

            "\\Core\\Extensions\\Resource File\\File.txt".LoadEmbeddedResource().ShouldNotBe(null);
            @"\Core\Extensions\Resource File\File.txt".LoadEmbeddedResource().ShouldNotBe(null);
            "\\Core\\Extensions/Resource File/File.txt".LoadEmbeddedResource().ShouldNotBe(null);

            "".LoadEmbeddedResource().ShouldBe(null);
            " ".LoadEmbeddedResource().ShouldBe(null);
            ((string)null).LoadEmbeddedResource().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldFormatUnderscoresOutOfName()
        {
            "".FormatUnderscores().ShouldBe(String.Empty);
            (null as string).FormatUnderscores().ShouldBe(null);
            "cat".FormatUnderscores().ShouldBe("cat");

            "one_".FormatUnderscores().ShouldBe("one");
            "one_two".FormatUnderscores().ShouldBe("one two");
            "one__two".FormatUnderscores().ShouldBe("one: two");
            "one__two_three__four".FormatUnderscores().ShouldBe("one: two three: four");
        }

        [TestMethod]
        public void ShouldReverseString()
        {
            (null as string).Reverse().ShouldBe(null);

            "".Reverse().ShouldBe("");
            " ".Reverse().ShouldBe(" ");

            "abc".Reverse().ShouldBe("cba");
            "a".Reverse().ShouldBe("a");
        }

        [TestMethod]
        public void ShouldCapitalizeFirstLetterOfSentence()
        {
            "".ToSentenceCase().ShouldBe("");
            ((string)null).ToSentenceCase().ShouldBe(null);

            "abc".ToSentenceCase().ShouldBe("Abc");
            "a".ToSentenceCase().ShouldBe("A");
        }

        [TestMethod]
        public void ShouldConvertToPlural()
        {
            "cat".ToPlural(2, "cats").ShouldBe("cats");
            "cat".ToPlural(0, "cats").ShouldBe("cats");
            "cat".ToPlural(-0, "cats").ShouldBe("cats");
            "cat".ToPlural(-2, "cats").ShouldBe("cats");

            (null as string).ToPlural(2, "cats").ShouldBe("cats");
            "cat".ToPlural(2, null).ShouldBe(null);
        }

        [TestMethod]
        public void ShouldNotConvertToPlural()
        {
            "cat".ToPlural(1, "cats").ShouldBe("cat");
            "cat".ToPlural(-1, "cats").ShouldBe("cat");
        }

        [TestMethod]
        public void ShouldGetFileExtension()
        {
            "".FileExtension().ShouldBe(null);
            "  ".FileExtension().ShouldBe(null);
            ((string)null).FileExtension().ShouldBe(null);

            // ---

            "file".FileExtension().ShouldBe(null);
            "file.doc".FileExtension().ShouldBe(".doc");
            "file.name.doc".FileExtension().ShouldBe(".doc");
            ".doc".FileExtension().ShouldBe(".doc");

            "file.name.".FileExtension().ShouldBe(null);
            "file.".FileExtension().ShouldBe(null);
            ".".FileExtension().ShouldBe(null);
        }

        [TestMethod]
        public void ShouldReturnEmptyListFromToKeyValuePairs()
        {
            "".ToKeyValuePairs().Count().ShouldBe(0);
            "   ".ToKeyValuePairs().Count().ShouldBe(0);
            ((string)null).ToKeyValuePairs().Count().ShouldBe(0);
        }


        [TestMethod]
        public void ShouldThrowIfKeyValuePairsDelimitersAreNull()
        {
            Should.Throw<ArgumentNullException>(() => "key=value".ToKeyValuePairs(keyValueDelimiter: null));
            Should.Throw<ArgumentNullException>(() => "key=value".ToKeyValuePairs(pairDelimiter: null));

            Should.Throw<ArgumentNullException>(() => "key=value".ToKeyValuePairs(keyValueDelimiter: ""));
            Should.Throw<ArgumentNullException>(() => "key=value".ToKeyValuePairs(pairDelimiter: ""));

            Should.Throw<ArgumentNullException>(() => "key=value".ToKeyValuePairs(keyValueDelimiter: "  "));
            Should.Throw<ArgumentNullException>(() => "key=value".ToKeyValuePairs(pairDelimiter: "  "));
        }

        [TestMethod]
        public void ShouldReturnTwoKeyValuePairs()
        {
            var pairs = "key1=value1&key2=value2".ToKeyValuePairs();
            pairs.Count().ShouldBe(2);

            pairs.ElementAt(0).Key.ShouldBe("key1");
            pairs.ElementAt(0).Value.ShouldBe("value1");

            pairs.ElementAt(1).Key.ShouldBe("key2");
            pairs.ElementAt(1).Value.ShouldBe("value2");
        }

        [TestMethod]
        public void ShouldTakeDifferentDelimitersWhenParsingKeyValuePairs()
        {
            var pairs = "key1:value1+key2:value2".ToKeyValuePairs(":", "+");

            pairs.ElementAt(0).Key.ShouldBe("key1");
            pairs.ElementAt(0).Value.ShouldBe("value1");

            pairs.ElementAt(1).Key.ShouldBe("key2");
            pairs.ElementAt(1).Value.ShouldBe("value2");
        }

        [TestMethod]
        public void ShouldReturnNoPairsFromToKeyValuePairs()
        {
            ((string)null).ToKeyValuePairs().Count().ShouldBe(0);
            "".ToKeyValuePairs().Count().ShouldBe(0);
            "    ".ToKeyValuePairs().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldReturnKeyOnly()
        {
            var pairs = "key".ToKeyValuePairs();
            pairs.Count().ShouldBe(1);
            pairs.ElementAt(0).Key.ShouldBe("key");
            pairs.ElementAt(0).Value.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldReturnMultipleKeysOnlyWithNoValues()
        {
            var pairs = "key1&key2".ToKeyValuePairs();
            pairs.Count().ShouldBe(2);

            pairs.ElementAt(0).Key.ShouldBe("key1");
            pairs.ElementAt(0).Value.ShouldBe(null);

            pairs.ElementAt(1).Key.ShouldBe("key2");
            pairs.ElementAt(1).Value.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldNotAddEmptyKeyValuePairs()
        {
            "key=value&".ToKeyValuePairs().Count().ShouldBe(1);
            "key=value&&&".ToKeyValuePairs().Count().ShouldBe(1);
            "&key=value".ToKeyValuePairs().Count().ShouldBe(1);

            "key=value&&&key=value".ToKeyValuePairs().Count().ShouldBe(2);

            "&".ToKeyValuePairs().Count().ShouldBe(0);
            "&&".ToKeyValuePairs().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldHaveEqualsInKeyValuePairValue()
        {
            "key=a=b".ToKeyValuePairs().ElementAt(0).Value.ShouldBe("a=b");
            "key=a=b&key=a=b".ToKeyValuePairs().ElementAt(1).Value.ShouldBe("a=b");
        }

        [TestMethod]
        public void ShouldHaveValueButNoKey()
        {
            var pairs = "=value&key=value".ToKeyValuePairs();
            pairs.Count().ShouldBe(2);
            pairs.ElementAt(0).Key.ShouldBe(null);
            pairs.ElementAt(0).Value.ShouldBe("value");
        }

        [TestMethod]
        public void ShouldGetSubstringAfterLast()
        {
            "".SubstringAfterLast(".").ShouldBe("");
            "  ".SubstringAfterLast(".").ShouldBe("  ");
            ((string)null).SubstringAfterLast(".").ShouldBe(null);

            "One.Two".SubstringAfterLast("").ShouldBe("One.Two");
            "One.Two".SubstringAfterLast(null).ShouldBe("One.Two");
            "One.Two".SubstringAfterLast("-").ShouldBe("One.Two");

            "One.Two".SubstringAfterLast(".").ShouldBe("Two");
            "One.Two.Three".SubstringAfterLast(".").ShouldBe("Three");

            "One.".SubstringAfterLast(".").ShouldBe("");
            ".".SubstringAfterLast(".").ShouldBe("");
        }

        [TestMethod]
        public void ShouldGetSubstringBeforeLast()
        {
            "".SubstringBeforeLast(".").ShouldBe("");
            "  ".SubstringBeforeLast(".").ShouldBe("  ");
            ((string)null).SubstringBeforeLast(".").ShouldBe(null);

            "One.Two".SubstringBeforeLast("").ShouldBe("One.Two");
            "One.Two".SubstringBeforeLast(null).ShouldBe("One.Two");
            "One.Two".SubstringBeforeLast("-").ShouldBe("One.Two");

            "One.Two".SubstringBeforeLast(".").ShouldBe("One");
            "One.Two.Three".SubstringBeforeLast(".").ShouldBe("One.Two");

            "One.".SubstringBeforeLast(".").ShouldBe("One");
            ".".SubstringBeforeLast(".").ShouldBe("");
        }

        [TestMethod]
        public void ShouldRepeatCharacter()
        {
            "".Repeat(3).ShouldBe("");
            ((string)null).Repeat(3).ShouldBe(null);
            " ".Repeat(3).ShouldBe("   ");

            "1".Repeat(0).ShouldBe("1");
            "1".Repeat(-1).ShouldBe("1");
        }
    }
}
