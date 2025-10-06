

using Zadanie5.Application.DTOs;
using Zadanie5.Domain.Entities;

namespace Zadanie5.Application.Interfaces;

public interface IKlientService
{
    Task<List<Klient>> GetAllKlientsAsync();
    Task<Klient?> GetKlientByIdAsync(int? id);
    Task CreateKlientAsync(KlientDto klient);
    Task UpdateKlientAsync(int id, KlientDto klient);
    Task DeleteKlientAsync(Klient klient);
    Task ImportKlientsAsync(List<KlientDto> klients);
}