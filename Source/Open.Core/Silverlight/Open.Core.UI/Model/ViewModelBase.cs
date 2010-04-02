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
using System.Linq.Expressions;
using System.Windows.Controls;
using System.Windows.Input;
using Open.Core.Composite.Command;

namespace Open.Core.Common
{
    /// <summary>Base class for all ViewModel's.</summary>
    public abstract class ViewModelBase : ModelBase
    {
        #region Events
        /// <summary>Fires when the IsActive property changes (this is accompanied also by the PropertyChanged event).</summary>
        public event EventHandler IsActiveChanged;
        private void OnIsActiveChanged(){if (IsActiveChanged != null) IsActiveChanged(this, new EventArgs());}
        #endregion

        #region Properties
        /// <summary>Gets or sets whether the view-model is currently active.</summary>
        /// <remarks>
        ///    Use this flag to suppress behavior when the view-model has been loaded into memory
        ///    but is not currently associated with an active screen.
        /// </remarks>
        public bool IsActive
        {
            get { return GetPropertyValue<ViewModelBase, bool>(m => m.IsActive, true); }
            set
            {
                if (value == IsActive) return;
                SetPropertyValue<ViewModelBase, bool>(m => m.IsActive, value, true);
                OnIsActiveChanged();
            }
        }
        #endregion

        #region Methods - GetCommand
        /// <summary>Gets a command corresponding to the specified Command property (using a Button command type).</summary>
        /// <typeparam name="TClass">The type of the model.</typeparam>
        /// <param name="commandProperty">An expression representing the Command property (for example 'n => n.PropertyName').</param>
        /// <param name="onExecute">The invoke action of the command.</param>
        /// <remarks>The returned ICommand is a 'DelegateCommand'.</remarks>
        protected ICommand GetCommand<TClass>(
                        Expression<Func<TClass, object>> commandProperty,
                        Action onExecute)
        {
            return GetCommand<TClass, Button>(commandProperty, onExecute);
        }

        /// <summary>Gets a command corresponding to the specified Command property.</summary>
        /// <typeparam name="TClass">The type of the model.</typeparam>
        /// <typeparam name="TCommand">The type of element binding to the command (eg. Button).</typeparam>
        /// <param name="commandProperty">An expression representing the Command property (for example 'n => n.PropertyName').</param>
        /// <param name="onExecute">The invoke action of the command.</param>
        /// <remarks>The returned ICommand is a 'DelegateCommand'.</remarks>
        protected ICommand GetCommand<TClass, TCommand>(
                        Expression<Func<TClass, object>> commandProperty,
                        Action onExecute)
        {
            return GetCommand<TClass, TCommand>(commandProperty, null, onExecute);
        }

        /// <summary>Gets a command corresponding to the specified Command property (using a Button command type).</summary>
        /// <typeparam name="TClass">The type of the model.</typeparam>
        /// <param name="commandProperty">An expression representing the Command property (for example 'n => n.PropertyName').</param>
        /// <param name="isEnabledProperty">
        ///     An expression representing the Property that indicates whether command is enabled (for example 'n => n.PropertyName').
        ///     Must be a return type of Bool.
        /// </param>
        /// <param name="onExecute">The invoke action of the command.</param>
        /// <remarks>The returned ICommand is a 'DelegateCommand'.</remarks>
        protected ICommand GetCommand<TClass>(
                        Expression<Func<TClass, object>> commandProperty, 
                        Expression<Func<TClass, object>> isEnabledProperty,
                        Action onExecute)
        {
            return GetCommand<TClass, Button>(commandProperty, isEnabledProperty, onExecute);
        }

        /// <summary>Gets a command corresponding to the specified Command property.</summary>
        /// <typeparam name="TClass">The type of the model.</typeparam>
        /// <typeparam name="TCommand">The type of element binding to the command (eg. Button).</typeparam>
        /// <param name="commandProperty">An expression representing the Command property (for example 'n => n.PropertyName').</param>
        /// <param name="isEnabledProperty">
        ///     An expression representing the Property that indicates whether command is enabled (for example 'n => n.PropertyName').
        ///     Must be a return type of Bool.
        /// </param>
        /// <param name="onExecute">The invoke action of the command.</param>
        /// <remarks>The returned ICommand is a 'DelegateCommand'.</remarks>
        protected ICommand GetCommand<TClass, TCommand>(
                        Expression<Func<TClass, object>> commandProperty, 
                        Expression<Func<TClass, object>> isEnabledProperty,
                        Action onExecute)
        {
            // Check if the command already exists.
            var wrapper = GetPropertyValue<TClass, CommandWrapper<TCommand>>(commandProperty);
            if (wrapper != null) return wrapper.Command;

            // Create the command.
            wrapper = new CommandWrapper<TCommand>();
            if (isEnabledProperty == null)
            {
                wrapper.Command = new DelegateCommand<TCommand>(m => onExecute());
            }
            else
            {
                // Get the IsEnabled property and ensure it is a boolean.
                var propName = isEnabledProperty.GetPropertyName();
                var property = GetType().GetProperty(propName);
                if (property.PropertyType != typeof(bool)) 
                    throw new ArgumentOutOfRangeException(
                        string.Format("The command enabled property '{0}' does not return a Boolean value.", propName));

                // Setup the command.
                wrapper.Command = new DelegateCommand<TCommand>(
                                    m => onExecute(),
                                    m => (bool)property.GetValue(this, null));
                wrapper.MonitorIsEnabled(this, propName);
            }

            // Store value and return.
            SetPropertyValue(commandProperty, wrapper);
            return wrapper.Command;
        }
        #endregion

        private class CommandWrapper<TCommand>
        {
            public DelegateCommand<TCommand> Command { get; set; }
            private string isEnabledPropertyName;
            public void MonitorIsEnabled(INotifyPropertyChanged parent, string propertyName)
            {
                isEnabledPropertyName = propertyName;
                parent.PropertyChanged += (s, e) =>
                                              {
                                                  if (e.PropertyName == isEnabledPropertyName && Command != null)
                                                  {
                                                      Command.RaiseCanExecuteChanged();
                                                  }
                                              };
            }
        }
    }
}
