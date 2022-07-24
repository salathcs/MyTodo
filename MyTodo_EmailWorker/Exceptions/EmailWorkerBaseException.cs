using System.Runtime.Serialization;

namespace MyTodo_EmailWorker.Exceptions
{
    internal class EmailWorkerBaseException : Exception
    {
        public EmailWorkerBaseException()
        {
        }

        public EmailWorkerBaseException(string? message) : base(message)
        {
        }

        public EmailWorkerBaseException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected EmailWorkerBaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
