using AudioDelivery.Domain.Enums;

namespace AudioDelivery.Application.Common.Interfaces;

/// <summary>
/// Defines a service for generating hyperlink references (hrefs) for specified entity types.
/// </summary>
public interface IHrefGenerationService
{
    /// <summary>
    /// Generates a hyperlink reference (href) for the specified entity type.
    /// </summary>
    /// <param name="entityType">The type of entity for which to generate the hyperlink reference.</param>
    /// <returns>A string containing the generated hyperlink reference for the specified entity type.</returns>
    string GenerateHref(EntityType entityType);

    /// <summary>
    /// Generates a hyperlink (HREF) for a paginated resource collection based on the specified entity type and
    /// pagination parameters.
    /// </summary>
    /// <param name="entityType">The type of entity for which to generate the paginated hyperlink.</param>
    /// <param name="limit">The maximum number of items to include in the page. Must be greater than 0.</param>
    /// <param name="offset">The zero-based index of the first item to include in the page. Must be greater than or equal to 0.</param>
    /// <returns>A string containing the generated hyperlink for the specified page of entities.</returns>
    string GeneratePaginatedHref(EntityType entityType, int limit = 50, int offset = 0);
}
