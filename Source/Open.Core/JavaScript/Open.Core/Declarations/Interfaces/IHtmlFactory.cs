namespace Open.Core
{
    /// <summary>An object that can create HTML.</summary>
    public interface IHtmlFactory
    {
        /// <summary>Retrieves HTML.</summary>
        string CreateHtml();
    }
}
