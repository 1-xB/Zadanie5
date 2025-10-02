using Zadanie5.Core;
using Zadanie5.Core.Models;

namespace Zadanie5.Services.Services;

public interface IKlientService
{
    Task<List<Klient>> GetAllKlientsAsync();
    Task<Klient?> GetKlientByIdAsync(int? id);
    Task CreateKlientAsync(Klient klient);
    Task UpdateKlientAsync(Klient klient);
    Task DeleteKlientAsync(Klient klient);
    Task ImportKlientsAsync(List<Klient> klients);
}