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
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Open.Core.Common.Testing;
using System.Net;

using T = Open.Core.Common.IDomainCredential;

namespace Open.Core.Common.Test.Core.Common.Model
{
    [TestClass]
    public class DomainCredentialTest
    {
        #region Head
        private IDomainCredential credential;

        [TestInitialize]
        public void TestSetup()
        {
            credential = new DomainCredential();
        }
        #endregion
        
        #region Tests
        [TestMethod]
        public void ShouldTurnNullIntoEmptyStringOnNetworkCredential()
        {
            var nc = new NetworkCredential {UserName = null};
            nc.UserName.ShouldBe(String.Empty);

            nc.UserName = "Value";
            nc.UserName.ShouldBe("Value");
        }

        [TestMethod]
        public void ShouldConstructWithDefaultValues()
        {
            credential.UserName.ShouldBe(null);
            credential.Domain.ShouldBe(null);
            credential.Password.ShouldBe(null);
            credential.IsValid.ShouldBe(null);
            credential.IsDomainRequired.ShouldBe(true);

            credential.NetworkCredential.ShouldNotBe(null);
            credential.NetworkCredential.UserName.ShouldBe(String.Empty);
            credential.NetworkCredential.Domain.ShouldBe(String.Empty);
            credential.NetworkCredential.Password.ShouldBe(String.Empty);
        }

        [TestMethod]
        public void ShouldUpdateNetworkCredentials()
        {
            credential.UserName = "Name";
            credential.Password = "Password";
            credential.Domain = "Domain";

            credential.NetworkCredential.UserName.ShouldBe("Name");
            credential.NetworkCredential.Password.ShouldBe("Password");
            credential.NetworkCredential.Domain.ShouldBe("Domain");
        }

        [TestMethod]
        public void ShouldClearValuesWhenDisposed()
        {
            credential.UserName = "Name";
            credential.Password = "Password";
            credential.Domain = "Domain";

            credential.Dispose();

            credential.IsPopulated.ShouldBe(false);
            credential.UserName.ShouldBe(null);
            credential.Domain.ShouldBe(null);
            credential.Password.ShouldBe(null);

            credential.NetworkCredential.UserName.ShouldBe("");
            credential.NetworkCredential.Domain.ShouldBe("");
            credential.NetworkCredential.Password.ShouldBe("");
        }

        [TestMethod]
        public void ShouldFireNetworkCredentialChanged()
        {
            credential.ShouldFirePropertyChanged<T>(() => credential.UserName = "Name", m => m.NetworkCredential);
            credential.ShouldFirePropertyChanged<T>(() => credential.Password = "Password", m => m.NetworkCredential);
            credential.ShouldFirePropertyChanged<T>(() => credential.Domain = "Domain", m => m.NetworkCredential);

            credential.ShouldNotFirePropertyChanged<T>(() => credential.UserName = "Name", m => m.NetworkCredential);
            credential.ShouldNotFirePropertyChanged<T>(() => credential.Password = "Password", m => m.NetworkCredential);
            credential.ShouldNotFirePropertyChanged<T>(() => credential.Domain = "Domain", m => m.NetworkCredential);
        }

