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
using System.Windows.Threading;

namespace Open.Core.Common
{
    /// <summary>Invokes an action after a delay times out (cancelling any previous actions that may be pending).</summary>
    /// <remarks>This can be used to invoke something after a series of rapid events have fired, like key-strokes size changed.</remarks>
    public class DelayedAction : DisposableBase
    {
        #region Event Handlers
        /// <summary>Fires when the delay timer is started.</summary>
        public event EventHandler Started;
        private void OnStarted(){if (Started != null) Started(this, new EventArgs());}

        /// <summary>Fires when the delay timer is stopped.</summary>
        public event EventHandler Stopped;
        private void OnStopped(){if (Stopped != null) Stopped(this, new EventArgs());}

        /// <summary>Fires immediately after the action is invoked after a delay.</summary>
        public event EventHandler ActionInvoked;
        private void OnActionInvoked(){if (ActionInvoked != null) ActionInvoked(this, new EventArgs());}
        #endregion

        #region Head
        private DispatcherTimer timer;
        private double delay;
        private static bool isAsyncronous = true;

        public DelayedAction(double delaySeconds, Action action)
        {
            // Store values.
            Delay = delaySeconds;
            Action = action;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            Stop();
        }
        #endregion

        #region Event Handlers
        private void OnTimerElapsed(object sender, EventArgs e)
        {
            lock (this)
            {
                Stop();
                InvokeAction();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the time delay (in seconds).</summary>
        public double Delay
        {
            get { return delay; }
            set
            {
                if (value < 0) value = 0;
                delay = value;
                lock (this)
                {
                    SetDelay(timer);    
                }
            }
        }

        /// <summary>Gets or sets the action.</summary>
        public Action Action { get; set; }

        /// <summary>Gets whether the timer is currently in progress.</summary>
        public bool IsRunning { get; private set; }

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
            lock(this)
            {
                // Setup initial conditions.
                IsRunning = true;

                // Check if the object is in synchronous testing mode.
                if (!IsAsyncronous)
                {
                    InvokeAction();
                    Stop();
                    return;
                }

                // Start the timer.
                if (timer == null) timer = CreateTimer();
                timer.Stop();
                timer.Start();

                // Finish up.
                OnStarted();
            }
        }

        /// <summary>Stops the timer.</summary>
        public void Stop()
        {
            lock(this)
            {
                var fireEvent = IsRunning;
                IsRunning = false;
                if (timer != null) timer.Stop(); 
                if (fireEvent) OnStopped();
            }
        }

        /// <summary>Invokes the given action after the specified delay.</summary>
        /// <param name="delay">The delay (in seconds) before invoking the action.</param>
        /// <param name="action">The action to invoke.</param>
        /// <remarks>Returns the 'DelayedAction' used to invoke the method (can be used to cancel the delayed invoke operation).</remarks>
        public static DelayedAction Invoke(double delay, Action action)
        {
            var delayedAction = new DelayedAction(delay, action);
            delayedAction.Start();
            return delayedAction;
        }
        #endregion

        #region Internal
        private void SetDelay(DispatcherTimer targetTimer)
        {
            if (targetTimer == null) return;
            var msecs = (int)(Delay * 1000);
            targetTimer.Interval = new TimeSpan(0, 0, 0, 0, msecs);
        }

        private DispatcherTimer CreateTimer()
        {
            var newTimer = new DispatcherTimer();
            SetDelay(newTimer);
            newTimer.Tick += OnTimerElapsed;
            return newTimer;
        }

        private void InvokeAction()
        {
            if (this.Action == null) return;
            this.Action.Invoke();
            OnActionInvoked();
        }
        #endregion
    }
}
