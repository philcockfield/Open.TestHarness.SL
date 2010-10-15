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
                if (Set(PropHorizontal, value, DefaultHorizontal))
                {
                    if (isInitialized) LayoutHorizontal();
                }
            }
        }

        /// <summary>Gets or sets the alignment to apply to the Y axis.</summary>
        public VerticalAlign Vertical
        {
            get { return (VerticalAlign) Get(PropVertical, DefaultVertical); }
            set
            {
                if (Set(PropVertical, value, DefaultVertical))
                {
                    if (isInitialized) LayoutVertical();
                }
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
        private static void InitializeChild(jQueryObject child)
        {
            child.CSS(Css.Position, Css.Absolute);
        }

        private Size GetChildrenOffset(ArrayList children)
        {
            int width = 0;
            int height = 0;
            int index = 0;
            foreach (jQueryObject child in children)
            {
                bool isFirst = index == 0;
                bool isLast = index == children.Count - 1;
//                index++;

                Size offset = GetChildOffset(child, isFirst, isLast);
                width += offset.Width;
                height += offset.Height;
            }
            return new Size(width, height);
        }

        private Size GetChildOffset(jQueryObject child, bool isFirst, bool isLast)
        {
            // Setup initial conditions.
            int width = child.GetWidth();
            int height = child.GetHeight();

            // Account for border.
            Spacing border = Css.GetPixelBorder(child);
            if (!isFirst)
            {
                width += border.Left;
                height += border.Top;
            }
            if (!isLast)
            {
                width += border.Right;
                height += border.Bottom;
            }

            // Account for uniform margin;
            width += ChildMargin.HorizontalOffset;
            height += ChildMargin.VerticalOffset;

            // Finish up.
            return new Size(width, height);
        }
        #endregion

        #region Internal : Horizontal Layout
        private void LayoutHorizontal()
        {
            if (!isInitialized) return;
            ArrayList children = Children;
            switch (Horizontal)
            {
                case HorizontalAlign.Left: AlignX(children, 0, Css.Left); break;
                case HorizontalAlign.Center:

                    Log.Warning("GetChildOffset(children): " + GetChildrenOffset(children).ToString());

                    int left = (Container.GetWidth() / 2) - (GetChildrenOffset(children).Width / 2);
                    AlignX(children, left, Css.Left); 
                    break;

                case HorizontalAlign.Right:
                    children.Reverse();
                    AlignX(children, 0, Css.Right);
                    break;

                case HorizontalAlign.Distribute:
                    break;
                default: throw new Exception("Not supported: " + Horizontal.ToString());
            }
        }

        private void AlignX(ArrayList children, int position, string cssAttribute)
        {
            int index = 0;
            foreach (jQueryObject child in children)
            {
                bool isFirst = index == 0;
                bool isLast = index == children.Count - 1;

                InitializeChild(child);
                child.CSS(Css.Left, string.Empty);
                child.CSS(Css.Right, string.Empty);
                Spacing border = Css.GetPixelBorder(child);

//                Log.Warning("ChildMargin.Left: " + ChildMargin.Left + " | border.Left: " + border.Left); //TEMP 

                position += ChildMargin.Left + (isFirst ? 0 : border.Left);
                child.CSS(cssAttribute, position + Css.Px);
                position += child.GetWidth() + ChildMargin.Right + (isLast ? 0 : border.Right);
            }
        }
        #endregion

        #region Internal : Vertical Layout
        private void LayoutVertical()
        {
            if (!isInitialized) return;
            //TODO
        }
        #endregion
    }
}
