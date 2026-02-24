namespace AudioDelivery.Domain.Enums;

/// <summary>
/// Repeat mode for a player session.
/// Included here for future Player feature expansion.
///
/// Values:
///   Off     – Repeat is disabled
///   Track   – The current track repeats indefinitely
///   Context – The entire context (album, playlist) repeats
/// </summary>
public enum RepeatMode
{
    Off,
    Track,
    Context
}
