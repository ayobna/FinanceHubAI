namespace FinanceHubAI.Application.Common.Exceptions;

public class ApplicationValidationException : Exception
{
    public ApplicationValidationException()
        : base("One or more validation failures have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ApplicationValidationException(IDictionary<string, string[]> errors)
        : this()
    {
        Errors = errors;
    }

    public IDictionary<string, string[]> Errors { get; }
}