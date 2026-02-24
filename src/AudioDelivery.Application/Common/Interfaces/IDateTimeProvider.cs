namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Abstraction over DateTime.UtcNow for testability.
///
/// By injecting IDateTimeProvider instead of calling DateTime.UtcNow directly,
/// you can freeze time in unit tests for deterministic assertions.
///
/// TODO: Register a concrete implementation in DI.
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
