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
            profile.IsAuthenticated.ShouldBe(true);
            profile.DisplayName.ShouldBe("philfoo");
            profile.Email.ShouldBe("philfoo@gmail.com");
            profile.Identifier.ShouldBe("https://www.google.com/accounts/o8/id?id=AItObyKkx1KGsBvEoaOaWBGEXeawmb57SDOIKPw");
            profile.Name.Given.ShouldBe("Phil");
            profile.Name.Family.ShouldBe("Foo");
            profile.Name.Formatted.ShouldBe("Phil Foo");
            profile.PreferredUserName.ShouldBe("philfoo");
            profile.AuthenticationProvider.ShouldBe("Google");
            profile.VerifiedEmail.ShouldBe("philfoo@gmail.com");

            profile.Error.ShouldBe(null);
            profile.HasError.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldNotBeAuthenticated()
        {
            var profile = OpenIdAuthentication.ToProfile(SampleFailXml);
            profile.IsAuthenticated.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldHaveException()
        {
            var profile = OpenIdAuthentication.ToProfile(SampleFailXml);
            profile.Error.ShouldBeInstanceOfType<AuthenticationException>();
            profile.HasError.ShouldBe(true);

            profile.Error.ErrorCode.ShouldBe(AuthenticationErrorCode.DataNotFound);
            profile.Error.Message.ShouldBe("Data not found");
            profile.Error.IsUnknownError.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldHaveExceptionWithUnknownCode()
        {
            var profile = OpenIdAuthentication.ToProfile(SampleFailXmlUnknownCode);
            profile.Error.ErrorCode.ShouldBe((AuthenticationErrorCode)500);
            profile.Error.Message.ShouldBe("Foo Bar");
            profile.Error.IsUnknownError.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldHaveExceptionWithUnknownCodeAndGeneratedError()
        {
            var profile = OpenIdAuthentication.ToProfile(SampleFailXmlNoCode);
            profile.Error.ErrorCode.ShouldBe(AuthenticationErrorCode.UnknownError);
            profile.Error.Message.ShouldBe("Unknown error occured.");
            profile.Error.IsUnknownError.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldHaveExceptionWithUnknownCodeAndGeneratedErrorWhenNoErrorChildPresent()
        {
            var profile = OpenIdAuthentication.ToProfile(SampleFailXmlNoErrorNode);
            profile.Error.ErrorCode.ShouldBe(AuthenticationErrorCode.UnknownError);
            profile.Error.Message.ShouldBe("Unknown error occured.");
            profile.Error.IsUnknownError.ShouldBe(true);
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

        private const string SampleFailXml = @"<?xml version='1.0' encoding='UTF-8'?><rsp stat='fail'><err msg='Data not found' code='2'/></rsp>";
        private const string SampleFailXmlUnknownCode = @"<?xml version='1.0' encoding='UTF-8'?><rsp stat='fail'><err msg='Foo Bar' code='500'/></rsp>";
        private const string SampleFailXmlNoCode = @"<?xml version='1.0' encoding='UTF-8'?><rsp stat='fail'><err code=''/></rsp>";
        private const string SampleFailXmlNoErrorNode= @"<?xml version='1.0' encoding='UTF-8'?><rsp stat='fail' />";

        // https://rpxnow.com/docs#api_error_responses

        #endregion
    }
}
