using System.ComponentModel.DataAnnotations;

namespace SpecificationPatternInEfCore.Entity;

public record Game : BaseEntity
{
    public string Name { get; set; } = null!;
    public Genre? Genre { get; set; }
}

public record Genre : BaseEntity
{
    public string Name { get; set; } = null!;
}
public record BaseEntity
{
    [Key]
    public int id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}
