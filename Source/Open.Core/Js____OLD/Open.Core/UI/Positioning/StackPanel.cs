using System;
using System.Collections;
using System.Html;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Stacks child elements within a containing block element.</summary>
    public class StackPanel : ModelBase
    {
        #region Head
        public const string PropHorizontal = "Horizontal";
        public const string PropVertical = "Vertical";
        public const string PropStackHorizontal = "StackHorizontal";
        public const string PropStackVertical = "StackVertical";

        private const HorizontalAlign DefaultHorizontal = HorizontalAlign.Left;
        private const VerticalAlign DefaultVertical= VerticalAlign.Top;

        private readonly jQueryObject container;
        private readonly bool isInitialized;
        private readonly Spacing childMargin = new Spacing();

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing block element whose children are being stacked.</param>
        [AlternateSignature]
        public extern StackPanel(jQueryObject container);

        /// <summary>Constructor.</summary>
        /// <param name="container">The containing block element whose children are being stacked.</param>
        /// <param name="horizontal">The alignment to apply to the X axis.</param>
        /// <param name="vertical">The alignment to apply to the Y axis.</param>
        public StackPanel(jQueryObject container, HorizontalAlign horizontal, VerticalAlign vertical)
        {
            // Setup initial conditions.
            this.container = container;

            // Set default values.
            Horizontal = Script.IsNullOrUndefined(horizontal) ? DefaultHorizontal : horizontal;
            Vertical = Script.IsNullOrUndefined(vertical) ? DefaultVertical: vertical;

            // Wire up events.
            childMargin.PropertyChanged += delegate { UpdateLayout(); };

            // Finish up.
            isInitialized = true;
            UpdateLayout();
        }
        #endregion

        #region Properties
        /// <summary>Gets the containing block element whose children are being stacked.</summary>
        public jQueryObject Container{get { return container; }}

        /// <summary>Gets or sets the alignment to apply to the X axis.</summary>
        public HorizontalAlign Horizontal
        {
            get { return (HorizontalAlign) Get(PropHorizontal, DefaultHorizontal); }
            set
            {
                if (Set(PropHorizontal, value, DefaultHorizontal)) LayoutHorizontal();
            }
        }

        /// <summary>Gets or sets the alignment to apply to the Y axis.</summary>
        public VerticalAlign Vertical
        {
            get { return (VerticalAlign) Get(PropVertical, DefaultVertical); }
            set
            {
                if (Set(PropVertical, value, DefaultVertical)) LayoutVertical();
            }
        }

        /// <summary>Gets or sets whether stacking occurs on the X axis.</summary>
        public bool StackHorizontal
        {
            get { return (bool) Get(PropStackHorizontal, true); }
            set
            {
                if (Set(PropStackHorizontal, value, true)) LayoutHorizontal();
            }
        }

        /// <summary>Gets or sets whether stacking occurs on the Y axis.</summary>
        public bool StackVertical
        {
            get { return (bool) Get(PropStackVertical, false); }
            set
            {
                if (Set(PropStackVertical, value, false)) LayoutVertical();
            }
        }

        /// <summary>Gets or sets the pixel margin to place around each item.</summary>
        public Spacing ChildMargin { get { return childMargin; } }
        #endregion

        #region Properties : Internal
        private ArrayList Children
        {
            get
            {
                ArrayList children = new ArrayList();
                Container.Children().Each(delegate(int index, Element element)
                                              {
                                                  children.Add(jQuery.FromElement(element));
                                              });
                return children;
            }
        }

        private int WidestChild
        {
            get 
            { 
                int width = 0;
                foreach (jQueryObject child in Children)
                {
                    int childWidth = child.GetWidth();
                    if (childWidth > width) width = childWidth;
                }
                return width;
            }
        }

        private int TallestChild
        {
            get
            {
                int height = 0;
                foreach (jQueryObject child in Children)
                {
                    int childHeight = child.GetHeight();
                    if (childHeight > height) height = childHeight;
                }
                return height;
            }
        }
        #endregion

        #region Methods
        /// <summary>Stacks the child elements.</summary>
        public void UpdateLayout()
        {
            LayoutHorizontal();
            LayoutVertical();
        }
        #endregion

        #region Internal
        private Size GetChildrenOffset(ArrayList children)
        {
            int width = 0;
            int height = 0;
            foreach (jQueryObject child in children)
            {
                Size offset = GetChildOffset(child);
                width += offset.Width;
                height += offset.Height;
            }
            return new Size(width, height);
        }

        private Size GetChildOffset(jQueryObject child)
        {
            // Setup initial conditions.
            int width = child.GetWidth();
            int height = child.GetHeight();

            // Account for border.
            Spacing border = Css.GetPixelBorder(child);
            width += border.HorizontalOffset;
            height += border.VerticalOffset;

            // Account for uniform margin;
            width += ChildMargin.HorizontalOffset;
            height += ChildMargin.VerticalOffset;

            // Finish up.
            return new Size(width, height);
        }
        #endregion

        #region Internal : Layout
        private void LayoutHorizontal()
        {
            if (!isInitialized) return;
            if (StackHorizontal) { HorizontalStack(); } else { AlignHorizontal(); }
        }

        private void LayoutVertical()
        {
            if (!isInitialized) return;
            if (StackVertical) { VerticalStack(); } else { AlignVertical(); }
        }

        private void AlignHorizontal()
        {
            switch (Horizontal)
            {
                case HorizontalAlign.Left: AlignChildren(Css.Left, 0); break;
                case HorizontalAlign.Center: AlignChildren(Css.Left, (Container.GetWidth() / 2) - (WidestChild / 2)); break;
                case HorizontalAlign.Right: AlignChildren(Css.Right, 0); break;
                default: throw new Exception("Not supported: " + Horizontal.ToString());
            }
        }

        private void AlignVertical()
        {
            switch (Vertical)
            {
                case VerticalAlign.Top: AlignChildren(Css.Top, 0); break;
                case VerticalAlign.Center: AlignChildren(Css.Top, (Container.GetHeight() / 2) - (TallestChild / 2)); break;
                case VerticalAlign.Bottom: AlignChildren(Css.Bottom, 0); break;
                default: throw new Exception("Not supported: " + Vertical.ToString());
            }
        }

        private void HorizontalStack()
        {
            ArrayList children = Children;
            switch (Horizontal)
            {
                case HorizontalAlign.Left: StackX(children, 0, Css.Left); break;
                case HorizontalAlign.Center:
                    int left = (Container.GetWidth() / 2) - (GetChildrenOffset(children).Width / 2);
                    StackX(children, left, Css.Left);
                    break;

                case HorizontalAlign.Right:
                    children.Reverse();
                    StackX(children, 0, Css.Right);
                    break;

                default: throw new Exception("Not supported: " + Horizontal.ToString());
            }
        }

        private void VerticalStack()
        {
            ArrayList children = Children;
            switch (Vertical)
            {
                case VerticalAlign.Top: StackY(children, 0, Css.Top); break;
                case VerticalAlign.Center:
                    int top = (Container.GetHeight() / 2) - (GetChildrenOffset(children).Height / 2);
                    StackY(children, top, Css.Top);
                    break;

                case VerticalAlign.Bottom:
                    children.Reverse();
                    StackY(children, 0, Css.Bottom);
                    break;

                default: throw new Exception("Not supported: " + Vertical.ToString());
            }
        }

        private void StackX(ArrayList children, int position, string cssAttribute)
        {
            int index = 0;
            foreach (jQueryObject child in children)
            {
                bool isFirst = index == 0;
                bool isLast = index == children.Count - 1;
                index++;

                InitHorizontalCss(child);
                Spacing border = Css.GetPixelBorder(child);

                position += ChildMargin.Left + (isFirst ? 0 : border.Left);
                child.CSS(cssAttribute, position + Css.Px);
                position += child.GetWidth() + ChildMargin.Right + (isLast ? 0 : border.Right);
            }
        }

        private void StackY(ArrayList children, int position, string cssAttribute)
        {
            int index = 0;
            foreach (jQueryObject child in children)
            {
                bool isFirst = index == 0;
                bool isLast = index == children.Count - 1;
                index++;

                InitVerticalCss(child);
                Spacing border = Css.GetPixelBorder(child);

                position += ChildMargin.Top + (isFirst ? 0 : border.Top);
                child.CSS(cssAttribute, position + Css.Px);
                position += child.GetHeight() + ChildMargin.Bottom + (isLast ? 0 : border.Bottom);
            }
        }

        private static void InitHorizontalCss(jQueryObject child)
        {
            child.CSS(Css.Position, Css.Absolute);
            child.CSS(Css.Left, string.Empty);
            child.CSS(Css.Right, string.Empty);
        }

        private static void InitVerticalCss(jQueryObject child)
        {
            child.CSS(Css.Position, Css.Absolute);
            child.CSS(Css.Top, string.Empty);
            child.CSS(Css.Bottom, string.Empty);
        }

        private void AlignChildren(string attribute, int value)
        {
            foreach (jQueryObject child in Children)
            {
                child.CSS(attribute, value + Css.Px);
            }
        }
        #endregion
    }
}
