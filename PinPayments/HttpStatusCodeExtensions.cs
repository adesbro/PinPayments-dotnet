using System.Net;

namespace PinPayments
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsNon20XCode(this HttpStatusCode statusCode)
        {
            return ((int)statusCode < 200 || (int)statusCode >= 300);
        }
    }
}