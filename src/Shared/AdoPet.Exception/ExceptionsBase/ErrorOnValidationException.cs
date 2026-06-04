namespace AdoPet.Exception.ExceptionsBase;

public class ErrorOnValidationException : AdoPetException
{
    private readonly List<string> _errors;
    public ErrorOnValidationException(List<string> errorMessages)
    {
        _errors = errorMessages;
    }
}
