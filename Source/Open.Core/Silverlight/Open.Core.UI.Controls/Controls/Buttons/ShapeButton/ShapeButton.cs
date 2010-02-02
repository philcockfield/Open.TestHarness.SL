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
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Open.Core.Common;
using System.Windows.Shapes;

namespace Open.Core.UI.Controls
{
    /// <summary>Represents a simple button that consists of an path shape.</summary>
    public class ShapeButton : VisualButton
    {
        #region Head
        private const double DefaultBrushOpacity = 0.5;
        private const double DefaultOverBrushOpacity = 0.6;
        private const double DefaultDownBrushOpacity = 0.7;

        private Grid rootVisual;
        private Border iconContainer;
        private bool isInitialized;
        private ParentViewModel viewModel;

        /// <summary>Constructor.</summary>
        public ShapeButton() 
        {
            // Setup initial conditions.
            ButtonTemplates.Instance.ApplyTemplate<ShapeButton>(this);
            Cursor = Cursors.Hand;

            // Wire up events.
            VisualStateChanged += delegate { UpdateVisualState(); };
        }

        public override void OnApplyTemplate()
        {
            // Retrieve elements.
            rootVisual = GetTemplateChild("root") as Grid;
            iconContainer = GetTemplateChild("iconContainer") as Border;
            if (rootVisual == null || iconContainer == null) throw new TemplateNotSetException();

            // Setup the embedded view-model.
            viewModel = new ParentViewModel(this);
            rootVisual.DataContext = viewModel;

            // Load the shape.
            isInitialized = true;
            LoadShape();

            // Finish up.
            base.OnApplyTemplate();
        }
        #endregion

        #region Properties
        /// <summary>Gets the root visual within the button.</summary>
        protected override UIElement RootVisual { get { return rootVisual; } }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the way to stretch the shape within the bounds of the button.</summary>
        public Stretch Stretch
        {
            get { return (Stretch) (GetValue(StretchProperty)); }
            set { SetValue(StretchProperty, value); }
        }
        /// <summary>Gets or sets the way to stretch the shape within the bounds of the button.</summary>
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ShapeButton>(m => m.Stretch),
                typeof (Stretch),
                typeof (ShapeButton),
                new PropertyMetadata(Stretch.Fill));
        #endregion

