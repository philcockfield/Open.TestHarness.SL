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
using Open.Core.Common;

namespace Open.Core.UI.Silverlight.Test
{
    public class SampleTemplates : ResourcesBase
    {
        public static readonly SampleTemplates Instance = new SampleTemplates();
        public override ResourceDictionary Dictionary { get { return typeof(SampleTemplates).Assembly.GetResourceDictionary("/Resources/SampleTemplates.xaml"); } }

        public static DataTemplate Placeholder1 { get { return Instance.GetDataTemplate("Placeholder1"); } }
        public static DataTemplate Placeholder2 { get { return Instance.GetDataTemplate("Placeholder2"); } }
        public static DataTemplate PlaceholderText { get { return Instance.GetDataTemplate("Placeholder.Text"); } }
        public static DataTemplate TextBox { get { return Instance.GetDataTemplate("TextBox"); } }
        public static DataTemplate Ellipse { get { return Instance.GetDataTemplate("Ellipse"); } }
    }
}
