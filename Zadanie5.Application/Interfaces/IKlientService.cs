

using Zadanie5.Domain.Entities;

namespace Zadanie5.Application.Interfaces;

public interface IKlientService
{
    Task<List<Klient>> GetAllKlientsAsync();
    Task<Klient?> GetKlientByIdAsync(int? id);
    Task CreateKlientAsync(Klient klient);
    Task UpdateKlientAsync(Klient klient);
    Task DeleteKlientAsync(Klient klient);
    Task ImportKlientsAsync(List<Klient> klients);
}