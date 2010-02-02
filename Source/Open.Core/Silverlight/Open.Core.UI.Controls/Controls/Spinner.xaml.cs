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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using Open.Core.Common;
using Open.Core.UI.Common;

namespace Open.Core.UI.Controls
{
    /// <summary>Flags indicating the mode of operation for the Spinner.</summary>
    public enum SpinnerMode
    {
        Fade,
        AlwaysVisible
    }

    /// <summary>A spinner visual that indicates system activity.</summary>
    public partial class Spinner : UserControl
    {
        #region Events
        /// <summary>Fires when the spinner has started spinning.</summary>
        public event EventHandler Started;
        protected void OnStarted()
        {
            OnIsSpinningChanged();
            if (Started != null) Started(this, new EventArgs());
        }

        /// <summary>Fires when the spinner has stopped spinning.</summary>
        public event EventHandler Stopped;
        protected void OnStopped()
        {
            OnIsSpinningChanged();
            if (Stopped != null) Stopped(this, new EventArgs());
        }

        /// <summary>Fires when the spinner has either started or stopped spinning.</summary>
        public event EventHandler IsSpinningChanged;
        protected void OnIsSpinningChanged() { if (IsSpinningChanged != null) IsSpinningChanged(this, new EventArgs()); }        
        #endregion

        #region Head
        public const string PropScale = "Scale";
        public const string PropIsSpinning = "IsSpinning";
        public const string PropColor = "Color";
        public const string PropMode = "Mode";

        private readonly DispatcherTimer timer = new DispatcherTimer();
        private static int stemsDefaultTotalStems = 12;
        private int totalStems;
        private double maxOpacity = 0.6;
        private double minOpacity = 0.1;
        private double speed;
        private int currentStem;
        private double fadeInDuration;
        private double fadeOutDuration;
        private bool isSpinning;
        private bool isStopping;

        public Spinner()
        {
            // Setup initial conditions.
            InitializeComponent();
            container.RenderTransform = new ScaleTransform();

            // Set default values.
            TotalStems = DefaultTotalStems;
            Speed = 0.06;
            FadeInDuration = DefaultFadeInDuration;
            FadeOutDuration = DefaultFadeOutDuration;

            // Wire up events.
            timer.Tick += Handle_Timer_Elapsed;
            Loaded += delegate { if (IsSpinning) Start(); };

            // Finish up.
            OnModeChange();
            OnScaleChange();
            OnIsSpinningChange();
        }
        static Spinner()
        {
            DefaultFadeInDuration = 0.3;
            DefaultFadeOutDuration = 0.5;
        }
        #endregion

        #region Event Handlers
        void Handle_Timer_Elapsed(object sender, EventArgs e)
        {
            if (!IsSpinning && !isStopping) return;
            SetHighlightTrail(currentStem);
            IncrementCurrentStem();
        }
        #endregion

        #region Properties - Static
        /// <summary>Gets or sets the default number of stems on all spinner.</summary>
        public static int DefaultTotalStems
        {
            get { return stemsDefaultTotalStems; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException("Must be greater than one.");
                stemsDefaultTotalStems = value;
            }
        }

        /// <summary>Gets or sets the default fade-in duration (in seconds).</summary>
        public static double DefaultFadeInDuration { get; set; }

        /// <summary>Gets or sets the default fade-out duration (in seconds).</summary>
        public static double DefaultFadeOutDuration { get; set; }
        #endregion
        
        #region Properties
        /// <summary>Gets or sets the total number of stems on the spinner.</summary>
        public int TotalStems
        {
            get{ return totalStems; }
            set
            {
                if (value < 1) throw new ArgumentOutOfRangeException("value", "Must be greater than one.");
                totalStems = value;
                AddStems(value);
            }
        }

        /// <summary>Gets or sets the maximum opacity of a stem (when it is animating).</summary>
        public double MaxOpacity
        {
            get { return maxOpacity; }
            set { maxOpacity = MathUtil.ToWithinBounds(value, 0, 1); }
        }

        /// <summary>Gets or sets the minumum opacity of a stem (default).</summary>
        public double MinOpacity
        {
            get { return minOpacity; }
            set { minOpacity = MathUtil.ToWithinBounds(value, 0, 1); }
        }

        /// <summary>Gets or sets the duration between each tick change when animating (in seconds).</summary>
        public double Speed
        {
            get { return speed; }
            set
            {
                speed = MathUtil.ToWithinBounds(value, 0, double.MaxValue);
                var msecs = (int) (speed*1000);
                timer.Interval = new TimeSpan(0, 0, 0, 0, msecs);
            }
        }