        #region Dependency Properties - Shapes
        /// <summary>Gets or sets the shape geometry to display within the button.</summary>
        public string ShapePathData
        {
            get { return (string)(GetValue(ShapePathDataProperty)); }
            set { SetValue(ShapePathDataProperty, value); }
        }
        /// <summary>Gets or sets the shape geometry to display within the button.</summary>
        public static readonly DependencyProperty ShapePathDataProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ShapeButton>(m => m.ShapePathData),
                typeof(string),
                typeof(ShapeButton),
                new PropertyMetadata(null, (s, e) => ((ShapeButton)s).LoadShape()));


        /// <summary>Gets or sets a specially shaped hit-target, if different from the 'ShapePathData'.</summary>
        public DataTemplate HitTargetTemplate
        {
            get { return (DataTemplate) (GetValue(HitTargetTemplateProperty)); }
            set { SetValue(HitTargetTemplateProperty, value); }
        }
        /// <summary>Gets or sets a specially shaped hit-target, if different from the 'ShapePathData'.</summary>
        public static readonly DependencyProperty HitTargetTemplateProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ShapeButton>(m => m.HitTargetTemplate),
                typeof (DataTemplate),
                typeof (ShapeButton),
                new PropertyMetadata(null));
        #endregion

        #region Dependency Properties - Brushes
        /// <summary>Gets or sets the brush used to paint the button when in it's default state (ie. not pressed, not down).</summary>
        public Brush DefaultBrush
        {
            get { return (Brush)(GetValue(DefaultBrushProperty)); }
            set { SetValue(DefaultBrushProperty, value); }
        }
        /// <summary>Gets or sets the brush used to paint the button when in it's default state (ie. not pressed, not down).</summary>
        public static readonly DependencyProperty DefaultBrushProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ShapeButton>(m => m.DefaultBrush),
                typeof(Brush),
                typeof(ShapeButton),
                new PropertyMetadata(Colors.Black.ToBrush(DefaultBrushOpacity), (s, e) => ((ShapeButton)s).UpdateVisualState()));


        /// <summary>Gets or sets the brush used to paint the button when the mouse is over it.</summary>
        public Brush OverBrush
        {
            get { return (Brush)(GetValue(OverBrushProperty)); }
            set { SetValue(OverBrushProperty, value); }
        }
        /// <summary>Gets or sets the brush used to paint the button when the mouse is over it.</summary>
        public static readonly DependencyProperty OverBrushProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ShapeButton>(m => m.OverBrush),
                typeof(Brush),
                typeof(ShapeButton),
                new PropertyMetadata(Colors.Black.ToBrush(DefaultOverBrushOpacity), (s, e) => ((ShapeButton)s).UpdateVisualState()));


        /// <summary>Gets or sets the brush used to paint the button when it is depressed.</summary>
        public Brush DownBrush
        {
            get { return (Brush)(GetValue(DownBrushProperty)); }
            set { SetValue(DownBrushProperty, value); }
        }
        /// <summary>Gets or sets the brush used to paint the button when it is depressed.</summary>
        public static readonly DependencyProperty DownBrushProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<ShapeButton>(m => m.DownBrush),
                typeof(Brush),
                typeof(ShapeButton),
                new PropertyMetadata(Colors.Black.ToBrush(DefaultDownBrushOpacity), (s, e) => ((ShapeButton)s).UpdateVisualState()));
        #endregion

        #region Methods
        /// <summary>Applies the given color to the three rendering brushes (default / over / down) using default opacities.</summary>
        /// <param name="color">The color to apply.</param>
        public void SetBrushColors(Color color)
        {
            SetBrushColors(color, DefaultBrushOpacity, DefaultOverBrushOpacity, DefaultDownBrushOpacity);
        }

        /// <summary>Applies the given color to the three rendering brushes (default / over / down) using the given opacities.</summary>
        /// <param name="color">The color to apply.</param>
        /// <param name="defaultOpacity">The opacity for the 'defaul' brush.</param>
        /// <param name="overOpacity">The opacity for the 'over' brush.</param>
        /// <param name="downOpacity">The opacity for the 'down' brush.</param>
        public void SetBrushColors(Color color, double defaultOpacity, double overOpacity, double downOpacity)
        {
            DefaultBrush = color.ToBrush(defaultOpacity);
            OverBrush = color.ToBrush(overOpacity);
            DownBrush = color.ToBrush(downOpacity);
        }
        #endregion

        #region Internal
        private void UpdateVisualState()
        {
            if (!isInitialized) return;
            viewModel.UpdateVisualState();
        }

        private void LoadShape()
        {
            // Setup initial conditions.
            if (!isInitialized) return;

            // Create the shape.
            var xaml = ShapePathData.AsNullWhenEmpty() ?? Shapes.Cross;
            var shape = xaml.ToPathGeometry();

            // Setup the data-binding.
            shape.SetBinding(Shape.FillProperty, ParentViewModel.GetBinding<ParentViewModel>(m => m.Fill));
            shape.SetBinding(Shape.StretchProperty, ParentViewModel.GetParentBinding(m => m.Stretch));
            shape.SetBinding(MarginProperty, ParentViewModel.GetParentBinding(m => m.Padding));

            // Add the visual tree.
            iconContainer.Child = shape;
        }
        #endregion

        public class ParentViewModel : EmbeddedParent<ShapeButton>
        {
            #region Head
            public ParentViewModel(ShapeButton parent) : base(parent)
            {
            }
            #endregion

            #region Properties
            public Brush Fill
            {
                get
                {
                    if (Parent.IsMouseDown) return Parent.DownBrush;
                    if (Parent.IsMouseOver) return Parent.OverBrush;
                    return Parent.DefaultBrush;
                }
            }
            #endregion

            #region Methods
            public void UpdateVisualState()
            {
                OnPropertyChanged<ParentViewModel>(m => m.Fill);
            }
            #endregion
        }
    }
}
