using System;

namespace Open.Core.Test.Samples
{
    public class SamplePart : Part
    {
        public static SamplePart Create() { return new SamplePart(); }

        protected override void OnInitialize(Action callback)
        {
            Log.Warning("<b>SamplePart - OnInitialize</b>");
            DelayedAction.Invoke(1, callback);
        }
    }
}
