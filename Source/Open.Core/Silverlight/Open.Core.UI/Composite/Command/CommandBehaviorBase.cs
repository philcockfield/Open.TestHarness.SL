// Based on CAL: Composite Application Library
// http://msdn.microsoft.com/en-us/library/dd458809.aspx

using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Open.Core.Composite.Command
{
    /// <summary>
    /// Base behavior to handle connecting a <see cref="Control"/> to a Command.
    /// </summary>
    /// <typeparam name="T">The target object must derive from Control</typeparam>
    /// <remarks>
    /// CommandBehaviorBase can be used to provide new behaviors similar to <see cref="ButtonBaseClickCommandBehavior"/>.
    /// </remarks>
    public class CommandBehaviorBase<T> where T : Control
    {
        #region Head
        private ICommand command;
        private object commandParameter;
        private readonly WeakReference targetObject;

        /// <summary>
        /// Constructor specifying the target object.
        /// </summary>
        /// <param name="targetObject">The target object the behavior is attached to.</param>
        public CommandBehaviorBase(T targetObject)
        {
            this.targetObject = new WeakReference(targetObject);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Corresponding command to be execute and monitored for <see cref="ICommand.CanExecuteChanged"/>
        /// </summary>
        public ICommand Command
        {
            get { return command; }
            set
            {
                if (command != null) command.CanExecuteChanged -= CommandCanExecuteChanged;

                command = value;
                if (command != null)
                {
                    command.CanExecuteChanged += CommandCanExecuteChanged;
                    UpdateEnabledState();
                }
            }
        }

        /// <summary>
        /// The parameter to supply the command during execution
        /// </summary>
        public object CommandParameter
        {
            get { return commandParameter; }
            set
            {
                if (commandParameter != value)
                {
                    commandParameter = value;
                    UpdateEnabledState();
                }
            }
        }

        /// <summary>
        /// Object to which this behavior is attached.
        /// </summary>
        protected T TargetObject
        {
            get
            {
                return targetObject.Target as T;
            }
        }        
        #endregion

        #region Methods
        /// <summary>
        /// Updates the target object's IsEnabled property based on the commands ability to execute.
        /// </summary>
        protected virtual void UpdateEnabledState()
        {
            if (TargetObject == null)
            {
                Command = null;
                CommandParameter = null;
            }
            else if (Command != null)
            {
                TargetObject.IsEnabled = Command.CanExecute(CommandParameter);
            }
        }

        /// <summary>
        /// Executes the command, if it's set, providing the <see cref="CommandParameter"/>
        /// </summary>
        protected virtual void ExecuteCommand()
        {
            if (Command != null) Command.Execute(CommandParameter);
        }
        #endregion

        #region Internal
        private void CommandCanExecuteChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }
        #endregion
    }
}
