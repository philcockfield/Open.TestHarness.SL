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
                             where p.Type.IsEnum || p.Type.IsA(typeof(bool))
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
            if (parameter.Type.IsEnum)
            {
                PopulateEnumDropdown(viewModel, parameter);
            }
            else if (parameter.Type.IsA(typeof(bool)))
            {
                PopulateBooleanDropdown(viewModel, parameter);
            }

            // Wire up events.
            Action syncParameterValue = () => { parameter.Value = viewModel.SelectedItemValue; };
            viewModel.SelectionChanged += delegate { syncParameterValue(); };

            // Finish up.
            syncParameterValue();
            return viewModel;
        }

        private static void PopulateEnumDropdown(ComboBoxViewModel viewModel, ViewTestParameter parameter)
        {
            foreach (var value in parameter.Type.GetEnumValues())
            {
                viewModel.Add(value.ToString(), value);
            }
            SelectDefaultValue(viewModel, parameter.Info.DefaultValue);
        }

        private static void PopulateBooleanDropdown(ComboBoxViewModel viewModel, ViewTestParameter parameter)
        {
            viewModel.Add("True", true);
            viewModel.Add("False", false);

            var defaultValue = parameter.Info.DefaultValue;
            SelectDefaultValue(
                            viewModel,
                            defaultValue is DBNull ? false : defaultValue);
        }

        private static void SelectDefaultValue(ComboBoxViewModel viewModel, object defaultValue)
        {
            var hasDefault = viewModel.Items.Count(m => Equals(m.Value, defaultValue)) > 0;
            if (hasDefault)
            {
                viewModel.SelectValue(defaultValue);
            }
            else
            {
                viewModel.SelectFirst();
            }
        }
        #endregion
    }
}
