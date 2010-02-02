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

using System.Collections.Generic;
using System.Windows.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Silverlight.Testing;
using Open.Core.Common;
using Open.Core.Common.Testing;

namespace Open.Core.UI.Silverlight.Test.Unit_Tests.Common.Helper_Classes
{
    [TestClass]
    public class UrlHashTest : SilverlightTest
    {
        [TestMethod]
        public void ShouldReadUrlHash()
        {
            HtmlPage.Window.Eval(string.Format("document.location.hash=\"{0}\"", ""));
            UrlHash.Value.ShouldBe(null);

            HtmlPage.Window.Eval(string.Format("document.location.hash=\"{0}\"", "abc"));
            UrlHash.Value.ShouldBe("abc");

            HtmlPage.Window.Eval(string.Format("document.location.hash=\"{0}\"", "    "));
            UrlHash.Value.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldWriteUrlHash()
        {
            HtmlPage.Window.Eval(string.Format("document.location.hash=\"{0}\"", ""));

            UrlHash.Value = null;
            ((string)HtmlPage.Window.Eval("document.location.hash")).TrimEnd("#".ToCharArray()).ShouldBe("");
            UrlHash.Value.ShouldBe(null);

            UrlHash.Value = "1234";
            HtmlPage.Window.Eval("document.location.hash").ShouldBe("#1234");
            UrlHash.Value.ShouldBe("1234");

            UrlHash.Value = "    ";
            ((string)HtmlPage.Window.Eval("document.location.hash")).TrimEnd("#".ToCharArray()).ShouldBe("");
            UrlHash.Value.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldSplitArrayOnAmpesand()
        {
            UrlHash.Value = null;
            UrlHash.ValueArray.Length.ShouldBe(0);

            UrlHash.Value = "   ";
            UrlHash.ValueArray.Length.ShouldBe(0);

            UrlHash.Value = "123";
            var array = UrlHash.ValueArray;
            array.Length.ShouldBe(1);
            array[0].ShouldBe("123");

            UrlHash.Value = "123&abc";
            array = UrlHash.ValueArray;
            array.Length.ShouldBe(2);
            array[0].ShouldBe("123");
            array[1].ShouldBe("abc");

            UrlHash.Value = "123&abc&";
            array = UrlHash.ValueArray;
            array.Length.ShouldBe(2);
            array[0].ShouldBe("123");
            array[1].ShouldBe("abc");

            UrlHash.Value = "123&abc&   ";
            array = UrlHash.ValueArray;
            array.Length.ShouldBe(2);
            array[0].ShouldBe("123");
            array[1].ShouldBe("abc");
        }

        [TestMethod]
        public void ShouldSplitKeyValuePairsToDictionary()
        {
            UrlHash.Value = null;
            UrlHash.ValueDictionary.Count.ShouldBe(0);

            UrlHash.Value = "    ";
            UrlHash.ValueDictionary.Count.ShouldBe(0);

            UrlHash.Value = "Key1=value1&Key2=value2";
            var dic = UrlHash.ValueDictionary;
            dic.Count.ShouldBe(2);
            dic["Key1"].ShouldBe("value1");
            dic["Key2"].ShouldBe("value2");

            UrlHash.Value = "value1&key2=value2";
            dic = UrlHash.ValueDictionary;
            dic["value1"].ShouldBe(null);
            dic["key2"].ShouldBe("value2");

            UrlHash.Value = "key1=value1&";
            UrlHash.ValueDictionary.Count.ShouldBe(1);

            UrlHash.Value = "key1=value1&   ";
            UrlHash.ValueDictionary.Count.ShouldBe(1);
        }

        [TestMethod]
        public void ShouldGetValueCaseInsensitive()
        {
            UrlHash.Value = null;
            UrlHash.GetValue("no-key").ShouldBe(null);

            UrlHash.Value = "Key1=value1&Key2=value2";
            UrlHash.GetValue("key1").ShouldBe("value1");
            UrlHash.GetValue("Key1").ShouldBe("value1");
            UrlHash.GetValue("KEY1").ShouldBe("value1");

            UrlHash.GetValue("KeY2").ShouldBe("value2");

            UrlHash.Value = "Key1&Key2=value2";
            UrlHash.GetValue("key1").ShouldBe(null);
            UrlHash.GetValue("key2").ShouldBe("value2");
        }

        [TestMethod]
        public void ShouldWriteDictionaryAsValue()
        {
            UrlHash.Value = null;
            var values = new Dictionary<string, string>
                             {
                                 {"key1", "Value1"}, 
                                 {"Key2", "value2"}
                             };
            UrlHash.SetValues(values);
            UrlHash.Value.ShouldBe("key1=Value1&Key2=value2");
        }

        [TestMethod]
        public void ShouldWriteSingleValueNonDestructively()
        {
            UrlHash.Value.ShouldBe("key1=Value1&Key2=value2");
            UrlHash.SetValue("key1", "NewValue");
            UrlHash.Value.ShouldBe("key1=NewValue&Key2=value2");

            UrlHash.SetValue("key3", "Value3");
            UrlHash.Value.ShouldBe("key1=NewValue&Key2=value2&key3=Value3");

            UrlHash.SetValue("Key2", "New2");
            UrlHash.Value.ShouldBe("key1=NewValue&Key2=New2&key3=Value3");

            UrlHash.Value = null;
            UrlHash.SetValue("KEY1", "NewValue");
            UrlHash.Value.ShouldBe("KEY1=NewValue");
        }

        [TestMethod]
        public void ShouldRemoveValueByPassingNullNonDestructively()
        {
            UrlHash.Value = "key1=Value1&Key2=value2";
            UrlHash.Value.ShouldBe("key1=Value1&Key2=value2");

            UrlHash.SetValue("key1", null);
            UrlHash.Value.ShouldBe("Key2=value2");

            UrlHash.SetValue("not-a-key", null);
            UrlHash.Value.ShouldBe("Key2=value2");

            UrlHash.SetValue("Key2", null);
            UrlHash.Value.ShouldBe(null);

            UrlHash.SetValue("not-a-key", null);
            UrlHash.Value.ShouldBe(null);
        }
    }
}
