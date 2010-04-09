namespace Open.Core.UI.Controls
{
    /// <summary>Event argument information (fired from the EventBus) when a tool is executed.</summary>
    public class ToolEvent : IToolEvent
    {
        public ITool Tool { get; set; }
    }
}
