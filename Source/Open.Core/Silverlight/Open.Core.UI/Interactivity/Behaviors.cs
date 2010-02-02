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
using System.Windows.Interactivity;

namespace Open.Core.Common.AttachedBehavior
{
    /// <summary>Index of behaviors.</summary>
    public static class Behaviors
    {
        #region Dependency Properties
        public static readonly DependencyProperty SyncSizeRatioProperty =
            DependencyProperty.RegisterAttached(
                "SyncSizeRatio",
                typeof(SyncSizeRatio),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static SyncSizeRatio GetSyncSizeRatio(FrameworkElement element) { return (SyncSizeRatio)element.GetValue(SyncSizeRatioProperty); }
        public static void SetSyncSizeRatio(FrameworkElement element, SyncSizeRatio value) { element.SetValue(SyncSizeRatioProperty, value); }

        public static readonly DependencyProperty TextMouseHighlightProperty =
            DependencyProperty.RegisterAttached(
                "TextMouseHighlight",
                typeof(TextMouseHighlight),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static TextMouseHighlight GetTextMouseHighlight(FrameworkElement element) { return (TextMouseHighlight)element.GetValue(TextMouseHighlightProperty); }
        public static void SetTextMouseHighlight(FrameworkElement element, TextMouseHighlight value) { element.SetValue(TextMouseHighlightProperty, value); }

        public static readonly DependencyProperty CommonWidthProperty =
            DependencyProperty.RegisterAttached(
                "CommonWidth",
                typeof(CommonWidth),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static CommonWidth GetCommonWidth(FrameworkElement element) { return (CommonWidth)element.GetValue(CommonWidthProperty); }
        public static void SetCommonWidth(FrameworkElement element, CommonWidth value) { element.SetValue(CommonWidthProperty, value); }

        public static readonly DependencyProperty ClipToBoundsProperty =
            DependencyProperty.RegisterAttached(
                "ClipToBounds",
                typeof(ClipToBounds),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static ClipToBounds GetClipToBounds(FrameworkElement element) { return (ClipToBounds)element.GetValue(ClipToBoundsProperty); }
        public static void SetClipToBounds(FrameworkElement element, ClipToBounds value) { element.SetValue(ClipToBoundsProperty, value); }

        public static readonly DependencyProperty DraggableProperty =
            DependencyProperty.RegisterAttached(
                "Draggable",
                typeof(Draggable),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static Draggable GetDraggable(UIElement element) { return (Draggable)element.GetValue(DraggableProperty); }
        public static void SetDraggable(UIElement element, Draggable value) { element.SetValue(DraggableProperty, value); }

        public static readonly DependencyProperty PositionSliderProperty =
            DependencyProperty.RegisterAttached(
                "PositionSlider",
                typeof(PositionSlider),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static PositionSlider GetPositionSlider(UIElement element) { return (PositionSlider)element.GetValue(PositionSliderProperty); }
        public static void SetPositionSlider(UIElement element, PositionSlider value) { element.SetValue(PositionSliderProperty, value); }

        public static readonly DependencyProperty UpdateOnKeyPressProperty =
            DependencyProperty.RegisterAttached(
                "UpdateOnKeyPress",
                typeof(UpdateOnKeyPress),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static UpdateOnKeyPress GetUpdateOnKeyPress(TextBox element) { return (UpdateOnKeyPress)element.GetValue(UpdateOnKeyPressProperty); }
        public static void SetUpdateOnKeyPress(TextBox element, UpdateOnKeyPress value) { element.SetValue(UpdateOnKeyPressProperty, value); }

        public static readonly DependencyProperty AutoRowDefinitionsProperty =
            DependencyProperty.RegisterAttached(
                "AutoRowDefinitions",
                typeof(AutoRowDefinitions),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static AutoRowDefinitions GetAutoRowDefinitions(Grid element) { return (AutoRowDefinitions)element.GetValue(AutoRowDefinitionsProperty); }
        public static void SetAutoRowDefinitions(Grid element, AutoRowDefinitions value) { element.SetValue(AutoRowDefinitionsProperty, value); }

        public static readonly DependencyProperty EnabledOpacityProperty =
            DependencyProperty.RegisterAttached(
                "EnabledOpacity",
                typeof(EnabledOpacity),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static EnabledOpacity GetEnabledOpacity(Control element) { return (EnabledOpacity)element.GetValue(EnabledOpacityProperty); }
        public static void SetEnabledOpacity(Control element, EnabledOpacity value) { element.SetValue(EnabledOpacityProperty, value); }

        public static readonly DependencyProperty SelectTextOnFocusProperty =
            DependencyProperty.RegisterAttached(
                "SelectTextOnFocus",
                typeof(SelectTextOnFocus),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static SelectTextOnFocus GetSelectTextOnFocus(TextBox element) { return (SelectTextOnFocus)element.GetValue(SelectTextOnFocusProperty); }
        public static void SetSelectTextOnFocus(TextBox element, SelectTextOnFocus value) { element.SetValue(SelectTextOnFocusProperty, value); }

        public static readonly DependencyProperty MouseWheelScrollerProperty =
            DependencyProperty.RegisterAttached(
                "MouseWheelScroller",
                typeof(MouseWheelScroller),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
                public static MouseWheelScroller GetMouseWheelScroller(ScrollViewer element) { return (MouseWheelScroller)element.GetValue(MouseWheelScrollerProperty); }
                public static void SetMouseWheelScroller(ScrollViewer element, MouseWheelScroller value) { element.SetValue(MouseWheelScrollerProperty, value); }

        public static readonly DependencyProperty ListBoxMouseWheelScrollerProperty =
            DependencyProperty.RegisterAttached(
                "ListBoxMouseWheelScroller",
                typeof(ListBoxMouseWheelScroller),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
                public static ListBoxMouseWheelScroller GetListBoxMouseWheelScroller(ListBox element) { return (ListBoxMouseWheelScroller)element.GetValue(ListBoxMouseWheelScrollerProperty); }
                public static void SetListBoxMouseWheelScroller(ListBox element, ListBoxMouseWheelScroller value) { element.SetValue(ListBoxMouseWheelScrollerProperty, value); }

        public static readonly DependencyProperty DataGridFillerColumnProperty =
            DependencyProperty.RegisterAttached(
                "DataGridFillerColumn",
                typeof(DataGridFillerColumn),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
                public static DataGridFillerColumn GetDataGridFillerColumn(DataGrid element) { return (DataGridFillerColumn)element.GetValue(DataGridFillerColumnProperty); }
                public static void SetDataGridFillerColumn(DataGrid element, DataGridFillerColumn value) { element.SetValue(DataGridFillerColumnProperty, value); }
        #endregion

        #region Dependency Properties - Animation
        public static readonly DependencyProperty SizeAnimationProperty =
            DependencyProperty.RegisterAttached(
                "SizeAnimation",
                typeof(SizeAnimation),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static SizeAnimation GetSizeAnimation(FrameworkElement element) { return (SizeAnimation)element.GetValue(SizeAnimationProperty); }
        public static void SetSizeAnimation(FrameworkElement element, SizeAnimation value) { element.SetValue(SizeAnimationProperty, value); }

        public static readonly DependencyProperty RotateAnimationProperty =
            DependencyProperty.RegisterAttached(
                "RotateAnimation",
                typeof(RotateAnimation),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static RotateAnimation GetRotateAnimation(FrameworkElement element) { return (RotateAnimation)element.GetValue(RotateAnimationProperty); }
        public static void SetRotateAnimation(FrameworkElement element, RotateAnimation value) { element.SetValue(RotateAnimationProperty, value); }

        public static readonly DependencyProperty OpacityAnimationProperty =
            DependencyProperty.RegisterAttached(
                "OpacityAnimation",
                typeof(OpacityAnimation),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static OpacityAnimation GetOpacityAnimation(FrameworkElement element) { return (OpacityAnimation)element.GetValue(OpacityAnimationProperty); }
        public static void SetOpacityAnimation(FrameworkElement element, OpacityAnimation value) { element.SetValue(OpacityAnimationProperty, value); }
        #endregion

        #region Dependency Property - PersistentSize
        /// <summary>Gets or sets an attached behavior that handles persisting the size of an element.</summary>
        public static readonly DependencyProperty PersistentSizeProperty =
            DependencyProperty.RegisterAttached(
                "PersistentSize", 
                typeof(PersistentSize), 
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static PersistentSize GetPersistentSize(FrameworkElement element){return (PersistentSize)element.GetValue(PersistentSizeProperty);}
        public static void SetPersistentSize(FrameworkElement element, PersistentSize value){element.SetValue(PersistentSizeProperty, value);}


        /// <summary>Gets or sets an attached behavior that handles persisting the size of a Grid column.</summary>
        public static readonly DependencyProperty PersistentColumnSizeProperty =
            DependencyProperty.RegisterAttached(
                "PersistentColumnSize",
                typeof(PersistentColumnSize),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static PersistentSize GetPersistentColumnSize(FrameworkElement element) { return (PersistentColumnSize)element.GetValue(PersistentColumnSizeProperty); }
        public static void SetPersistentColumnSize(FrameworkElement element, PersistentColumnSize value) { element.SetValue(PersistentColumnSizeProperty, value); }


        /// <summary>Gets or sets an attached behavior that handles persisting the size of a Grid row.</summary>
        public static readonly DependencyProperty PersistentRowSizeProperty =
            DependencyProperty.RegisterAttached(
                "PersistentRowSize",
                typeof(PersistentRowSize),
                typeof(Behaviors),
                new PropertyMetadata(HandleAttachedBehaviorPropertyChanged));
        public static PersistentSize GetPersistentRowSize(FrameworkElement element) { return (PersistentRowSize)element.GetValue(PersistentRowSizeProperty); }
        public static void SetPersistentRowSize(FrameworkElement element, PersistentRowSize value) { element.SetValue(PersistentRowSizeProperty, value); }
        #endregion

        #region Methods
        /// <summary>Generic handler for updating an AttachedBehavior when it's dependency property changes.</summary>
        /// <param name="o">The dependnecy object.</param>
        /// <param name="e">Event arguments.</param>
        public static void HandleAttachedBehaviorPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var element = o as UIElement;
            if (element == null) return;

            if (e.OldValue != null)
            {
                var behavior = e.OldValue as IAttachedObject;
                if (behavior != null) behavior.Detach();
            }
            if (e.NewValue != null)
            {
                var behavior = e.NewValue as IAttachedObject;
                if (behavior != null) behavior.Attach(element);
            }
        }
        #endregion
    }
}
