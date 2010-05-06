namespace Open.Core.Common
{
    /// <summary>Information describing an error.</summary>
    public interface IErrorInfo
    {
        /// <summary>Gets or sets the unique identifier of the error.</summary>
        int ErrorCode { get; set; }

        /// <summary>Gets or sets whether the error is a warning.</summary>
        bool IsWarning { get; set; }

        /// <summary>Gets or sets the descriptive error message.</summary>
        string ErrorMessage { get; set; }

        /// <summary>Converts the error to a string.</summary>
        string ToString();
    }
}