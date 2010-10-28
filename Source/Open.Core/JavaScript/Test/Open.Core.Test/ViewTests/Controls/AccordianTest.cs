using System;
using jQueryApi;
using Open.Testing;

namespace Open.Core.Test.ViewTests.Controls
{
    public class AccordianTest
    {
        #region Head
        private jQueryObject divContainer;
        private bool isInitialized;

        public void ClassInitialize()
        {
            divContainer = TestHarness.AddElement();
            Css.SetSize(divContainer, 200, 300);

            string url = "/Samples/AccordianTest";
            Log.Info("Downloading HTML. " + Html.ToHyperlink(url));
            jQuery.Get(url, delegate(object data)
                                                     {
                                                         Log.Success("HTML downloaded.");
                                                         divContainer.Append(data.ToString());
                                                         isInitialized = true;
                                                         TestHarness.UpdateLayout();
                                                     });

            divContainer.Empty();
        }
        #endregion

        #region Methods
        public void Initialize_Accordian()
        {
            if (!isInitialized) return;

            Script.Literal( @"
$(function() {
		$( '#accordion' ).accordion({
			fillSpace: true
		});
	})");

//            Script.Literal(script);

        }
        #endregion
    }
}
