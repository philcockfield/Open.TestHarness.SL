using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using Open.Core.Identity;

namespace Open.Core.Common.Test.Core.Web.Identity
{
    [TestClass]
    public class OpenIdAuthenticationTest
    {
        #region Head
        [TestInitialize]
        public void TestSetup()
        {
            
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
            var doc = XDocument.Parse(SampleXml);
            doc.Root.ShouldNotBe(null);
        }

        [TestMethod]
        public void ShouldParseXml()
        {
            var profile = OpenIdAuthentication.ToProfile(SampleXml);
            profile.DisplayName.ShouldBe("philfoo");
            profile.Email.ShouldBe("philfoo@gmail.com");
            profile.Identifier.ShouldBe("https://www.google.com/accounts/o8/id?id=AItObyKkx1KGsBvEoaOaWBGEXeawmb57SDOIKPw");
            profile.Name.Given.ShouldBe("Phil");
            profile.Name.Family.ShouldBe("Foo");
            profile.Name.Formatted.ShouldBe("Phil Foo");
            profile.PreferredUserName.ShouldBe("philfoo");
            profile.AuthenticationProvider.ShouldBe("Google");
            profile.VerifiedEmail.ShouldBe("philfoo@gmail.com");
        }
        #endregion

        #region Sample
        private const string SampleXml = 
                @"<?xml version='1.0' encoding='UTF-8'?>
                    <rsp stat='ok'>
                      <profile>
                        <displayName>philfoo</displayName>
                        <email>philfoo@gmail.com</email>
                        <identifier>https://www.google.com/accounts/o8/id?id=AItObyKkx1KGsBvEoaOaWBGEXeawmb57SDOIKPw</identifier>
                        <name>
                          <givenName>Phil</givenName>
                          <familyName>Foo</familyName>
                          <formatted>Phil Foo</formatted>
                        </name>
                        <preferredUsername>philfoo</preferredUsername>
                        <providerName>Google</providerName>
                        <verifiedEmail>philfoo@gmail.com</verifiedEmail>
                        <googleUserId>535024701020725674864</googleUserId>
                      </profile>
                    </rsp>";
        #endregion
    }
}
