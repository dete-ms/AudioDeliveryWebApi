namespace AudioDelivery.Infrastructure.Exceptions;

public class EntityUpdateFailedException : Exception
{
    public EntityUpdateFailedException(string message) : base(message)
    {
    }
    public EntityUpdateFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
