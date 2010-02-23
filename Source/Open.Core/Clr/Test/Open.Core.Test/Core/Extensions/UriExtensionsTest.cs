using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;

namespace Open.Core.Common.Test.Core.Extensions
{
    [TestClass]
    public class UriExtensionsTest
    {
        [TestMethod]
        public void ShouldHaveQueryStringOnAbsoluteUri()
        {
            var uri = new Uri("http://google.com?key=value&key=value", UriKind.Absolute);
            var pairs = uri.GetQueryString();
            pairs.Count().ShouldBe(2);

            pairs.ElementAt(0).Key.ShouldBe("key");
            pairs.ElementAt(0).Value.ShouldBe("value");

            pairs.ElementAt(1).Key.ShouldBe("key");
            pairs.ElementAt(1).Value.ShouldBe("value");
        }


        [TestMethod]
        public void ShouldReturnStringKeyValuePair()
        {
            var uri = new Uri("http://google.com?key=value", UriKind.Absolute);
            var pairs = uri.GetQueryString();
            pairs.Count().ShouldBe(1);

            pairs.ElementAt(0).Key.ShouldBe("key");
            pairs.ElementAt(0).Value.ShouldBe("value");
        }

        [TestMethod]
        public void ShouldHaveQueryStringOnRelativeUri()
        {
            var uri = new Uri("/Default.aspx?key=value&key=value", UriKind.Relative);
            var pairs = uri.GetQueryString();
            pairs.Count().ShouldBe(2);

            pairs.ElementAt(0).Key.ShouldBe("key");
            pairs.ElementAt(0).Value.ShouldBe("value");

            pairs.ElementAt(1).Key.ShouldBe("key");
            pairs.ElementAt(1).Value.ShouldBe("value");
        }

        [TestMethod]
        public void ShouldNotHaveQueryString()
        {
            new Uri("/Default.aspx", UriKind.Relative).GetQueryString().Count().ShouldBe(0);
            new Uri("/Default.aspx?", UriKind.Relative).GetQueryString().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldReturnKeysOnlyInQueryString()
        {
            var uri = new Uri("/Default.aspx?key1&key2&key3", UriKind.Relative);
            var pairs = uri.GetQueryString();
            pairs.Count().ShouldBe(3);

            pairs.ElementAt(0).Key.ShouldBe("key1");
            pairs.ElementAt(1).Key.ShouldBe("key2");
            pairs.ElementAt(2).Key.ShouldBe("key3");
        }

        [TestMethod]
        public void ShouldReturnEmptyQueryStringWhenNullUriPassed()
        {
            ((Uri)null).GetQueryString().Count().ShouldBe(0);
        }

        [TestMethod]
        public void ShouldReturnQueryStringThatContainsQuestionMark()
        {
            var uri = new Uri("/Default.aspx?key?=value&key=valu?e", UriKind.Relative);
            var pairs = uri.GetQueryString();
            pairs.Count().ShouldBe(2);

            pairs.ElementAt(0).Key.ShouldBe("key?");
            pairs.ElementAt(0).Value.ShouldBe("value");

            pairs.ElementAt(1).Key.ShouldBe("key");
            pairs.ElementAt(1).Value.ShouldBe("valu?e");
        }

        [TestMethod]
        public void ShouldReturnQueryStringWithKeyStartingWithQuestionMark()
        {
            var uri = new Uri("/Default.aspx??key=value", UriKind.Relative);
            uri.GetQueryString().ElementAt(0).Key.ShouldBe("?key");
        }
    }
}
