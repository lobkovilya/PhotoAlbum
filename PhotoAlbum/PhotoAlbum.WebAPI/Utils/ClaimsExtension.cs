using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace PhotoAlbum.WebAPI.Utils
{
    public static class ClaimsExtension
    {
        public static string GetClaim(this ClaimsIdentity identity, string type)
        {
            return identity.Claims.FirstOrDefault(x => x.Type == type)?.Value;
        }
    }
}