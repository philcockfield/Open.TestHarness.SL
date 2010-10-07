using System;
using System.Runtime.CompilerServices;
using jQueryApi;

namespace Open.Core
{
    /// <summary>Flags representing how to manage spacing for an element.</summary>
    public enum SpacingMode
    {
        /// <summary>Sync the padding value.</summary>
        Padding = 0,

        /// <summary>Sync the magin value.</summary>
        Margin = 1,
    }

    /// <summary>Used to alter an edge value when being synced by the Spacing object.</summary>
    /// <param name="edge">The edge being synced.</param>
    /// <param name="value">The value being written.</param>
    /// <returns>The altered value.</returns>
    public delegate int SpacingSync(Edge edge, int value);

    /// <summary>Represents a spacing for edges (left, right, top, bottom).</summary>
    public class Spacing : ModelBase
    {
        #region Head
        public const string PropLeft = "Left";
        public const string PropTop = "Top";
        public const string PropRight = "Right";
        public const string PropBottom = "Bottom";

        private jQueryObject element;
        private SpacingMode mode;
        private SpacingSync onBeforeSync;

        /// <summary>Constructor.</summary>
        [AlternateSignature]
        public extern Spacing();

        /// <summary>Constructor.</summary>
        /// <param name="left">The left value.</param>
        /// <param name="top">The top value.</param>
        /// <param name="right">The right value.</param>
        /// <param name="bottom">The botton value.</param>
        public Spacing(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the left spacing value.</summary>
        public int Left
        {
            get { return (int) Get(PropLeft, 0); }
            set { Set(PropLeft, FormatValue(value), 0); }
        }

        /// <summary>Gets or sets the top spacing value.</summary>
        public int Top
        {
            get { return (int) Get(PropTop, 0); }
            set { Set(PropTop, FormatValue(value), 0); }
        }

        /// <summary>Gets or sets the right spacing value.</summary>
        public int Right
        {
            get { return (int) Get(PropRight, 0); }
            set { Set(PropRight, FormatValue(value), 0); }
        }

        /// <summary>Gets or sets the bottom spacing value.</summary>
        public int Bottom
        {
            get { return (int) Get(PropBottom, 0); }
            set { Set(PropBottom, FormatValue(value), 0); }
        }
        #endregion

        #region Properties : Calculated
        /// <summary>Gets the sum of the Left and Right.</summary>
        public int HorizontalOffset { get { return Left + Right; } }

        /// <summary>Gets the sum of the Top and Bottom.</summary>
        public int VerticalOffset { get { return Top + Bottom; } }
        #endregion

        #region Methods
        /// <summary>Gets the value for the specified edge.</summary>
        /// <param name="edge">The edge to retrieve the value for.</param>
        public int GetValue(Edge edge) { return (int)PropertyRefFromEdge(edge).Value; }

        /// <summary>Sets the value for the specified edge.</summary>
        /// <param name="edge">The edge to write the value for.</param>
        /// <param name="value">The edge value.</param>
        public void SetValue(Edge edge, int value) { PropertyRefFromEdge(edge).Value = value; }

        /// <summary>Assigns the value to all edge.</summary>
        /// <param name="value">The edge value to assign.</param>
        [AlternateSignature]
        public extern void Uniform(int value);

        /// <summary>Assigns the value to all edge.</summary>
        /// <param name="leftRight">The left and right edge values to assign.</param>
        /// <param name="topBottom">The top and bottom edge values to assign.</param>
        public void Uniform(int leftRight, int topBottom)
        {
            if (Script.IsNullOrUndefined(topBottom)) topBottom = leftRight;
            Left = leftRight;
            Right = leftRight;
            Top = topBottom;
            Bottom = topBottom;
        }

        /// <summary>Copies the values from the given Spacing object.</summary>
        /// <param name="spacing">The object to copy from.</param>
        public void CopyValues(Spacing spacing)
        {
            Left = spacing.Left;
            Top = spacing.Top;
            Right = spacing.Right;
            Bottom = spacing.Bottom;
        }

        /// <summary>Gets the property-ref for the specified edge.</summary>
        /// <param name="edge">The edge to retrieve the property ref for.</param>
        public PropertyRef PropertyRefFromEdge(Edge edge)
        {
            switch (edge)
            {
                case Edge.Left: return GetPropertyRef(PropLeft);
                case Edge.Top: return GetPropertyRef(PropTop);
                case Edge.Right: return GetPropertyRef(PropRight);
                case Edge.Bottom: return GetPropertyRef(PropBottom);
                default: return null;
            }
        }

        /// <summary>Converts the specified property-ref to an edge.</summary>
        /// <param name="propertyName">The name of the property.</param>
        public static Edge ToEdge(string propertyName)
        {
            if (propertyName == PropLeft) return Edge.Left;
            if (propertyName == PropTop) return Edge.Top;
            if (propertyName == PropRight) return Edge.Right;
            if (propertyName == PropBottom) return Edge.Bottom;
            throw new Exception(string.Format("Property '{0}' cannot be converted to an edge.", propertyName));
        }

        public override string ToString()
        {
            return string.Format("[Left:{0},Top:{1},Right:{2},Bottom:{3}]", Left, Top, Right, Bottom);
        }

        /// <summary>Syncs the bound element (if the object is bound to an element).</summary>
        public void SyncElement()
        {
            SyncCss(Edge.Left);
            SyncCss(Edge.Top);
            SyncCss(Edge.Right);
            SyncCss(Edge.Bottom);
        }
        #endregion

        #region Methods : Static
        /// <summary>Factory creator (provides syncing behavior).</summary>
        /// <param name="element">The element the spacing pertains to.</param>
        /// <param name="mode">The mode to use when syncing</param>
        [AlternateSignature]
        public extern static Spacing Create(jQueryObject element, SpacingMode mode);

        /// <summary>Factory creator (provides syncing behavior).</summary>
        /// <param name="element">The element the spacing pertains to.</param>
        /// <param name="mode">The mode to use when syncing</param>
        /// <param name="onBeforeSync">Invoked before the element is updated (use to alter the value).</param>
        public static Spacing Create(jQueryObject element, SpacingMode mode, SpacingSync onBeforeSync)
        {
            Spacing spacing = new Spacing();

            spacing.element = element;
            spacing.mode = mode;
            spacing.onBeforeSync = onBeforeSync;
            if (!Script.IsNullOrUndefined(element)) spacing.InitializeSyncing();

            return spacing;
        }
        #endregion

        #region Internal
        private static int FormatValue(int value)
        {
            return Script.IsNullOrUndefined(value) ? 0 : value;
        }

        private void InitializeSyncing()
        {
            PropertyChanged += delegate(object sender, PropertyChangedEventArgs args)
                                   {
                                       try
                                       {
                                           SyncCss(ToEdge(args.Property.Name));
                                       }
                                       catch (Exception)
                                       {
                                           // Ignore - non supported property.
                                       }
                                   };
        }

        private void SyncCss(Edge edge)
        {
            // Setup initial conditions.
            int value = GetValue(edge);

            // Run the value through the modifier delegate.
            if (!Script.IsNullOrUndefined(onBeforeSync))
            {
                value = onBeforeSync(edge, value);
            }

            // Apply the CSS.
            string attribute = string.Format("{0}-{1}", 
                                             mode.ToString().ToLowerCase(), 
                                             edge.ToString().ToLowerCase());
            element.CSS(attribute, value + Css.Px);
        }
        #endregion
    }
}
