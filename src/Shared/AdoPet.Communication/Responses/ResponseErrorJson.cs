namespace AdoPet.Communication.Responses;

public class ResponseErrorJson
{
    public List<string> Error{ get; private set; }

    public ResponseErrorJson(List<string> errorMessages)
    {
        Error = errorMessages;
    }

    public ResponseErrorJson(string errorMessage)
    {
        Error = new List<string> { errorMessage };
    }
}

