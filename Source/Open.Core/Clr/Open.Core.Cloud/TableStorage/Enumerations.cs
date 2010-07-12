namespace Open.Core.Cloud.TableStorage
{
    /// <summary>Flags indicating how to treat a key query.</summary>
    public enum KeyQueryType
    {
        Literal, // The given keys are the exact keys to look for.
        StartsWith // The given key values are the starting values of the keys to look for.
    }
}
