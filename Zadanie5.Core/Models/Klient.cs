using System.ComponentModel.DataAnnotations;

namespace Zadanie5.Core.Models;
public class Klient
{
    [Key] public int Id { get; set; }

    [Required(ErrorMessage = "Imię jest wymagane")]
    [MaxLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków")]
    [MinLength(2, ErrorMessage = "Imię musi mieć conajmniej 2 znaki")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [MaxLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków")]
    [MinLength(2, ErrorMessage = "Nazwisko musi mieć conajmniej 2 znaki")]
    public string Surname { get; set; } = null!;

    [Required]
    [MaxLength(11, ErrorMessage = "Pesel może mieć maksymalnie 11 znaków")]
    [MinLength(11, ErrorMessage = "Pesel musi mieć conajmniej 11 znaków")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Pesel musi składać się z 11 cyfr")]
    public string Pesel { get; set; } = null!;

    [Required] public short BirthYear { get; set; }

    [Required] [Range(0, 1)] public short Gender { get; set; }
}