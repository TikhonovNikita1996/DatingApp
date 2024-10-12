using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

[Table("Photos")]
public class Photo
{
    public int Id { get; set; }

    public required string Url { get; set; }
    
    public bool IsMain { get; set; }

    public string? PunlicId { get; set; }

    // Nav properties

    public AppUser AppUser { get; set; } = null!;
    public int AppUserId { get; set; }
}   