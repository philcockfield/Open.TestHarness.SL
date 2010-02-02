// Based on CAL: Composite Application Library
// http://msdn.microsoft.com/en-us/library/dd458809.aspx

using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Open.Core.Composite.Command
{
    /// <summary>
    /// Static Class that holds all Dependency Properties and Static methods to allow 
    /// the Click event of the ButtonBase class to be attached to a Command. 
    /// </summary>
    /// <remarks>
    /// This class is required, because Silverlight doesn't have native support for Commands. 
    /// </remarks>
    public static class Click
    {
        #region Properties
        /// <summary>Command to execute on click event.</summary>
        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(Click),
                new PropertyMetadata(OnSetCommandCallback));
        public static void SetCommand(ButtonBase buttonBase, ICommand command) { buttonBase.SetValue(CommandProperty, command); }
        public static ICommand GetCommand(ButtonBase buttonBase) { return buttonBase.GetValue(CommandProperty) as ICommand; }


        private static readonly DependencyProperty clickCommandBehaviorProperty = DependencyProperty.RegisterAttached(
                "clickCommandBehavior",
                typeof(ButtonBaseClickCommandBehavior),
                typeof(Click),
                null);
        #endregion

        #region Internal
        private static void OnSetCommandCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            var buttonBase = dependencyObject as ButtonBase;
            if (buttonBase != null)
            {
                var behavior = GetOrCreateBehavior(buttonBase);
                behavior.Command = e.NewValue as ICommand;
            }
        }

        private static ButtonBaseClickCommandBehavior GetOrCreateBehavior(ButtonBase buttonBase)
        {
            var behavior = buttonBase.GetValue(clickCommandBehaviorProperty) as ButtonBaseClickCommandBehavior;
            if (behavior == null)
            {
                behavior = new ButtonBaseClickCommandBehavior(buttonBase);
                buttonBase.SetValue(clickCommandBehaviorProperty, behavior);
            }
            return behavior;
        }
        #endregion
    }
}
