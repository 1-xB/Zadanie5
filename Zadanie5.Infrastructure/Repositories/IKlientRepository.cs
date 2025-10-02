namespace Zadanie5.Models.Interfaces;

public interface IKlientRepository
{
    Task<List<Klient>> GetAllAsync();
    Task<Klient?> GetByIdAsync(int id);
    Task AddAsync(Klient klient);
    Task AddRangeAsync(List<Klient> klients);
    Task UpdateAsync(Klient klient);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}