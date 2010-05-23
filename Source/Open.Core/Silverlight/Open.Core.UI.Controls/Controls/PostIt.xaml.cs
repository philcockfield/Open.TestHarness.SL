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
using Open.Core.Common;
using T = Open.Core.UI.Controls.PostIt;

namespace Open.Core.UI.Controls
{
    /// <summary>Renders a simple post-it.</summary>
    public partial class PostIt : UserControl
    {
        #region Head
        public PostIt()
        {
            InitializeComponent();
            root.DataContext = this;
        }
        #endregion

        #region Dependency Properties
        
        /// <summary>Gets or sets the text in the post-it.</summary>
        public string Text
        {
            get { return (string) (GetValue(TextProperty)); }
            set { SetValue(TextProperty, value); }
        }
        /// <summary>Gets or sets the text in the post-it.</summary>
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<T>(m => m.Text),
                typeof (string),
                typeof (T),
                new PropertyMetadata(null));


        /// <summary>Gets or sets the angle of the post-it.</summary>
        public double Angle
        {
            get { return (double) (GetValue(AngleProperty)); }
            set { SetValue(AngleProperty, value); }
        }
        /// <summary>Gets or sets the angle of the post-it.</summary>
        public static readonly DependencyProperty AngleProperty =
            DependencyProperty.Register(
                LinqExtensions.GetPropertyName<PostIt>(m => m.Angle),
                typeof (double),
                typeof (T),
                new PropertyMetadata(0d));
        #endregion
    }
}