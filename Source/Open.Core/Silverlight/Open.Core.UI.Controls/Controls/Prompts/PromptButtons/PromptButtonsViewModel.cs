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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows;
using Open.Core.Common;
using Open.Core.UI.Controls.Assets;
using T = Open.Core.UI.Controls.PromptButtonsViewModel;

namespace Open.Core.UI.Controls
{
    [Export(typeof(IPromptButtons))]
    public class PromptButtonsViewModel : ViewModelBase, IPromptButtons
    {
        #region Head
        private static ButtonImporter importer;
        private const PromptButtonConfiguration DefaultConfiguration = PromptButtonConfiguration.OkCancel;
        private const double DefaultLargeSpacing = 8;
        private const double DefaultSmallSpacing = 2;

        /// <summary>Constructor.</summary>
        public PromptButtonsViewModel()
        {
            // Setup initial conditions.
            if (importer == null) importer = new ButtonImporter();

            // Create buttons.
            AcceptButton = CreateButton();
            DeclineButton = CreateButton();
            CancelButton = CreateButton();
            NextButton = CreateButton();
            BackButton = CreateButton();
            Buttons = new[] { AcceptButton, DeclineButton, CancelButton, NextButton, BackButton };

            // Finish up.
            UpdateConfiguration(Configuration);
        }
        #endregion

        #region Properties : IPromptButtons
        public PromptButtonConfiguration Configuration
        {
            get { return Property.GetValue<T, PromptButtonConfiguration>(m => m.Configuration, DefaultConfiguration); }
            set
            {
                if (Property.SetValue<T, PromptButtonConfiguration>(m => m.Configuration, value, DefaultConfiguration))
                {
                    UpdateConfiguration(value);
                }
            }
        }

        public IButton AcceptButton { get; private set; }
        public IButton DeclineButton { get; private set; }
        public IButton CancelButton { get; private set; }
        public IButton NextButton { get; private set; }
        public IButton BackButton { get; private set; }

        public double LargeSpacing
        {
            get { return Property.GetValue<T, double>(m => m.LargeSpacing, DefaultLargeSpacing); }
            set { Property.SetValue<T, double>(m => m.LargeSpacing, value, DefaultLargeSpacing); }
        }

        public double SmallSpacing
        {
            get { return Property.GetValue<T, double>(m => m.SmallSpacing, DefaultSmallSpacing); }
            set { Property.SetValue<T, double>(m => m.SmallSpacing, value, DefaultSmallSpacing); }
        }
        #endregion

        #region Properties : ViewModel
        public IEnumerable<IButton> Buttons { get; private set; }
        #endregion

        #region Methods
        public IButton GetButton(PromptResult buttonType)
        {
            switch (buttonType)
            {
                case PromptResult.Accept:return AcceptButton;
                case PromptResult.Decline: return DeclineButton;
                case PromptResult.Cancel: return CancelButton;
                case PromptResult.Back: return BackButton;
                case PromptResult.Next: return NextButton;

                default: throw new NotSupportedException(buttonType.ToString());
            }
        }

        public FrameworkElement CreateView()
        {
            return new PromptButtons { ViewModel = this };
        }
        #endregion

        #region Internal
        private static IButton CreateButton()
        {
            var button = importer.ButtonFactory.CreateExport().Value;
            return button;
        }

        private void UpdateConfiguration(PromptButtonConfiguration configuration)
        {
            // Setup initial conditions.
            HideAllButtons();
            ClearAllButtonMargins();

            // Set the specified configuration.
            switch (configuration)
            {
                case PromptButtonConfiguration.YesNo:
                    AcceptButton.IsVisible = true;
                    DeclineButton.IsVisible = true;

                    AcceptButton.Label = StringLibrary.Prompt_Yes;
                    DeclineButton.Label = StringLibrary.Prompt_No;

                    SetSmallSpacing(DeclineButton);
                    break;

                case PromptButtonConfiguration.YesNoCancel:
                    AcceptButton.IsVisible = true;
                    DeclineButton.IsVisible = true;
                    CancelButton.IsVisible = true;

                    AcceptButton.Label = StringLibrary.Prompt_Yes;
                    DeclineButton.Label = StringLibrary.Prompt_No;
                    CancelButton.Label = StringLibrary.Prompt_Cancel;

                    SetSmallSpacing(DeclineButton);
                    SetLargeSpacing(CancelButton);
                    break;

                case PromptButtonConfiguration.Ok:
                    AcceptButton.IsVisible = true;
                    AcceptButton.Label = StringLibrary.Prompt_OK;
                    break;

                case PromptButtonConfiguration.OkCancel:
                    AcceptButton.IsVisible = true;
                    CancelButton.IsVisible = true;

                    AcceptButton.Label = StringLibrary.Prompt_OK;
                    CancelButton.Label = StringLibrary.Prompt_Cancel;

                    SetSmallSpacing(CancelButton);
                    break;

                case PromptButtonConfiguration.Done:
                    CancelButton.IsVisible = true;
                    CancelButton.Label = StringLibrary.Prompt_Done;
                    break;

                case PromptButtonConfiguration.BackNext:
                    BackButton.IsVisible = true;
                    NextButton.IsVisible = true;

                    BackButton.Label = StringLibrary.Prompt_Back;
                    NextButton.Label = StringLibrary.Prompt_Next;

                    SetSmallSpacing(NextButton);
                    break;

                case PromptButtonConfiguration.BackNextCancel:
                    BackButton.IsVisible = true;
                    NextButton.IsVisible = true;
                    CancelButton.IsVisible = true;

                    BackButton.Label = StringLibrary.Prompt_Back;
                    NextButton.Label = StringLibrary.Prompt_Next;
                    CancelButton.Label = StringLibrary.Prompt_Cancel;

                    SetSmallSpacing(NextButton);
                    SetLargeSpacing(CancelButton);
                    break;
                
                default: throw new ArgumentOutOfRangeException(configuration.ToString());
            }
        }

        private void HideAllButtons()
        {
            foreach (var button in Buttons) { button.IsVisible = false; }
        }

        private void ClearAllButtonMargins( )
        {
            foreach (var button in Buttons) { button.Margin = default(Thickness); }
        }

        private void SetLargeSpacing(IButton button) { button.Margin = new Thickness(LargeSpacing, 0, 0, 0); }
        private void SetSmallSpacing(IButton button) { button.Margin = new Thickness(SmallSpacing, 0, 0, 0); }
        #endregion

        public class ButtonImporter : ImporterBase
        {
            [Import]
            public ExportFactory<IButton> ButtonFactory { get; set; }
        }
    }
}
