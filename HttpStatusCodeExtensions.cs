using System.Net;

namespace EichkustMusic.S3
{
    public static class HttpStatusCodeExtensions
    {
        public static bool IsSuccess(this HttpStatusCode httpStatusCode)
            => (int)httpStatusCode >= 200 && (int)httpStatusCode <= 299;
    }
}
