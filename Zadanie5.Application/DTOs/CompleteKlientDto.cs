using System.ComponentModel.DataAnnotations;

namespace Zadanie5.Application.DTOs;

public class CompleteKlientDto
{
    [Required]
    public int Id { get; set; }
    [Required(ErrorMessage = "Imię jest wymagane")]
    [MaxLength(50, ErrorMessage = "Imię może mieć maksymalnie 50 znaków")]
    [MinLength(2, ErrorMessage = "Imię musi mieć conajmniej 2 znaki")]
    public string Name { get; set; }
    
    [Required(ErrorMessage = "Nazwisko jest wymagane")]
    [MaxLength(50, ErrorMessage = "Nazwisko może mieć maksymalnie 50 znaków")]
    [MinLength(2, ErrorMessage = "Nazwisko musi mieć conajmniej 2 znaki")]
    public string Surname { get; set; }
    
    [Required(ErrorMessage = "Pesel jest wymagany")]
    [MaxLength(11, ErrorMessage = "Pesel może mieć maksymalnie 11 znaków")]
    [MinLength(11, ErrorMessage = "Pesel musi mieć conajmniej 11 znaków")]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Pesel musi składać się z 11 cyfr")]
    public string Pesel { get; set; }
    
    [Required(ErrorMessage = "Rok urodzenia jest wymagany")] 
    public short BirthYear { get; set; }

    [Required(ErrorMessage = "Płeć jest wymagana")] 
    [Range(0, 1, ErrorMessage = "Podanie płci jest wymagane. 0 - Dla kobiet, 1 - Dla mężczyzn")] 
    public short Gender { get; set; }
}