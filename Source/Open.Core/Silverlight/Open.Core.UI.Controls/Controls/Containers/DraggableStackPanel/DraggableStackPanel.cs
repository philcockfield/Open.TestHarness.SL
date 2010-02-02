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
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Diagnostics;
using Open.Core.Common;
using Open.Core.Common.AttachedBehavior;

namespace Open.Core.UI.Silverlight.Controls
{
    /// <summary>Stacks a set of child items along an axis (horizontal or vertical) and makes each child dragable enabling re-ordering.</summary>
    public class DraggableStackPanel : Canvas
    {
        #region Head
        public const string PropOrientation = "Orientation";
        public const string PropItemsSource = "ItemsSource";
        public const string PropEasing = "Easing";
        public const string PropSlideDuration = "SlideDuration";
        public const string PropDragContainment = "DragContainment";

        private readonly StackArranger arranger;
        private IList itemsCollection;
        private readonly List<Child> childItems = new List<Child>(); 
        private UIElement currentlyDraggedChild;
        private bool isCollectionChanged;

        public DraggableStackPanel()
        {
            // Setup initial conditions.
            arranger=new StackArranger(
                                    Children, 
                                    Orientation, 
                                    null,
                                    ArrangeChild);

            // Wire up events.
//            SizeChanged += delegate { ArrangeChildren(); };
            LayoutUpdated += delegate { ArrangeChildren(); };
            Loaded += HandleLoaded;
        }
        #endregion

        #region Event Handlers
        void HandleLoaded(object sender, RoutedEventArgs e)
        {
            Loaded -= HandleLoaded;
            ArrangeChildren(true);
        }

        private void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e)
        {
            // Unwire existing collection (if it exists), and wire up the new one.
            WireCollection(itemsCollection as INotifyCollectionChanged, false);
            WireCollection(e.NewValue as INotifyCollectionChanged, true);

            // Store a reference to the value.
            itemsCollection = e.NewValue as IList;
        }

        private void OnOrientationChanged()
        {
            arranger.Orientation = Orientation;
            SyncChildProperties();
            ArrangeChildren();
        }

        void Handle_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            isCollectionChanged = true;

