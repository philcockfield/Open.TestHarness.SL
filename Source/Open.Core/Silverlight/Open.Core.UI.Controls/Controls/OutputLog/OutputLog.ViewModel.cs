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
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Open.Core.Common;
using Open.Core.Composite.Command;
using Open.Core.UI.Controls.Assets;

using T = Open.Core.UI.Controls.OutputLogViewModel;
using TLine = Open.Core.UI.Controls.OutputLineViewModel;

namespace Open.Core.UI.Controls
{
    /// <summary>An output display log.</summary>
    /// <remarks>This view-model is automatically constructed by the OutputLog control (view first).</remarks>
    public class OutputLogViewModel : ViewModelBase
    {
        #region Head
        private readonly DelayedAction scrollDelay;
        private readonly Brush dividerColor;
        private readonly Brush lineBreakColor;

        public OutputLogViewModel(IOutput writer, OutputLog control)
        {
            // Store values.
            Writer = writer;
            Control = control;

            // Create objects.
            Strings = new StringLibrary();
            Lines = new ObservableCollection<OutputLineViewModel>();
            scrollDelay = new DelayedAction(0.1, ScrollToBottom);

            // Create brushes.
            dividerColor = new SolidColorBrush(Colors.Black) { Opacity = 0.1 };
            lineBreakColor = new SolidColorBrush(Color.FromArgb(255, 255, 0, 228)) { Opacity = 0.4 };

            // Create commands.
            ClearCommand = new DelegateCommand<Button>(m => Clear(), m => IsClearButtonEnabled);

            // Wire up events.
            writer.WrittenTo += HandleWrittenTo;
            writer.Cleared += delegate { Clear(); };
            writer.BreakInserted += delegate { InsertBreak(); };

            // Finish up.
            UpdateLineMargin();
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            Writer.WrittenTo -= HandleWrittenTo;
            scrollDelay.Stop();
        }
        #endregion

        #region Event Handlers
        private void HandleWrittenTo(object sender, OutputEventArgs e)
        {
            Control.SyncContext.Send(state => Write(e.Line), null);
        }

        internal void OnShowTimeStampChanged()
        {
            UpdateVisualState();
            foreach (var line in Lines)
            {
                line.UpdateVisualState();
            }
        }
        #endregion

        #region Properties
        public StringLibrary Strings { get; private set; }
        public int Count { get; set; }
        public IOutput Writer { get; private set; }
        public OutputLog Control { get; private set; }
        public ObservableCollection<OutputLineViewModel> Lines { get; private set; }
        public DelegateCommand<Button> ClearCommand { get; private set; }
        public bool IsClearButtonEnabled { get { return Lines.Count > 0; } }
        public Thickness LineMargin { get; private set; }
        public bool IsLineMarginVisible { get { return Lines.Count > 0; } }
        public double NumberColumnWidth
        {
            get
            {
                return Control.ShowTimeStamp
                                ? 108
                                : (Count.ToString().Length * 8) + 25;
            }
        }
        #endregion

        #region Methods
        /// <summary>Updates the visual state of the control.</summary>
        internal void UpdateVisualState()
        {
            UpdateLineMargin();
            OnPropertyChanged<T>(
                            m => m.LineMargin, 
                            m => m.NumberColumnWidth, 
                            m => m.IsLineMarginVisible);
            ClearCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region Internal - Main Actions
        private void Write(IOutputLine e)
        {
            // Setup initial conditions.
            if (!Control.IsActive) return;
            Count++;

            // Update the collection.
            var line = new OutputLineViewModel
                                    {
                                        Parent = this,
                                        LineNumber = Count,
                                        Created = DateTime.Now,
                                        Text = ToText(e),
                                        FontWeight = e.FontWeight,
                                        Background = GetBackground(e),
                                        DividerColor = dividerColor,
                                    };
            lock (Lines)
            {
                Lines.Add(line);
            }

            // Update visual state.
            UpdateVisualState();

            // Scroll to bottom item.
            ScrollToBottom();
            scrollDelay.Start(); // NB: Ensure scroll is moved to the bottom if multiple calls made before redraw.
        }

        private void Clear()
        {
            Count = 0;
            Lines.Clear();
            UpdateVisualState();
        }

        private void InsertBreak()
        {
            var line = Lines.LastOrDefault();
            if (line == null) return;
            line.DividerColor = lineBreakColor;
            line.UpdateVisualState();
        }
        #endregion

        #region Internal
        private static Brush GetBackground(IOutputLine e)
        {
            // Setup initial conditions.
            var defaultColor = default(Color);
            var color = e.Color;
            if (Equals(color, defaultColor)) return null;

            // Set opacity of color (only adjust if the color does not already contain an opacity value).
            if (color.A == 255) color.A = 20;

            // Finish up.
            return new SolidColorBrush(color);
        }

        private static string ToText(IOutputLine line)
        {
            if (line == null || line.Value == null) return null;
            return line.Value.ToString();
        }

        private void UpdateLineMargin()
        {
            var left = NumberColumnWidth;
            if (LineMargin.Left == left) return;
            LineMargin = new Thickness(left, 0, 0, 0);
        }

        private void ScrollToBottom()
        {
            var scroller = Control.Scroller;
            if (scroller == null) return;
            scroller.ScrollToVerticalOffset(scroller.ExtentHeight);
        }
        #endregion
    }

    public class OutputLineViewModel : ViewModelBase
    {
        public OutputLogViewModel Parent { get; set; }
        public string Text { get; set; }
        public FontWeight FontWeight { get; set; }
        public Brush Background { get; set; }
        public Brush DividerColor { get; set; }

        public DateTime Created { get; set; }
        public string TimeStamp { get { return Created.ToString("h:mm:ss.ff tt"); } }
        public int LineNumber { get; set; }
        public string LineIdentifier { get { return Parent.Control.ShowTimeStamp ? TimeStamp : LineNumber.ToString(); } }

        public void UpdateVisualState()
        {
            OnPropertyChanged<TLine>(
                                m => m.Background,
                                m => m.DividerColor,
                                m => m.LineIdentifier);
        }
    }
}
