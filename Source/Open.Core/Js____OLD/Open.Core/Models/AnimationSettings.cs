using jQueryApi;

namespace Open.Core
{
    /// <summary>Common property settings for an animnation.</summary>
    public class AnimationSettings : ModelBase
    {
        #region Head
        public const string PropDuration = "Duration";
        public const string PropEasing = "Easing";

        public const double DefaultDuration = 0.3;
        #endregion

        #region Properties
        /// <summary>Gets or sets the easing effect to apply to the animation.</summary>
        public EffectEasing Easing
        {
            get { return (EffectEasing)Get(PropEasing, EffectEasing.Swing); }
            set { Set(PropEasing, value, EffectEasing.Swing); }
        }

        /// <summary>Gets or sets the duration of the animation.</summary>
        public double Duration
        {
            get { return (double)Get(PropDuration, DefaultDuration); }
            set { Set(PropDuration, value, DefaultDuration); }
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return string.Format("[Duration:{0} secs; Easing:{1}]", Duration, Easing.ToString());
        }

        /// <summary>Converts the duration from seconds to milliseconds.</summary>
        public int ToMsecs()
        {
            return Helper.Time.ToMsecs(Duration); 
        }
        #endregion
    }
}
