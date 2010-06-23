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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Open.Core.Common
{
    /// <summary>Implements INotifyDataErrorInfo on the model.</summary>
    public abstract partial class ModelBase : INotifyDataErrorInfo  
    {
        #region Head
        /// <summary>Fires when the validation errors have changed for a property or for the entire object.</summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private void FireErrorsChanged(string propertyName) { if (ErrorsChanged != null) ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName)); }

        private List<ErrorItem> currentErrors;
        private class ErrorItem
        {
            public string PropertyName { get; set; }
            public readonly List<IErrorInfo> Errors = new List<IErrorInfo>();
        }
        #endregion

        #region Properties
        /// <summary>Gets a value that indicates whether the object has validation errors.</summary>
        public bool HasErrors { get { return CurrentErrors.Count > 0; } }

        private List<ErrorItem> CurrentErrors
        {
            get { return currentErrors ?? (currentErrors = new List<ErrorItem>()); }
        }
        #endregion

        #region Methods
        /// <summary>Gets all validation errors for the objec6.</summary>
        public IEnumerable<IErrorInfo> GetErrors() { return GetErrorsInternal(null); }

        /// <summary>Gets the validation errors for a specified property or for the entire object.</summary>
        /// <param name="property">
        ///    An expression that represents the property
        ///    to retrieve errors for (for example 'n => n.PropertyName'.)
        /// </param>
        public IEnumerable<IErrorInfo> GetErrors<T>(Expression<Func<T, object>> property)
        {
            var propertyName = (property == null)
                                    ? null
                                    : property.GetPropertyName();
            return GetErrorsInternal(propertyName);
        }

        /// <summary>Gets the validation errors for a specified property or for the entire object.</summary>
        /// <param name="propertyName">
        ///     The name of the property to retrieve validation errors for, or null or String.Empty to retrieve errors for the entire object.
        /// </param>
        public IEnumerable GetErrors(string propertyName)
        {
            return GetErrorsInternal(propertyName);
        }

        private IEnumerable<IErrorInfo> GetErrorsInternal(string propertyName)
        {
            return propertyName.IsNullOrEmpty(true) 
                        ? GetAllErrors() :
                        GetPropertyErrorList(propertyName);
        }
        #endregion

        #region Methods - Protected
        /// <summary>Assigns the given error to the specified property.</summary>
        /// <typeparam name="T">The type of model.</typeparam>
        /// <param name="property">
        ///    An expression that represents the property
        ///    to assign the error to (for example 'n => n.PropertyName'.)
        /// </param>
        /// <param name="errorCode">The unique identifier of the error.</param>
        /// <param name="message">The descriptive error message.</param>
        protected void AddError<T>(Expression<Func<T, object>> property, 
                int errorCode, 
                string message)
        {
            AddError(property, new ErrorInfo { ErrorCode = errorCode, ErrorMessage = message});
        }


        /// <summary>Assigns the given error to the specified property.</summary>
        /// <typeparam name="T">The type of model.</typeparam>
        /// <param name="property">
        ///    An expression that represents the property
        ///    to assign the error to (for example 'n => n.PropertyName'.)
        /// </param>
        /// <param name="error">The error to assign.</param>
        protected void AddError<T>(Expression<Func<T, object>> property, IErrorInfo error)
        {
            // Setup initial conditions.
            if (property == null) throw new ArgumentNullException("property");
            if (error == null) throw new ArgumentNullException("error");
            var propertyName = property.GetPropertyName();
            var list = GetPropertyErrorList(propertyName);

            // Remove any existing errors.
            var existingError = list.FirstOrDefault(m => m.ErrorCode == error.ErrorCode);
            if (existingError != null) list.Remove(existingError);

            // Update the error-list.
            list.Add(error);

            // Finish up.
            FireErrorsChanged(propertyName);
        }

        /// <summary>Removes the given error from the specified property.</summary>
        /// <typeparam name="T">The type of model.</typeparam>
        /// <param name="property">
        ///    An expression that represents the property
        ///    to remove the error from (for example 'n => n.PropertyName'.)
        /// </param>
        /// <param name="errorCode">The unique identifier of the error.</param>
        protected void RemoveError<T>(Expression<Func<T, object>> property, int errorCode)
        {
            // Setup initial conditions.
            if (property == null) throw new ArgumentNullException("property");
            var propertyName = property.GetPropertyName();
            var list = GetPropertyErrorList(propertyName);

            // Retrieve the error to remove.
            var error = list.FirstOrDefault(m => m.ErrorCode == errorCode);
            if (error == null) return;

            // Remove from the list.
            list.Remove(error);

            // Finish up.
            FireErrorsChanged(propertyName);
        }

        /// <summary>Removes all errors from the specified property.</summary>
        /// <typeparam name="T">The type of model.</typeparam>
        /// <param name="property">
        ///    An expression that represents the property
        ///    to remove the errors from (for example 'n => n.PropertyName'.)
        /// </param>
        protected void ClearErrors<T>(Expression<Func<T, object>> property)
        {
            if (property == null) throw new ArgumentNullException("property");
            ClearErrors(property.GetPropertyName());
        }
        private void ClearErrors(string propertyName)
        {
            // Retrieve the list of errors (don't continue if there are no errors).
            var list = GetPropertyErrorList(propertyName);
            if (list.Count == 0) return;

            // Clear the list or errors.
            list.Clear();

            // Finish up.
            FireErrorsChanged(propertyName);
        }

        /// <summary>Clears all errors from the model (for all properties).</summary>
        protected void ClearErrors()
        {
            if (currentErrors == null || CurrentErrors.Count == 0) return;
            foreach (var item in CurrentErrors.ToList())
            {
                ClearErrors(item.PropertyName);
            }
        }
        #endregion

        #region Internal
        private List<IErrorInfo> GetPropertyErrorList(string propertyName)
        {
            var item = CurrentErrors.FirstOrDefault(m => m.PropertyName == propertyName);
            if (item == null)
            {
                item = new ErrorItem {PropertyName = propertyName};
                CurrentErrors.Add(item);
            }
            return item.Errors;
        }

        private IEnumerable<IErrorInfo> GetAllErrors()
        {
            var errors = new List<IErrorInfo>();
            foreach (var item in CurrentErrors)
            {
                errors.AddRange(item.Errors);
            }
            return errors;
        }
        #endregion
    }
}
