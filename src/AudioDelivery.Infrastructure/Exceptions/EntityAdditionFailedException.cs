namespace AudioDelivery.Infrastructure.Exceptions;

public class EntityAdditionFailedException : Exception
{
    public EntityAdditionFailedException(string message) : base(message)
    {
    }
    public EntityAdditionFailedException(string message, Exception innerException) : base(message, innerException)
    {
    }
}
