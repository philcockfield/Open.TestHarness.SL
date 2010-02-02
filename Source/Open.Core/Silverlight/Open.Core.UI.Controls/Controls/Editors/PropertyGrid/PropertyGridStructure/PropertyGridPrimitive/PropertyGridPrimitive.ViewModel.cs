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

using System.Collections.ObjectModel;
using Open.Core.Common.Collection;

namespace Open.Core.Common.Controls.Editors.PropertyGridStructure
{
    /// <summary>The logical model for the 'PropertyGridPrimitive' view.</summary>
    public class PropertyGridPrimitiveViewModel : ViewModelBase
    {
        #region Head
        private readonly ObservableCollection<PropertyModel> model;
        private readonly ObservableCollectionWrapper<PropertyModel, PropertyViewModel> properties;

        public PropertyGridPrimitiveViewModel(ObservableCollection<PropertyModel> model)
        {
            this.model = model;
            properties = new ObservableCollectionWrapper<PropertyModel, PropertyViewModel>(
                                                                                model, 
                                                                                 item => new PropertyViewModel(item));
        }

        protected override void Dispose(bool isDisposing)
        {
            base.Dispose(isDisposing);
            if (isDisposing)
            {
                properties.Dispose();
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of properties in the grid.</summary>
        public ObservableCollectionWrapper<PropertyModel, PropertyViewModel> Properties
        {
            get { return properties; }
        }
        #endregion
    }
}
