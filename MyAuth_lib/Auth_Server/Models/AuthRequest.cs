namespace MyAuth_lib.Auth_Server.Models
{
    public class AuthRequest
    {
        public string? UserName { get; set; }

        public string? Password { get; set; }

        public override string? ToString()
        {
            return $"UserName: {UserName}";
        }
    }
}
