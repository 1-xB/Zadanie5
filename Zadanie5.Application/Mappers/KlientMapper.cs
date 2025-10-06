using Zadanie5.Application.DTOs;
using Zadanie5.Application.Interfaces;
using Zadanie5.Domain.Entities;

namespace Zadanie5.Application.Mappers;

public static class KlientMapper
{
    public static Klient ToEntity(KlientDto dto, IPeselValidator peselValidator)
    {
        var (birthYear, gender) = peselValidator.ParsePesel(dto.Pesel);
        return new Klient
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Pesel = dto.Pesel,
            BirthYear = birthYear,
            Gender = gender
        };
    }

    public static KlientDto ToDto(Klient klient)
    {
        return new KlientDto
        {
            Name = klient.Name,
            Surname = klient.Surname,
            Pesel = klient.Pesel,
        };
    }
}