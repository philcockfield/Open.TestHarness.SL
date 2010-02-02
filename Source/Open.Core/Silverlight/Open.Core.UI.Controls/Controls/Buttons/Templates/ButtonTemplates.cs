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

namespace Open.Core.UI.Controls
{
    /// <summary>Index of common templates used for buttons.</summary>
    public class ButtonTemplates : ResourcesBase
    {
        private ButtonTemplates(){}
        public static readonly ButtonTemplates Instance = new ButtonTemplates();
        public override ResourceDictionary Dictionary { get { return GetResourceDictionarySingleton("/Controls/Buttons/Templates/ButtonTemplates.xaml"); } }

        #region Properties - Templates
        /// <summary>Gets the default simple template used for a button background.</summary>
        public static DataTemplate SimpleButtonBackground { get { return Instance.GetDataTemplate("SimpleButtonBackground"); } }

        /// <summary>Gets a simple square (transparent) hit target.</summary>
        public static DataTemplate SquareButtonHitTarget { get { return Instance.GetDataTemplate("SquareButtonHitTarget"); } }

        /// <summary>Gets the hit target shape for the Remove button.</summary>
        public static DataTemplate RemoveButtonHitTarget { get { return Instance.GetDataTemplate("RemoveButtonHitTarget"); } }
        #endregion
    }
}
