namespace Open.Core.UI.Controls
{
    /// <summary>Event argument information (fired from the EventBus) when a tool is executed.</summary>
    public interface IToolEvent 
    {
        /// <summary>Gets the tool that caused the event to fire.</summary>
        ITool Tool { get; }
    }
}
