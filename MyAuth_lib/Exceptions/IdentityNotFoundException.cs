namespace MyAuth_lib.Exceptions
{
    public class IdentityNotFoundException : Exception
    {
        public IdentityNotFoundException(string? message) : base(message)
        {
        }
    }
}
