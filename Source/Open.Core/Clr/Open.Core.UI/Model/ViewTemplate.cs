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
using System.ComponentModel;
using System.Windows;
using T = Open.Core.Common.ViewTemplate;
using System.Windows.Controls;

namespace Open.Core.Common
{
    /// <summary>Represents the template of a view, along with it's supporting View-Model.</summary>
    /// <example>
    ///    The following shows a content control that is binding to a 'ViewTemplate' that has
    ///    been exposed on the parent ViewModel (DataContext) via a property named 'Content':
    /// 
    ///         <ContentControl 
    ///                DataContext="{Binding Content}"
    ///                ContentTemplate="{Binding Template}"
    ///                Style="{Binding StretchedContentStyle}">
    ///            <ContentPresenter />
    ///        </ContentControl>
    /// 
    ///    Including the child ContentPresenter is important to ensure data-binding happens correctly.
    /// </example>
    public class ViewTemplate : ModelBase, IViewTemplate
    {
        #region Head
        private static Style stretchedContentStyle;

        /// <summary>Constructor.</summary>
        public ViewTemplate() { }

        /// <summary>Constructor.</summary>
        /// <param name="template">The XAML template.</param>
        /// <param name="viewModel">The view-model (the logical representation of the view).</param>
        public ViewTemplate(DataTemplate template, INotifyPropertyChanged viewModel)
        {
            Template = template;
            ViewModel = viewModel;
        }

        protected override void OnDisposed()
        {
            base.OnDisposed();
            if (ViewModel != null)
            {
                var disposableViewModel = ViewModel as IDisposable;
                if (disposableViewModel != null) disposableViewModel.Dispose();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets or sets the XAML template.</summary>
        public DataTemplate Template
        {
            get { return GetPropertyValue<T, DataTemplate>(m => m.Template); }
            set { SetPropertyValue<T, DataTemplate>(m => m.Template, value); }
        }

        /// <summary>Gets or sets the view-model (the logical representation of the view).</summary>
        public INotifyPropertyChanged ViewModel
        {
            get { return GetPropertyValue<T, INotifyPropertyChanged>(m => m.ViewModel); }
            set { SetPropertyValue<T, INotifyPropertyChanged>(m => m.ViewModel, value); }
        }

        /// <summary>Gets the XAML style that stretches the child content of a ContentControl (ie. the Template) both vertically and horizontally.</summary>
        public static Style StretchedContentStyle
        {
            get
            {
                if (stretchedContentStyle == null) stretchedContentStyle = CreateStretchedContentStyle();
                return stretchedContentStyle;
            }
        }
        #endregion

        #region Methods
        /// <summary>Applies this template, and view-model, to the given ContentControl (with stretched content).</summary>
        /// <param name="control">The control to apply this to.</param>
        public void ApplyTo(ContentControl control)
        {
            ApplyTo(control, true);
        }

        /// <summary>Applies this template, and view-model, to the given ContentControl.</summary>
        /// <param name="control">The control to apply this to.</param>
        /// <param name="stretchContent">Flag indicating if the content-alignment should be set to stretch.</param>
        public void ApplyTo(ContentControl control, bool stretchContent)
        {
            if (control == null) return;
            control.DataContext = this;
            control.ContentTemplate = Template;
            if (stretchContent)
            {
                control.HorizontalContentAlignment = HorizontalAlignment.Stretch;
                control.VerticalContentAlignment = VerticalAlignment.Stretch;
            }
        }
        #endregion

        #region Internal
        private static Style CreateStretchedContentStyle( )
        {
            var style = new Style(typeof(ContentControl));
            style.Setters.Add(new Setter(Control.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
            style.Setters.Add(new Setter(Control.VerticalContentAlignmentProperty, HorizontalAlignment.Stretch));
            return style;
        }
        #endregion
    }
}
