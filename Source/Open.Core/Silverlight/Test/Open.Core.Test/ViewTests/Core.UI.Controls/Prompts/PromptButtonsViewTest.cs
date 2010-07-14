﻿using System.ComponentModel.Composition;
using Open.Core.Common;
using Open.Core.UI.Controls;

namespace Open.Core.Test.ViewTests.Core.UI.Controls.Prompts
{
    [ViewTestClass]
    public class PromptButtonsViewTest
    {
        #region Head
        [Import]
        public IPromptButtons ButtonBar { get; set; }

        [ViewTest(Default = true, IsVisible = false)]
        public void Initialize(PromptButtonsTestControl control)
        {
            CompositionInitializer.SatisfyImports(this);
            control.content.ViewFactory = ButtonBar;
        }
        #endregion

        #region Tests
        [ViewTest]
        public void Change_Configuration(PromptButtonsTestControl control, PromptButtonConfiguration configuration = PromptButtonConfiguration.YesNoCancel)
        {
            ButtonBar.Configuration = configuration;
        }
        #endregion
    }
}
