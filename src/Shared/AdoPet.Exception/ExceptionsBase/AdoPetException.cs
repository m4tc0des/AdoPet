using System.Net;

namespace AdoPet.Exception.ExceptionsBase;

public abstract class AdoPetException: System.Exception
{
    public abstract List<string> GetErrorsMessages();
    public abstract HttpStatusCode GetStatusCode();
}
