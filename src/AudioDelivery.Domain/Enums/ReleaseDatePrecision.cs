namespace AudioDelivery.Domain.Enums;

/// <summary>
/// Precision with which a release date is known.
/// Spotify returns dates as "1981", "1981-12", or "1981-12-15" 
/// depending on what is available in their catalog.
///
/// Values:
///   Year  – Only the year is known (e.g. "1981")
///   Month – Year and month are known (e.g. "1981-12")
///   Day   – Full date is known (e.g. "1981-12-15")
/// </summary>
public enum ReleaseDatePrecision
{
    Year,
    Month,
    Day
}
