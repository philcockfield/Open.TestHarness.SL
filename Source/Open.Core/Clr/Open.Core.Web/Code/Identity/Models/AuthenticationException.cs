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
using System.Runtime.Serialization;
using System.Text;

namespace Open.Core.Identity
{
    /// <summary>Codes for authentication errors.</summary>
    public enum AuthenticationErrorCode
    {
        UnknownError = -2,
        ServiceTemporarilyUnavailable = -1,
        MissingParameter = 0,
        InvalidParameter = 1,
        DataNotFound = 2,
        AuthenticationError = 3,
        FacebookError = 4,
        MappingExists = 5,
        ErrorInteractingWithPreviouslyOperationalProvider = 6,
        RpxAccountUpgradeNeededToAccessApi = 7,
        MissingThirdPartyCredentialsForIdentifier = 8,
        ThirdPartyCredentialsRevoked = 9,
        ApplicationNotProperlyConfigured = 10,
        ProviderOrIdentifierDoesNotSupportFeature = 11,
        GoogleError = 12,
        TwitterError = 13,
        LinkedInError = 14,
        LiveIdError = 15,
        MySpaceError = 16,
        YahooError = 17,
    }

    /// <summary>Exception that is used when authentication fails.</summary>
    public class AuthenticationException : Exception
    {
        // Constructors.
        protected AuthenticationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
        public AuthenticationException(AuthenticationErrorCode errorCode, string message) : this(errorCode, message, null) { }
        public AuthenticationException(AuthenticationErrorCode errorCode, string message, Exception innerException) : base(message, innerException)
        {
            ErrorCode = errorCode;
        }

        /// <summary>Gets the error code.</summary>
        public AuthenticationErrorCode ErrorCode { get; private set; }

        /// <summary>Gets whether the error is not a known exception type defined within the 'AuthenticationErrorCode' enum.</summary>
        public bool IsUnknownError
        {
            get
            {
                if (ErrorCode == AuthenticationErrorCode.UnknownError) return true;
                return !(typeof (AuthenticationErrorCode)
                                    .GetEnumValues().Cast<AuthenticationErrorCode>()
                                    .Contains(ErrorCode));
            }
        }
    }
}
