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
using System.Linq;
using System.Net;
using T = Open.Core.Common.DomainCredential;

namespace Open.Core.Common
{
    /// <summary>Represents a user's credentials.</summary>
    public class DomainCredential : ModelBase, IDomainCredential
    {
        #region Head
        private bool previousIsPopulated;

        /// <summary>Constructor.</summary>
        public DomainCredential()
        {
            NetworkCredential = new NetworkCredential();
            SyncNetworkCredential();
            previousIsPopulated = IsPopulated;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            Clear();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
        public string Domain
        {
            get { return GetPropertyValue<T, string>(m => m.Domain); }
            set
            {
                value = value.AsNullWhenEmpty();
                if (value == Domain) return;
                SetPropertyValue<T, string>(m => m.Domain, value, m => m.DomainUser);
                OnCredentialValueChanged();
            }
        }

        /// <summary>Gets or sets the user name associated with the credentials.</summary>
        public string UserName
        {
            get { return GetPropertyValue<T, string>(m => m.UserName); }
            set
            {
                value = value.AsNullWhenEmpty();
                if (value == UserName) return;
                SetPropertyValue<T, string>(m => m.UserName, value, m => m.DomainUser);
                OnCredentialValueChanged();
            }
        }

        /// <summary>Gets or sets the DOMAIN\UserName formatted value.</summary>
        public string DomainUser
        {
            get { return Domain == null ? UserName : string.Format(@"{0}\{1}", Domain, UserName); }
            set
            {
                var credentials = Parse(value);
                Domain = credentials.Domain;
                UserName = credentials.UserName;
            }
        }

        /// <summary>Gets or sets the password.</summary>
        public string Password
        {
            get { return GetPropertyValue<T, string>(m => m.Password); }
            set
            {
                value = value.AsNullWhenEmpty();
                if (value == Password) return;
                SetPropertyValue<T, string>(m => m.Password, value);
                OnCredentialValueChanged();
            }
        }

        /// <summary>Gets or sets whether the credentials are valid (null means validity has not been determined).</summary>
        public bool? IsValid
        {
            get { return GetPropertyValue<T, bool?>(m => m.IsValid); }
            set { SetPropertyValue<T, bool?>(m => m.IsValid, value); }
        }

        /// <summary>Gets the 'NetworkCredential' object based on the current settings.</summary>
        public NetworkCredential NetworkCredential { get; private set; }

        /// <summary>Gets or sets whether the domain value is required (see also 'IsPopulated').</summary>
        public bool IsDomainRequired
        {
            get { return GetPropertyValue<T, bool>(m => m.IsDomainRequired, true); }
            set
            {
                if (value == IsDomainRequired) return;
                SetPropertyValue<T, bool>(m => m.IsDomainRequired, value, true);
                FireIsPopulatedChanged();
            }
        }

        /// <summary>Gets whether all values have been populated (see 'IsDomainRequired').</summary>
        public bool IsPopulated
        {
            get
            {
                if (UserName.AsNullWhenEmpty() == null) return false;
                if (Password.AsNullWhenEmpty() == null) return false;
                return IsDomainRequired
                           ? Domain.AsNullWhenEmpty() != null
                           : true;
            }
        }
        #endregion

        #region Methods
        /// <summary>Sets the credentials properties with the given values.</summary>
        /// <param name="userName">The user name associated with the credentials.</param>
        /// <param name="password">The password.</param>
        public void Set(string userName, string password)
        {
            Set(userName, password, null);
        }

        /// <summary>Sets the credentials properties with the given values.</summary>
        /// <param name="userName">The user name associated with the credentials.</param>
        /// <param name="password">The password.</param>
        /// <param name="domain">The domain or computer name that verifies the credentials.</param>
        public void Set(string userName, string password, string domain)
        {
            UserName = userName.AsNullWhenEmpty();
            Password = password.AsNullWhenEmpty();
            Domain = domain.AsNullWhenEmpty();
        }

        /// <summary>Clears all credential values.</summary>
        public void Clear()
        {
            NetworkCredential = new NetworkCredential();
            UserName = null;
            Password = null;
            Domain = null;
        }

        /// <summary>Creates a copy of the credentials.</summary>
        public IDomainCredential Clone()
        {
            return new DomainCredential
                       {
                           UserName = UserName,
                           Password = Password,
                           Domain = Domain,
                           IsDomainRequired = IsDomainRequired,
                           IsValid =  IsValid,
                       };
        }


        /// <summary>Copies the given credentials to this instance.</summary>
        /// <param name="value">The value to copy.</param>
        public void Copy(IDomainCredential value)
        {
            // Setup initial conditions.
            if (value == null) throw new ArgumentNullException("value");

            // Copy the values.
            UserName = value.UserName;
            Password = value.Password;
            Domain = value.Domain;
            IsDomainRequired = value.IsDomainRequired;
            IsValid = value.IsValid;
        }
        #endregion

        #region Methods - Parse
        /// <summary>Parses the given DOMAIN\User into a domain-credential (with no password associated).</summary>
        /// <param name="domainUser">The DOMAIN\User value.</param>
        public static DomainCredential Parse(string domainUser)
        {
            return Parse(domainUser, null);
        }

        /// <summary>Parses the given values into a domain-credential (seperating a single string into DOMAIN\User).</summary>
        /// <param name="domainUser">The DOMAIN\User value.</param>
        /// <param name="password">The password.</param>
        public static DomainCredential Parse(string domainUser, string password)
        {
            // Setup initial conditions.
            var credentials = new DomainCredential { Password = password.AsNullWhenEmpty() };

            // Seperate the domain and the user.
            string domain;
            string user;
            ToDomainAndUser(domainUser, out domain, out user);

            // Assign to the credentials object.
            credentials.Domain = domain;
            credentials.UserName = user;

            // Finish up.
            return credentials;
        }

        private static void ToDomainAndUser(string value, out string domain, out string userName)
        {
            // Setup initial conditions.
            domain = null;
            userName = null;
            if (value.AsNullWhenEmpty() == null) return;

            // Check for single value (user-name only).
            const string backSlash = @"\";
            var parts = value.Split(backSlash.ToCharArray()).Where(m => m.AsNullWhenEmpty() != null);
            if (parts.Count() == 0) return;
            if (parts.Count() == 1)
            {
                if (value.EndsWith(backSlash))
                {
                    domain = parts.FirstOrDefault();
                }
                else
                {
                    userName = parts.FirstOrDefault();
                }
                return;
            }

            // Has multiple parts, assign the domain from the first value.
            domain = parts.FirstOrDefault();

            // Retrieve the remaining user-name portion.
            userName = value.RemoveStart(domain).TrimStart(backSlash.ToCharArray()).AsNullWhenEmpty();
        }
        #endregion

        #region Internal
        private void OnCredentialValueChanged()
        {
            SyncNetworkCredential();
            FireIsPopulatedChanged();
        }

        private void SyncNetworkCredential()
        {
            var name = UserName.AsNullWhenEmpty();
            var password = Password.AsNullWhenEmpty();
            var domain = Domain.AsNullWhenEmpty();

            var fireEvent =
                NetworkCredential.UserName != name ||
                NetworkCredential.Password != password ||
                NetworkCredential.Domain != domain;

            NetworkCredential.UserName = name;
            NetworkCredential.Password = password;
            NetworkCredential.Domain = domain;

            if (fireEvent) OnPropertyChanged<T>(m => m.NetworkCredential);
        }

        private void FireIsPopulatedChanged()
        {
            if (previousIsPopulated != IsPopulated)
            {
                OnPropertyChanged<T>(m => m.IsPopulated);
            }
            previousIsPopulated = IsPopulated;
        }
        #endregion
    }
}
