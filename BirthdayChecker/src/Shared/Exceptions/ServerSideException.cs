using System.Net;

namespace Shared.Exceptions;

public class ServerSideException : Exception
{
    public int StatusCode { get; } = 0;

    public ServerSideException(string message) : base(message)
    {

    }

    public ServerSideException(HttpStatusCode statusCode, string message) : base(message)
    {
        StatusCode = Convert.ToInt32(statusCode);
    }

    public ServerSideException(int statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
