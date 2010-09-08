using System;
using System.Html;

namespace Open.Core
{
    /// <summary>Invokes an action after a delay times out (cancelling any previous actions that may be pending).</summary>
    /// <remarks>This can be used to invoke something after a series of rapid events have fired, like key-strokes or window size changed.</remarks>
    public class DelayedAction : IDisposable
    {
        #region Head
        /// <summary>Fires immediately after the action is invoked when the delay time elapses.</summary>
        public event EventHandler Invoked;
        private void FireInvoked() { if (Invoked != null) Invoked(this, new EventArgs()); }

        private const int NullTimerId = -1;

        private double delay;
        private Action action;
        private static bool isAsyncronous = true;
        private int timerId = NullTimerId;


        /// <summary>Constructor.</summary>
        /// <param name="delaySeconds">The time delay in delay (in seconds).</param>
        /// <param name="action">The action that is invoked after the delay.</param>
        public DelayedAction(double delaySeconds, Action action)
        {
            // Store values.
            Delay = delaySeconds;
            Action = action;
        }

        public void Dispose()
        {
            Stop();
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the time delay in delay (in seconds).</summary>
        public double Delay
        {
            get { return delay; }
            set
            {
                if (value < 0) value = 0;
                delay = value;
            }
        }

        /// <summary>Gets or sets the action that is invoked after the delay.</summary>
        public Action Action
        {
            get { return action; }
            set { action = value; }
        }

        /// <summary>Gets whether the timer is currently in progress.</summary>
        public bool IsRunning { get { return timerId != -1; } }

        /// <summary>Gets or sets whether the delayed action runs asynchronously (true) or synchronously (false) for testing purposes.</summary>
        public static bool IsAsyncronous
        {
            get { return isAsyncronous; }
            set { isAsyncronous = value; }
        }
        #endregion

        #region Methods
        /// <summary>Starts the delay timer.  Subsequent calls to this method restart the timer.</summary>
        public void Start()
        {
            // Stop the timer (if it's already running).
            Stop();
            
            // Start a new timer.
            if (IsAsyncronous)
            {
                timerId = Window.SetTimeout(
                                        delegate { InvokeAction(); }, 
                                        Helper.Time.ToMsecs(Delay));
            }
            else
            {
                // Not in async mode (testing).  Invoke immediately.
                InvokeAction();
            }
        }

        /// <summary>Stops the timer.</summary>
        public void Stop()
        {
            if (IsRunning) Window.ClearTimeout(timerId);
            timerId = NullTimerId;
        }

        /// <summary>Invokes the given action after the specified delay.</summary>
        /// <param name="delay">The delay (in seconds) before invoking the action.</param>
        /// <param name="action">The action to invoke.</param>
        /// <remarks>Returns the 'DelayedAction' used to invoke the method (can be used to cancel the delayed invoke operation).</remarks>
        public static DelayedAction Invoke(double delay, Action action)
        {
            DelayedAction delayedAction = new DelayedAction(delay, action);
            delayedAction.Start();
            return delayedAction;
        }
        #endregion

        #region Internal
        private void InvokeAction()
        {
            if (Script.IsNullOrUndefined(Action)) return;
            Action();
            FireInvoked();
        }
        #endregion
    }
}