            switch (e.Action)
            {
                // Ignore.
                case NotifyCollectionChangedAction.Reset: break;

                // Animate.
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
//TEMP                     AnimateChange();
                    break;
                
                // Error.
                default: throw new ArgumentOutOfRangeException();
            }
        }

        void Handle_Child_IsDraggingChanged(object sender, EventArgs e)
        {
            // Setup initial conditions.
            var behavior = (Draggable)sender;

            // Store the currently dragged element (or clear if no longer dragging).
            currentlyDraggedChild = behavior.IsDragging ? behavior.AssociatedObject : null;

            // If drag has stopped update the ItemsSource collection to reflect the new order.
            if (currentlyDraggedChild == null) ReorderCollection();

            // Update positioning.
            InvalidateMeasure();
        }

        void Handle_Child_Dragging(object sender, DraggingEventArgs e)
        {
            //TEMP - Do not call this so aggressively.  Calculate when 'ArrangeChildren' should be invoked.
            ArrangeChildren();
        }
        #endregion

        #region Dependency Properties
        /// <summary>Gets or sets the orientation in which child items are stacked within the panel.</summary>
        public Orientation Orientation
        {
            get { return (Orientation)(GetValue(OrientationProperty)); }
            set { SetValue(OrientationProperty, value); }
        }
        /// <summary>Gets or sets the orientation in which child items are stacked within the panel.</summary>
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register(
                PropOrientation,
                typeof(Orientation),
                typeof(DraggableStackPanel),
                new PropertyMetadata(
                            Orientation.Vertical,
                            (sender, e) => ((DraggableStackPanel)sender).OnOrientationChanged()));


        /// <summary>Gets or sets the collection data-source of the master ItemsControl that this panel is housed within.</summary>
        public IEnumerable ItemsSource
        {
            get { return (IEnumerable) (GetValue(ItemsSourceProperty)); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        /// <summary>Gets or sets the collection data-source of the master ItemsControl that this panel is housed within.</summary>
        /// <remarks>
        ///    For updates to be monitored, makes sure the bound value is an ObservableCollection (or some other collection that 
        ///    that implements 'INotifyCollectionChanged').
        /// </remarks>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register(
                PropItemsSource,
                typeof (IEnumerable),
                typeof (DraggableStackPanel),
                new PropertyMetadata(null, (sender, e) => ((DraggableStackPanel)sender).OnItemsSourceChanged(e)));


        /// <summary>Gets or sets the easing function to use when sliding items within the stack.</summary>
        public IEasingFunction Easing
        {
            get { return (IEasingFunction) (GetValue(EasingProperty)); }
            set { SetValue(EasingProperty, value); }
        }
        /// <summary>Gets or sets the easing function to use when sliding items within the stack.</summary>
        public static readonly DependencyProperty EasingProperty =
            DependencyProperty.Register(
                PropEasing,
                typeof (IEasingFunction),
                typeof (DraggableStackPanel),
                new PropertyMetadata(new QuadraticEase{EasingMode = EasingMode.EaseIn}));
        

        /// <summary>Gets or sets the duration of the slide animation when moving items within the stack.</summary>
        public double SlideDuration
        {
            get { return (double) (GetValue(SlideDurationProperty)); }
            set { SetValue(SlideDurationProperty, value); }
        }
        /// <summary>Gets or sets the duration of the slide animation when moving items within the stack.</summary>
        public static readonly DependencyProperty SlideDurationProperty =
            DependencyProperty.Register(
                PropSlideDuration,
                typeof (double),
                typeof (DraggableStackPanel),
                new PropertyMetadata(0.1, (sender, e) => ((DraggableStackPanel)sender).SyncChildProperties()));


        /// <summary>Gets or sets how the child elements are contained within it's parent.</summary>
        public DragContainment DragContainment
        {
            get { return (DragContainment) (GetValue(DragContainmentProperty)); }
            set { SetValue(DragContainmentProperty, value); }
        }
        /// <summary>Gets or sets how the child elements are contained within it's parent.</summary>
        public static readonly DependencyProperty DragContainmentProperty =
            DependencyProperty.Register(
                PropDragContainment,
                typeof (DragContainment),
                typeof (DraggableStackPanel),
                new PropertyMetadata(DragContainment.PixelsWithin, (sender, e) => ((DraggableStackPanel)sender).SyncChildProperties()));
        #endregion
        
        #region Properties - Private
        private bool IsDragging{get{ return currentlyDraggedChild != null;}}

        private IEnumerable<UIElement> OrderedChildElements
        {
            get { return IsDragging ? GetCurrentChildOrder() : Children; }
        }
        #endregion
        
        #region Internal - Child Items
        private void UpdateChildItemsCollection(bool force)
        {
            // Setup initial conditions.
            if (!isCollectionChanged && !force) return;
            isCollectionChanged = false;

            // Remove orphaned child-items (no longer represented in master 'Children' collection).
            foreach (var item in childItems.ToArray())
            {
                if (GetChild(item.Element) == null)
                {
                    item.Dispose();
                    childItems.Remove(item);
                }
            }

            // Add child-items which are in the master 'Children' collection but not yet represented.
            foreach (var child in Children)
            {
                if (childItems.FirstOrDefault(item => child == item.Element) == null)
                {
                    var childItem = new Child(this, child.GetDataContext()){Element = child};
                    childItems.Add(childItem);
                }
            }
        }

        private Child GetChild(UIElement element)
        {
            return childItems.FirstOrDefault(child => child.Element == element);
        }

        private void SyncChildProperties()
        {
            foreach (var item in childItems) { item.SyncPropertyValues(); }
        }
        #endregion

        #region Internal
        private void ArrangeChildren()
        {
            ArrangeChildren(false);
        }

        private void ArrangeChildren(bool force)
        {
            // Setup initial conditions.
            UpdateChildItemsCollection(force);

            // Calculate layout positioning and pass execution to the arranger.
            var newLayout = CalculateNewLayout();
            arranger.Arrange(newLayout);
        }

        private void ArrangeChild(UIElement element, Rect bounds)
        {
            // Setup initial conditions.
            if (element == currentlyDraggedChild) return; // Ignore the element if it's being dragged.

            // Retrieve the corresponding [Child].
            var childItem = GetChild(element);
            if (childItem == null) return;

            // Update size and position.
            StackArranger.UpdateSize(element, bounds);
            childItem.SlideBehavior.Position = new Point(bounds.X, bounds.Y); // NB: Setting this causes the slide animation to occur.
        }

        private List<ElementBounds> CalculateNewLayout()
        {
            // Setup initial conditions.
            arranger.Orientation = Orientation;
            var size = this.GetActualSize();
            List<ElementBounds> newLayout;

            // Perform calcualtion.
            arranger.Children = OrderedChildElements;
            arranger.Measure(size);
            arranger.ArrangeCalculate(size, out newLayout);

            // Finish up.
            return newLayout;
        }

        private IEnumerable<UIElement> GetCurrentChildOrder()
        {
            var list = new List<UIElement>(Children);
            switch (Orientation)
            {
                case Orientation.Vertical: return list.OrderBy(element => GetTop(element)).ToList();
                case Orientation.Horizontal: return list.OrderBy(element => GetLeft(element)).ToList();
                default: throw new NotSupportedException(Orientation.ToString());
            }
        }

        private void ReorderCollection()
        {
            // Setup initial conditions.
            var items = ItemsSource as IList;
            if (items == null) return;
            var children = GetCurrentChildOrder();

            // Populate with current child order.
            items.Clear();
            foreach (var element in children)
            {
                var childItem = childItems.FirstOrDefault(item => item.Model == element.GetDataContext());
                items.Add(childItem.Model);
            }
        }

        private void WireCollection(INotifyCollectionChanged collection, bool add)
        {
            if (collection == null) return;
            if (add)
            {
                collection.CollectionChanged += Handle_CollectionChanged;
            }
            else
            {
                collection.CollectionChanged -= Handle_CollectionChanged;
            }
        }
        #endregion

        private sealed class Child : IDisposable
        {
            #region Head
            private readonly DraggableStackPanel parent;
            private UIElement element;

            public Child(DraggableStackPanel parent, object model)
            {
                this.parent = parent;
                Model = model;
            }
            #endregion

            #region Dispose | Finalize
            ~Child()
            {
                Dispose(false);
            }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool isDisposing)
            {
                // Setup initial conditions.
                if (IsDisposed) return;

                // Perform disposal or managed resources.
                if (isDisposing)
                {
                    DestroyBehaviors();
                }

                // Finish up.
                IsDisposed = true;
            }
            #endregion

            #region Properties
            public bool IsDisposed { get; private set; }
            public object Model { get; private set; }
            public UIElement Element
            {
                get { return element; }
                set
                {
                    // Setup initial conditions.
                    if (value == Element) return;

                    // Destroy existing behaviors.
                    if (Element != null) DestroyBehaviors();

                    // Store value.
                    element = value;

                    // Update corresponding behaviors.
                    DragBehavior = CreateDraggableBehavior(value);
                    SlideBehavior = CreateSlideBehavior(value);
                }
            }

            public Draggable DragBehavior { get; private set; }
            public PositionSlider SlideBehavior { get; private set; }
            #endregion

            #region Methods
            public void SyncPropertyValues()
            {
                SyncDraggableProperties(DragBehavior);
                SyncSlideProperties(SlideBehavior);
            }
            #endregion

            #region Internal
            private Draggable CreateDraggableBehavior(UIElement child)
            {
                var behavior = new Draggable();
                SyncDraggableProperties(behavior);

                Behaviors.SetDraggable(child, behavior);
                behavior.IsDraggingChanged += parent.Handle_Child_IsDraggingChanged;
                behavior.Dragging += parent.Handle_Child_Dragging;

                return behavior;
            }

            private PositionSlider CreateSlideBehavior(UIElement child)
            {
                var behavior = new PositionSlider();
                SyncSlideProperties(behavior);
                Behaviors.SetPositionSlider(child, behavior);
                return behavior;
            }

            private void SyncDraggableProperties(Draggable behavior)
            {
                if (behavior == null) return;
                behavior.DragX = parent.Orientation == Orientation.Horizontal;
                behavior.DragY = parent.Orientation == Orientation.Vertical;
                behavior.DragContainment = parent.DragContainment;
            }

            private void SyncSlideProperties(PositionSlider behavior)
            {
                if (behavior == null) return;
                behavior.Duration = parent.SlideDuration;
            }
            #endregion

            #region Internal - Destroy
            private void DestroyBehaviors()
            {
                DestroyDraggableBehavior();
                DestroySlideBehavior();
            }

            private void DestroyDraggableBehavior()
            {
                if (DragBehavior == null) return;
                DragBehavior.IsDraggingChanged -= parent.Handle_Child_IsDraggingChanged;
                DragBehavior.Dragging -= parent.Handle_Child_Dragging;
                DragBehavior.Detach();
            }

            private void DestroySlideBehavior()
            {
                if (SlideBehavior == null) return;
                SlideBehavior.Detach();
            }
            #endregion
        }
    }
}

