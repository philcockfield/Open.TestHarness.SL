namespace Open.Core.Test
{
    public class SampleView : ViewBase
    {
        public PropertyRef LastPropertyChanged;

        public SampleView() : base(Html.CreateDiv())
        {
            PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) { LastPropertyChanged = args.Property; };
        }
    }
}
