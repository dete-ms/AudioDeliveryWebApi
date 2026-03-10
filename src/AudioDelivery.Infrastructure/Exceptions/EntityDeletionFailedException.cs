namespace AudioDelivery.Infrastructure.Exceptions;

public class EntityDeletionFailedException : Exception
{
    public EntityDeletionFailedException(string message) : base(message)
    {
    }
    public EntityDeletionFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
