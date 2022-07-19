namespace MyAuth_lib.Exceptions
{
    public class LoginFailedException : Exception
    {
        public LoginFailedException(string? message) : base(message)
        {
        }
    }
}
