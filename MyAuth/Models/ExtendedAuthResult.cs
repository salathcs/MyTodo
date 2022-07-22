using MyAuth_lib.Auth_Server.Models;

namespace MyAuth.Models
{
    public class ExtendedAuthResult : AuthResult
    {
        public ExtendedAuthResult(AuthResult authResult, string redirectUrl)
        {
            Token = authResult.Token;
            Expiration = authResult.Expiration;
            Name = authResult.Name;
            UserId = authResult.UserId;

            RedirectUrl = redirectUrl;
        }

        public string RedirectUrl { get; set; }
    }
}
