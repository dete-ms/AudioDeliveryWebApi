using AudioDelivery.Domain.Entities;

namespace AudioDelivery.Domain.JoinTables;

/// <summary>
/// Join entity for the many-to-many relationship between Category and Image.
/// </summary>
public class CategoryImage
{
    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;

    public Guid ImageId { get; set; }
    public Image Image { get; set; } = null!;
}
