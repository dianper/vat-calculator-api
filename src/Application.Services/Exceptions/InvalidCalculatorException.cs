namespace Application.Services.Exceptions
{
    using System.Diagnostics.CodeAnalysis;

    [ExcludeFromCodeCoverage]
    public sealed class InvalidCalculatorException : Exception
    {
        public InvalidCalculatorException(string message) : base(message)
        {
        }

        public InvalidCalculatorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
