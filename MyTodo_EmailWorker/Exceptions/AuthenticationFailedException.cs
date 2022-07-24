namespace MyTodo_EmailWorker.Exceptions
{
    internal class AuthenticationFailedException : EmailWorkerBaseException
    {
        public AuthenticationFailedException(string? message) : base(message)
        {
        }
    }
}
