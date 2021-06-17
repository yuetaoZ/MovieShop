using System;
using System.Net;

namespace ApplicationCore.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode Code { get; }
        public object Errors { get; set; }

        public HttpException(HttpStatusCode code, object errors = null)
        {
            Code = code;
            Errors = errors;
        }
    }
}
