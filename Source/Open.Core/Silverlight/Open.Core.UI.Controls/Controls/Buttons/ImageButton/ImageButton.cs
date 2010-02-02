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

using System.Windows;
using System.Windows.Media;
using Open.Core.Common;
using System.Windows.Controls;

namespace Open.Core.UI.Controls
{
    /// <summary>Represents a simple button that consists of an image.</summary>
    public class ImageButton : VisualButton
    {
        #region Head
        private Grid rootVisual;
        private bool isInitialized;
        private ParentViewModel viewModel;

        /// <summary>Constructor.</summary>
        public ImageButton()
        {
            // Setup initial conditions.
            ButtonTemplates.Instance.ApplyTemplate(this);

            // Wire up events.
            VisualStateChanged += delegate { UpdateVisualState(); };
            IsEnabledChanged += delegate { UpdateVisualState(); };
        }

        public override void OnApplyTemplate()
        {
            // Retrieve elements.
            rootVisual = GetTemplateChild("root") as Grid;
            if (rootVisual == null) throw new TemplateNotSetException();

            // Setup the embedded view-model.
            viewModel = new ParentViewModel(this);
            rootVisual.DataContext = viewModel;

            // Finish up.
            isInitialized = true;
            base.OnApplyTemplate();
        }
        #endregion

        #region Properties
        protected override UIElement RootVisual { get { return rootVisual; } }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the default image used for the button.</summary>
        public ImageSource Source
        {
            get { return (ImageSource) (GetValue(SourceProperty)); }
            set { SetValue(SourceProperty, value); }
        }
        /// <summary>Gets or sets the default image used for the button.</summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ImageButton>(m => m.Source),
                typeof (ImageSource),
                typeof (ImageButton),
                new PropertyMetadata(null, (s, e) => ((ImageButton) s).UpdateVisualState()));


        /// <summary>Gets or sets the image used when the mouse is over the button (the default 'Source' is used if not set).</summary>
        public ImageSource OverSource
        {
            get { return (ImageSource) (GetValue(OverSourceProperty)); }
            set { SetValue(OverSourceProperty, value); }
        }
        /// <summary>Gets or sets the images used when the mouse is over the button (the default 'Source' is used if not set).</summary>
        public static readonly DependencyProperty OverSourceProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ImageButton>(m => m.OverSource),
                typeof (ImageSource),
                typeof (ImageButton),
                new PropertyMetadata(null, (s, e) => ((ImageButton)s).UpdateVisualState()));


        /// <summary>Gets or sets the image used when the the button is depressed (the default 'Source' is used if not set).</summary>
        public ImageSource DownSource
        {
            get { return (ImageSource) (GetValue(DownSourceProperty)); }
            set { SetValue(DownSourceProperty, value); }
        }
        /// <summary>Gets or sets the images used when the the button is depressed (the default 'Source' is used if not set).</summary>
        public static readonly DependencyProperty DownSourceProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ImageButton>(m => m.DownSource),
                typeof (ImageSource),
                typeof (ImageButton),
                new PropertyMetadata(null, (s, e) => ((ImageButton)s).UpdateVisualState()));
        

        /// <summary>Gets or sets the image used when the button is disabled (the default 'Source' is used if not set).</summary>
        public ImageSource DisabledSource
        {
            get { return (ImageSource) (GetValue(DisabledSourceProperty)); }
            set { SetValue(DisabledSourceProperty, value); }
        }
        /// <summary>Gets or sets the image used when the button is disabled (the default 'Source' is used if not set).</summary>
        public static readonly DependencyProperty DisabledSourceProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ImageButton>(m => m.DisabledSource),
                typeof (ImageSource),
                typeof (ImageButton),
                new PropertyMetadata(null, (s, e) => ((ImageButton)s).UpdateVisualState()));
        


        /// <summary>Gets or sets the way to stretch the shape within the bounds of the button.</summary>
        public Stretch Stretch
        {
            get { return (Stretch)(GetValue(StretchProperty)); }
            set { SetValue(StretchProperty, value); }
        }
        /// <summary>Gets or sets the way to stretch the image within the bounds of the button.</summary>
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ShapeButton>(m => m.Stretch),
                typeof(Stretch),
                typeof(ImageButton),
                new PropertyMetadata(Stretch.None));

        #endregion

        #region Internal
        private void UpdateVisualState()
        {
            if (!isInitialized) return;
            viewModel.UpdateVisualState();
        }
        #endregion

        public class ParentViewModel : EmbeddedParent<ImageButton>
        {
            #region Head
            public ParentViewModel(ImageButton parent) : base(parent)
            {
            }
            #endregion

            #region Properties
            public ImageSource Source
            {
                get
                {
                    if (!Parent.IsEnabled) return DefaultIfNull(Parent.DisabledSource);
                    if (Parent.IsMouseDown) return DefaultIfNull(Parent.DownSource);
                    if (Parent.IsMouseOver) return DefaultIfNull(Parent.OverSource);
                    return Parent.Source;
                }
            }
            #endregion

            #region Methods
            public void UpdateVisualState()
            {
                OnPropertyChanged<ParentViewModel>(m => m.Source);
            }
            #endregion

            #region Internal
            private ImageSource DefaultIfNull(ImageSource image)
            {
                return image ?? Parent.Source;
            }
            #endregion
        }
    }
}