        [TestMethod]
        public void ShouldReportIsPopulatedWithDomain()
        {
            credential.IsDomainRequired = true;
            credential.IsPopulated.ShouldBe(false);

            credential.UserName = "Name";
            credential.Password = "Password";
            credential.Domain = "Domain";
            credential.IsPopulated.ShouldBe(true);

            credential.Domain = "  ";
            credential.IsPopulated.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldReportIsPopulatedWithoutDomain()
        {
            credential.IsDomainRequired = false;
            credential.IsPopulated.ShouldBe(false);

            credential.UserName = "Name";
            credential.Password = "Password";
            credential.IsPopulated.ShouldBe(true);

            credential.UserName = "  ";
            credential.Password = "Password";
            credential.IsPopulated.ShouldBe(false);

            credential.UserName = "Name";
            credential.Password = " ";
            credential.IsPopulated.ShouldBe(false);
        }

        [TestMethod]
        public void ShouldFireIsPopulatedChanged()
        {
            credential.UserName = "Name";
            credential.Password = "Password";

            credential.ShouldFirePropertyChanged<T>(() => credential.Domain = "Domain", m => m.IsPopulated);
            credential.ShouldNotFirePropertyChanged<T>(() => credential.Domain = "Domain", m => m.IsPopulated);
            credential.ShouldNotFirePropertyChanged<T>(() => credential.Domain = "NEW", m => m.IsPopulated);

            // --

            credential = new DomainCredential
                             {
                                 IsDomainRequired = false,
                                 UserName = "name",
                                 Password = "1234",
                             };
            credential.IsPopulated.ShouldBe(true);
            credential.ShouldFirePropertyChanged<T>(() => credential.IsDomainRequired = true, m => m.IsPopulated);
            credential.IsPopulated.ShouldBe(false);

            // --

            credential = new DomainCredential
                            {
                                IsDomainRequired = true,
                                UserName = "name",
                                Password = "1234",
                            };
            credential.IsPopulated.ShouldBe(false);
            credential.ShouldFirePropertyChanged<T>(() => credential.IsDomainRequired = false, m => m.IsPopulated);
            credential.IsPopulated.ShouldBe(true);

            // --

            credential = new DomainCredential
                        {
                            IsDomainRequired = true,
                            UserName = "name",
                            Password = "1234",
                            Domain = "domain"
                        };
            credential.IsPopulated.ShouldBe(true);
            credential.ShouldFirePropertyChanged<T>(() => credential.UserName = null, m => m.IsPopulated);
        }

        [TestMethod]
        public void ShouldSetViaMethod()
        {
            credential.IsPopulated.ShouldBe(false);
            credential.Set("Name", "Password", "Domain");

            credential.NetworkCredential.UserName.ShouldBe("Name");
            credential.NetworkCredential.Password.ShouldBe("Password");
            credential.NetworkCredential.Domain.ShouldBe("Domain");

            credential.Set(" ", " ", " ");
            credential.UserName.ShouldBe(null);
            credential.Domain.ShouldBe(null);
            credential.Password.ShouldBe(null);

            credential.Set("Name", "Password");
            credential.NetworkCredential.UserName.ShouldBe("Name");
            credential.NetworkCredential.Password.ShouldBe("Password");
            credential.Domain.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldClear()
        {
            credential.Set("Name", "Password", "Domain");
            credential.IsPopulated.ShouldBe(true);

            credential.Clear();

            credential.IsPopulated.ShouldBe(false);
            credential.UserName.ShouldBe(null);
            credential.Domain.ShouldBe(null);
            credential.Password.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldClone()
        {
            credential.Set("Name", "Password", "Domain");
            credential.IsDomainRequired = false;
            credential.IsValid = true;

            var clone = credential.Clone();
            clone.UserName.ShouldBe("Name");
            clone.Password.ShouldBe("Password");
            clone.Domain.ShouldBe("Domain");
            clone.IsDomainRequired.ShouldBe(false);
            clone.IsValid.ShouldBe(true);
        }

        [TestMethod]
        public void ShouldCopy()
        {
            var value = new DomainCredential();
            value.Set("Name", "Password", "Domain");
            value.IsDomainRequired = false;
            value.IsValid = true;

            credential.Copy(value);

            credential.UserName.ShouldBe("Name");
            credential.Password.ShouldBe("Password");
            credential.Domain.ShouldBe("Domain");
            credential.IsDomainRequired.ShouldBe(false);
            credential.IsValid.ShouldBe(true);

            Should.Throw<ArgumentNullException>(() => credential.Copy(null));
        }

        [TestMethod]
        public void ShouldParseDomainUser()
        {
            DomainCredential.Parse("UserName").UserName.ShouldBe("UserName");
            DomainCredential.Parse("UserName").Domain.ShouldBe(null);
            DomainCredential.Parse("UserName").Password.ShouldBe(null);

            DomainCredential.Parse(@"DOMAIN\UserName").UserName.ShouldBe("UserName");
            DomainCredential.Parse(@"DOMAIN\UserName").Domain.ShouldBe("DOMAIN");
            DomainCredential.Parse(@"DOMAIN\UserName").Password.ShouldBe(null);

            DomainCredential.Parse(@"DOMAIN\UserName", "password").UserName.ShouldBe("UserName");
            DomainCredential.Parse(@"DOMAIN\UserName", "password").Domain.ShouldBe("DOMAIN");
            DomainCredential.Parse(@"DOMAIN\UserName", "password").Password.ShouldBe("password");

            DomainCredential.Parse(null, "password").UserName.ShouldBe(null);
            DomainCredential.Parse(null, "password").Domain.ShouldBe(null);
            DomainCredential.Parse(null, "password").Password.ShouldBe("password");

            DomainCredential.Parse(null, null).UserName.ShouldBe(null);
            DomainCredential.Parse(null, null).Domain.ShouldBe(null);
            DomainCredential.Parse(null, null).Password.ShouldBe(null);

            DomainCredential.Parse(@"DOMAIN\\UserName").UserName.ShouldBe("UserName");
            DomainCredential.Parse(@"DOMAIN\\UserName").Domain.ShouldBe("DOMAIN");

            DomainCredential.Parse(@"DOMAIN\\\\\UserName").UserName.ShouldBe("UserName");
            DomainCredential.Parse(@"DOMAIN\\\\\UserName").Domain.ShouldBe("DOMAIN");

            DomainCredential.Parse(@"DOMAIN/UserName").UserName.ShouldBe("DOMAIN/UserName");
            DomainCredential.Parse(@"DOMAIN/UserName").Domain.ShouldBe(null);

            DomainCredential.Parse(@"DOMAIN\/UserName").UserName.ShouldBe("/UserName");
            DomainCredential.Parse(@"DOMAIN\/UserName").Domain.ShouldBe("DOMAIN");

            DomainCredential.Parse(@"\UserName").UserName.ShouldBe("UserName");
            DomainCredential.Parse(@"\UserName").Domain.ShouldBe(null);

            DomainCredential.Parse(@"\\\\\UserName").UserName.ShouldBe("UserName");
            DomainCredential.Parse(@"\\\UserName").Domain.ShouldBe(null);

            DomainCredential.Parse(@"DOMAIN\").UserName.ShouldBe(null);
            DomainCredential.Parse(@"DOMAIN\").Domain.ShouldBe("DOMAIN");
            DomainCredential.Parse(@"DOMAIN\\\").Domain.ShouldBe("DOMAIN");

            DomainCredential.Parse(@"DOMAIN\\UserName\value").UserName.ShouldBe(@"UserName\value");
            DomainCredential.Parse(@"DOMAIN\\UserName\value").Domain.ShouldBe("DOMAIN");

            DomainCredential.Parse(@"\\\").UserName.ShouldBe(null);
            DomainCredential.Parse(@"\\\").Domain.ShouldBe(null);

            DomainCredential.Parse(@"\").UserName.ShouldBe(null);
            DomainCredential.Parse(@"\").Domain.ShouldBe(null);

            DomainCredential.Parse(@"").UserName.ShouldBe(null);
            DomainCredential.Parse(@"").Domain.ShouldBe(null);
        }

        [TestMethod]
        public void ShouldGetDomainUser()
        {
            credential.DomainUser.ShouldBe(null);

            credential.UserName = "name";
            credential.DomainUser.ShouldBe("name");
            credential.Clear();

            credential.Domain = "DOMAIN";
            credential.DomainUser.ShouldBe(@"DOMAIN\");
            credential.Clear();

            credential.Set("user", "password", "DOMAIN");
            credential.Domain.ShouldBe("DOMAIN");
            credential.UserName.ShouldBe("user");
            credential.DomainUser.ShouldBe(@"DOMAIN\user");
        }

        [TestMethod]
        public void ShouldSetDomainUser()
        {
            credential.DomainUser = @"DOMAIN\User";
            credential.Domain.ShouldBe("DOMAIN");
            credential.UserName.ShouldBe("User");

            credential.DomainUser = "  ";
            credential.Domain.ShouldBe(null);
            credential.UserName.ShouldBe(null);

            credential.DomainUser = @"DOMAIN\\\\User\wierd\value";
            credential.Domain.ShouldBe("DOMAIN");
            credential.UserName.ShouldBe(@"User\wierd\value");
            credential.Clear();

            credential.DomainUser = @"DOMAIN\";
            credential.Domain.ShouldBe("DOMAIN");
            credential.UserName.ShouldBe(null);
            credential.Clear();

            credential.DomainUser = @"Name";
            credential.Domain.ShouldBe(null);
            credential.UserName.ShouldBe("Name");
            credential.Clear();
        }

        [TestMethod]
        public void ShouldFireDomainUserChanged()
        {
            credential.ShouldFirePropertyChanged<IDomainCredential>(() => credential.Domain = "DOMAIN", m => m.DomainUser);
            credential.ShouldFirePropertyChanged<IDomainCredential>(() => credential.UserName = "User", m => m.DomainUser);
            credential.ShouldFirePropertyChanged<IDomainCredential>(() => credential.DomainUser = @"DOM\Bob", m => m.DomainUser);
            credential.ShouldFirePropertyChanged<IDomainCredential>(() => credential.DomainUser = null, m => m.DomainUser);
            credential.Clear();

            credential.ShouldFirePropertyChanged<IDomainCredential>(() => credential.DomainUser = @"DOMAIN\User1", m => m.Domain);
            credential.ShouldFirePropertyChanged<IDomainCredential>(() => credential.DomainUser = @"DOMAIN\User2", m => m.UserName);
        }
        #endregion
    }
}
