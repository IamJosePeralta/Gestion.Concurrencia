namespace Domain.Exceptions
{
    public class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message, Exception? innerException = null)
            : base(message, innerException) { }
    }

    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
            : base(message) { }
    }
}
