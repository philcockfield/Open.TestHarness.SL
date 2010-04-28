using Open.Core.UI.Controls;

namespace Open.Core.Test.UnitTests.Core.UI.Controls.ToolBar
{
    public class MockTool : ToolBase
    {
        public void FireExecutedEventPublic()
        {
            base.PublishToolEvent();
        }
    }
}
