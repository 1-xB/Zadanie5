using Zadanie5.Application.DTOs;
using Zadanie5.Application.Interfaces;
using Zadanie5.Domain.Entities;

namespace Zadanie5.Application.Mappers;

public class CompleteKlientMapper
{
    public static Klient ToEntity(CompleteKlientDto dto)
    {
        return new Klient
        {
            Name = dto.Name,
            Surname = dto.Surname,
            Pesel = dto.Pesel,
            BirthYear = dto.BirthYear,
            Gender = dto.Gender,
        };
    }

    public static CompleteKlientDto ToDto(Klient klient)
    {
        return new CompleteKlientDto
        {
            Id = klient.Id,
            Name = klient.Name,
            Surname = klient.Surname,
            Pesel = klient.Pesel,
            BirthYear = klient.BirthYear,
            Gender = klient.Gender
        };
    }
}