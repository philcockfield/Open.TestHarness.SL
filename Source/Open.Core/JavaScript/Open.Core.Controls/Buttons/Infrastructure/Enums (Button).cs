namespace Open.Core.Controls.Buttons
{
    /// <summary>The enabled-related conditions for which button content applies.</summary>
    public enum EnabledCondition
    {
        DisabledOnly = 0,
        EnabledOnly = 1,
        Either = 2
    }

    /// <summary>The focus-related conditions for which button content applies.</summary>
    public enum FocusCondition
    {
        UnfocusedOnly = 0,
        FocusedOnly = 1,
        Either = 2
    }
}
