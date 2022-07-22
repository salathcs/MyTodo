namespace MyAuth_lib.Auth_Server.Models
{
    public class AuthResult
    {
        public string? Token { get; set; }

        public DateTime? Expiration { get; set; }

        public string Name { get; set; }

        public long UserId { get; set; }
    }
}
