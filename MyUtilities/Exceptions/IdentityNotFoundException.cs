namespace MyUtilities.Exceptions
{
    public class IdentityNotFoundException : Exception
    {
        public IdentityNotFoundException(string? message) : base(message)
        {
        }
    }
}
