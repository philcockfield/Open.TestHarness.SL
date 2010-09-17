using Open.Core.Lists;

namespace Open.Core.Test
{
    public class SampleListItem : ListItem
    {
        public SampleListItem(string text) { Text = text; }
        public override string ToString() { return base.ToString(); }

        public static SampleListItem Create(string text, string childPrefix, int totalChildren)
        {
            SampleListItem root = new SampleListItem(text);
            for (int i = 1; i <= totalChildren; i++)
            {
                root.AddChild(new SampleListItem(string.Format("{0} {1}", childPrefix, i)));
            }
            return root;
        }
    }
}
