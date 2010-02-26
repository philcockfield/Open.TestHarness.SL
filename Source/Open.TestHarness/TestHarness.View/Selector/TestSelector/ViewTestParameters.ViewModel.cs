using System;
using System.Linq;
using System.Collections.ObjectModel;
using Open.Core.Common;
using Open.Core.UI.Controls;
using Open.TestHarness.Model;

namespace Open.TestHarness.View.Selector
{
    /// <summary>Renders editable parameters for a view-test.</summary>
    public class ViewTestParametersViewModel : ViewModelBase
    {
        #region Head

        public ViewTestParametersViewModel(ViewTest model)
        {
            // Setup initial conditions.
            if (model == null) throw new ArgumentNullException("model");
            EnumDropDowns = new ObservableCollection<ComboBoxViewModel>();

            // Populate with enum combo-boxes.
            var enumParams = from p in model.Parameters.Items
                             where p.Type.IsA(typeof (Enum))
                             select p;
            foreach (var parameter in enumParams)
            {
                EnumDropDowns.Add(CreateComboBox(parameter));
            }
        }
        #endregion

        #region Properties
        /// <summary>Gets the collection of view-models for the enum parameters.</summary>
        public ObservableCollection<ComboBoxViewModel> EnumDropDowns { get; set; }
        #endregion

        #region Internal
        private static ComboBoxViewModel CreateComboBox(ViewTestParameter parameter)
        {
            // Setup initial conditions.
            var viewModel = new ComboBoxViewModel
                                {
                                    ToolTip = string.Format("Parameter: {0}", parameter.Info.Name)
                                };

            // Populate with values.
            foreach (var value in parameter.Type.GetEnumValues())
            {
                viewModel.Add(value.ToString(), value);
            }
            viewModel.SelectFirst();

            // Wire up events.
            Action syncParameterValue = () => { parameter.Value = viewModel.SelectedItemValue; };
            viewModel.SelectionChanged += delegate { syncParameterValue(); };

            // Finish up.
            syncParameterValue();
            return viewModel;
        }
        #endregion
    }
}
