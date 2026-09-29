using System.Net;

namespace AdoPet.Exception.ExceptionsBase;

public class InvalidLoginException : AdoPetException
{
    public override List<string> GetErrorsMessages()
    {
        return new List<string> { ResourceMessagesException.VALIDATION_LOGIN_INVALID };
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }
}
