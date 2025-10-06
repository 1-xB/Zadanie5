using System.ComponentModel.DataAnnotations;

namespace Zadanie5.Domain.Entities;
public class Klient
{
    [Key] public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public string Surname { get; set; } = null!;
    
    public string Pesel { get; set; } = null!;

    [Required] public short BirthYear { get; set; }

    [Required] [Range(0, 1)] public short Gender { get; set; }
}