        /// <summary>Gets or sets the fade-in duration for the spinner when starting.</summary>
        /// <remarks>This is only relevant when the Spinner's 'Mode' is set to 'Fade'.</remarks>
        public double FadeInDuration
        {
            get { return fadeInDuration; }
            set { fadeInDuration = MathUtil.ToWithinBounds(value, 0, double.MaxValue); }
        }

        /// <summary>Gets or sets the fade-in duration for the spinner when starting.</summary>
        /// <remarks>This is only relevant when the Spinner's 'Mode' is set to 'Fade'.</remarks>
        public double FadeOutDuration
        {
            get { return fadeOutDuration; }
            set { fadeOutDuration = MathUtil.ToWithinBounds(value, 0, double.MaxValue); }
        }

        private ScaleTransform containerScaleTransform
        {
            get { return container.RenderTransform as ScaleTransform; }
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the color of the spinner stems.</summary>
        public Brush Color
        {
            get { return (Brush) (GetValue(ColorProperty)); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>Gets or sets the color of the spinner stems.</summary>
        public static readonly DependencyProperty ColorProperty =
            DependencyProperty.Register(
                PropColor,
                typeof (Brush),
                typeof (Spinner),
                new PropertyMetadata(new SolidColorBrush(Colors.Black), OnColorChange));
        private static void OnColorChange(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var me = (Spinner) obj;
            foreach (Border item in me.stemParent.Children)
            {
                item.Background = me.Color;
            }
        }


        /// <summary>Gets or sets whether the spinner is currently spinning.</summary>
        public bool IsSpinning
        {
            get { return (bool) (GetValue(IsSpinningProperty)); }
            set { SetValue(IsSpinningProperty, value); }
        }
        /// <summary>Gets or sets whether the spinner is spinning.</summary>
        public static readonly DependencyProperty IsSpinningProperty =
            DependencyProperty.Register(
                PropIsSpinning,
                typeof (bool),
                typeof (Spinner),
                new PropertyMetadata(false, (s, e) => ((Spinner)s).OnIsSpinningChange()));
        private void OnIsSpinningChange()
        {
            if (IsSpinning) Start(); else Stop();
        }


        /// <summary>Sets the zoom percentage of the spinner.</summary>
        public double Scale
        {
            get { return (double) (GetValue(ScaleProperty)); }
            set { SetValue(ScaleProperty, value); }
        }
        /// <summary>Sets the zoom percentage of the spinner.</summary>
        public static readonly DependencyProperty ScaleProperty =
            DependencyProperty.Register(
                PropScale,
                typeof (double),
                typeof (Spinner),
                new PropertyMetadata(0.3, (s, e) => ((Spinner)s).OnScaleChange()));
        private void OnScaleChange()
        {
            var value = Scale;
            value = MathUtil.ToWithinBounds(value, 0, double.MaxValue);
            containerScaleTransform.ScaleX = value;
            containerScaleTransform.ScaleY = value;
            SyncContainerSize();
        }


        /// <summary>Gets or sets the spinner operating mode.</summary>
        public SpinnerMode Mode
        {
            get { return (SpinnerMode) (GetValue(ModeProperty)); }
            set { SetValue(ModeProperty, value); }
        }
        /// <summary>Gets or sets the spinner operating mode.</summary>
        public static readonly DependencyProperty ModeProperty =
            DependencyProperty.Register(
                PropMode,
                typeof (SpinnerMode),
                typeof (Spinner),
                new PropertyMetadata(SpinnerMode.Fade, (s, e) => ((Spinner)s).OnModeChange()));
        private void OnModeChange()
        {
            container.Opacity = (Mode == SpinnerMode.Fade) ? 0 : 1;
        }
        #endregion

        #region Methods
        /// <summary>Retrieves the specified stem.</summary>
        /// <param name="index">The index of the stem to retrieve.</param>
        /// <returns>The specified stem.</returns>
        public Border GetStem(int index)
        {
            return stemParent.Children[index] as Border;
        }
        #endregion

        #region Internal - Start | Stop
        /// <summary>Starts the spinner spinning.</summary>
        private void Start()
        {
            if (isSpinning) return;
            isSpinning = true;
            isStopping = false; // Reset if a stop operation is currently in progress.
            timer.Start();

            double duration;
            switch (Mode)
            {
                case SpinnerMode.AlwaysVisible: duration = 0; break;
                case SpinnerMode.Fade: duration = FadeInDuration; break;
                default: throw new NotSupportedException(Mode.ToString());
            }
            AnimationUtil.FadeIn(container, duration, null, null);
            OnStarted();
        }

        /// <summary>Stops the spinner spinning.</summary>
        private void Stop()
        {
            Stop(null);
        }

        /// <summary>Stops the time spinning.</summary>
        /// <param name="callback">Action to invoke when the stop operation has completed.</param>
        private void Stop(Action callback)
        {
            if (!isSpinning) return;
            isStopping = true;
            switch (Mode)
            {
                case SpinnerMode.AlwaysVisible: StopCallback(callback); break;
                case SpinnerMode.Fade: AnimationUtil.FadeOut(container, FadeOutDuration, null, () => StopCallback(callback)); break;
                default: throw new NotSupportedException(Mode.ToString());
            }
            isSpinning = false;
        }

        private void StopCallback(Action callback)
        {
            // Setup initial conditions.
            if (!isStopping) return;

            // Stop the spin operation.
            timer.Stop();
            Reset();
            SetMinOpacity();

            // Finish up.
            isStopping = false;
            OnStopped();
            if (callback != null) callback.Invoke();
        }
        #endregion

        #region Internal
        private void Reset()
        {
            currentStem = 0;
        }

        private void IncrementCurrentStem()
        {
            currentStem--;
            if (currentStem < 0) currentStem = TotalStems - 1;
        }

        private void AddStems(int total)
        {
            // Setup initial conditions.
            Reset();
            stemParent.Children.Clear();
            var angle = (double)decimal.Divide(360, total);

            // Add the stems
            double cumulativeAngle = 0;
            for (var i = 0; i < total; i++)
            {
                AddStem(cumulativeAngle);
                cumulativeAngle -= angle;
            }

            // Offset the container so that the spinner is centered.
            var transform = new TranslateTransform
                                {
                                    Y = 0 - (GetStemRotateCenter(stemParent).Y - (stemParent.Height * 0.5))
                                };
            stemParent.RenderTransform = transform;

            // Finish up.
            SyncContainerSize();
            SetMinOpacity();
        }

        private void AddStem(double angle)
        {
            var stem = CreateStem();
            ((RotateTransform)stem.RenderTransform).Angle = angle;
            stemParent.Children.Add(stem);
        }

        private Border CreateStem()
        {
            // Create the stem.
            var stem = new Border
                           {
                               Width = stemParent.Width,
                               Height = stemParent.Height,
                               CornerRadius = new CornerRadius(stemParent.Width *0.5),
                               Background = Color,
                               RenderTransform = new RotateTransform()
                           };

            // Setup the transform.
            var rotate = (RotateTransform) stem.RenderTransform;
            var offset = GetStemRotateCenter(stem);
            rotate.CenterX = offset.X;
            rotate.CenterY = offset.Y;

            // Finish up.
            return stem;
        }

        private static Point GetStemRotateCenter(FrameworkElement stem)
        {
            var x = stem.Width * 0.5;
            var y = stem.Height * 1.7;
            return new Point(x, y);
        }

        private void SyncContainerSize()
        {
            // Update the size.
            var side = (GetStemRotateCenter(stemParent).Y * 2) * Scale;
            Width = side;
            Height = side;

            // Adjust the center position around which scaling is calculated.
            var transformCenter = (side * 0.5);
            containerScaleTransform.CenterX = transformCenter;
            containerScaleTransform.CenterY = transformCenter;
        }

        private void SetMinOpacity()
        {
            foreach (FrameworkElement item in stemParent.Children)
            {
                item.Opacity = MinOpacity;
            }
        }

        private void SetHighlightTrail(int startStem)
        {
            // Setup initial conditions.
            if (TotalStems == 0) return;
            var current = startStem;
            var trailOpacity = GetTrailOpacity(TotalStems);

            for (var i = 0; i < TotalStems; i++)
            {
                // Update the opacity.
                GetStem(current).Opacity = trailOpacity[i];

                // Increment the stem
                current++;
                if (current >= TotalStems) current = 0;
            }
        }

        private List<double> GetTrailOpacity(int total)
        {
            var returnList = new List<double>();
            for (var i = 0; i < total; i++)
            {
                var x = ((double)Decimal.Divide(i + 1, total));
                var opacity = MathUtil.ExponentialDecay(x, 3);
                opacity = Math.Min(MaxOpacity, MaxOpacity * opacity + MinOpacity);
                returnList.Add(opacity);
            }
            return returnList;
        }
        #endregion
    }
}
