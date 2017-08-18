using System;
using System.Net;

namespace PhotoAlbum.Client.Infrastructure
{
    public class ServerResponseException : Exception
    {
        public string ReasonPhrase { get; set; }

        public HttpStatusCode StatusCode { get; set;} 

        public ServerResponseException(string reasonPhrase, string message, HttpStatusCode statusCode) : base(message)
        {
            ReasonPhrase = reasonPhrase;
            StatusCode = statusCode;
        }
    }
}