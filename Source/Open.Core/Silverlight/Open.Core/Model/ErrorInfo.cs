namespace Open.Core.Common
{
    /// <summary>Information describing an error.</summary>
    public class ErrorInfo : IErrorInfo
    {
        /// <summary>Gets or sets the unique identifier of the error.</summary>
        public int ErrorCode { get; set; }

        /// <summary>Gets or sets the descriptive error message.</summary>
        public string ErrorMessage { get; set; }

        /// <summary>Converts the error to a string.</summary>
        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}
