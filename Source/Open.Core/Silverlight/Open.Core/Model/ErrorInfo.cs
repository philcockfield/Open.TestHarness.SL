namespace Open.Core.Common
{
    /// <summary>Information describing an error.</summary>
    public class ErrorInfo : IErrorInfo
    {
        /// <summary>Gets or sets the unique identifier of the error.</summary>
        public int ErrorCode { get; set; }

        /// <summary>Gets or sets whether the error is a warning.</summary>
        public bool IsWarning { get; set; }

        /// <summary>Gets or sets the descriptive error message.</summary>
        public string ErrorMessage { get; set; }

        /// <summary>Converts the error to a string.</summary>
        public override string ToString()
        {
            return (string.Format("{0}: {1}, {2}",
                                  IsWarning ? "Warning" : "Error",
                                  ErrorCode,
                                  ErrorMessage));
        }
    }
}
