using System;
using System.Collections;

namespace Open.Core.Test.Samples
{
    public class SamplePart : Part
    {
        #region Head
        private static int instanceCount = 0;
        private int instanceId = -1;
        #endregion

        #region Properties
        public int InstanceId { get { return instanceId; } }
        #endregion

        #region Methods
        public static SamplePart Create()
        {
//            throw new Exception("MY FOO"); //TEMP 
            return new SamplePart();
        }

        protected override void OnInitialize(Action callback)
        {
            // Increment static counter and set the instance id.
            instanceCount++;
            instanceId = instanceCount;
            Log.WriteIcon(string.Format("<b>SamplePart</b> - OnInitialize ({0})", InstanceId), Icons.SilkEmoticonSmile);

            // Insert some sample content.
            Helper.Template.GetAsync("/Samples/SamplePart", "#samplePartTemplate", delegate(Template template)
                                        {
                                            Dictionary dic = new Dictionary();
                                            dic["instanceId"] = InstanceId;

                                            Container.Empty();
                                            template.AppendTo(Container, dic);

                                            callback();
                                        });
        }
        #endregion
    }
}
