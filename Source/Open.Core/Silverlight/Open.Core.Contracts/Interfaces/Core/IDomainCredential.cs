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
using System.ComponentModel;
using System.Net;

namespace Open.Core.Common
{
    /// <summary>Represents a user's credentials.</summary>
    public interface IDomainCredential : INotifyPropertyChanged, IDisposable
    {
        /// <summary>Gets or sets the domain or computer name that verifies the credentials.</summary>
        string Domain { get; set; }

        /// <summary>Gets or sets the user name associated with the credentials.</summary>
        string UserName { get; set; }

        /// <summary>Gets the DOMAIN\UserName formatted value.</summary>
        string DomainUser { get; set; }

        /// <summary>Gets or sets the password.</summary>
        string Password { get; set; }

        /// <summary>Gets the 'NetworkCredential' object based on the current settings.</summary>
        NetworkCredential NetworkCredential { get; }

        /// <summary>Gets or sets whether the credentials are valid (null means validity has not been determined).</summary>
        bool? IsValid { get; set; }

        /// <summary>Gets or sets whether the domain value is required (see also 'IsPopulated').</summary>
        bool IsDomainRequired { get; set; }

        /// <summary>Gets whether all values have been populated (see 'IsDomainRequired').</summary>
        bool IsPopulated { get; }

        /// <summary>Sets the credentials properties with the given values.</summary>
        /// <param name="userName">The user name associated with the credentials.</param>
        /// <param name="password">The password.</param>
        void Set(string userName, string password);

        /// <summary>Sets the credentials properties with the given values.</summary>
        /// <param name="userName">The user name associated with the credentials.</param>
        /// <param name="password">The password.</param>
        /// <param name="domain">The domain or computer name that verifies the credentials.</param>
        void Set(string userName, string password, string domain);

        /// <summary>Clears all credential values.</summary>
        void Clear();

        /// <summary>Creates a copy of the credentials.</summary>
        IDomainCredential Clone();

        /// <summary>Copies the given credentials to this instance.</summary>
        /// <param name="value">The value to copy.</param>
        void Copy(IDomainCredential value);
    }
}
