namespace MyTodo_EmailWorker.Exceptions
{
    internal class TodosRequestFailedException : EmailWorkerBaseException
    {
        public TodosRequestFailedException(string? message) : base(message)
        {
        }
    }
}
