namespace Open.Core.Test
{
    public class SampleView : ViewBase
    {
        #region Head
        private string text;

        public SampleView() : base(Html.CreateDiv())
        {
            PropertyChanged += delegate(object sender, PropertyChangedEventArgs args) { LastPropertyChanged = args.Property; };
        }
        #endregion

        #region Properties
        public PropertyRef LastPropertyChanged;

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                Container.Empty();
                Container.Append(value);
            }
        }
        #endregion
    }
}
