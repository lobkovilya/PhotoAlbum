using System;

namespace PhotoAlbum.BLL.Contracts.Infrastructure
{
    public class NotModifiedException : Exception
    {
        public NotModifiedException() : base("Not modified")
        {
        }
    }
}