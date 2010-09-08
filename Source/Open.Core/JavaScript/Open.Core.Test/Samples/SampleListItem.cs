using Open.Core.Lists;

namespace Open.Core.Test
{
    public class SampleListItem : ListItem
    {
        public SampleListItem(string text) { Text = text; }
        public override string ToString() { return base.ToString(); }
    }
}
