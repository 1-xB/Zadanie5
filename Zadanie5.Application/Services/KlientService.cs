using Zadanie5.Helpers;
using Zadanie5.Models;
using Zadanie5.Models.Interfaces;


namespace Zadanie5.Services.Services;

public class KlientService : IKlientService
{
    private readonly IKlientRepository _repository;
    private readonly PeselValidator _peselValidation;

    public KlientService(IKlientRepository repository, PeselValidator peselValidation)
    {
        _repository = repository;
        _peselValidation = peselValidation;
    }
    
    public async Task<List<Klient>> GetAllKlientsAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Klient?> GetKlientByIdAsync(int? id)
    {
        if (id == null)
        {
            return null;
        }
        return await _repository.GetByIdAsync(id.Value);
    }

    public async Task CreateKlientAsync(Klient klient)
    {
        var (birthYear, gender) = _peselValidation.ParsePesel(klient.Pesel);
        klient.BirthYear = birthYear;
        klient.Gender = gender;
        
        await _repository.AddAsync(klient);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateKlientAsync(Klient klient)
    {
        var (birthYear, gender) = _peselValidation.ParsePesel(klient.Pesel);
        klient.BirthYear = birthYear;
        klient.Gender = gender;
        
        await _repository.UpdateAsync(klient);
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteKlientAsync(Klient klient)
    {
        await _repository.DeleteAsync(klient.Id);
        await _repository.SaveChangesAsync();
    }

    public async Task ImportKlientsAsync(List<Klient> klients)
    {
        await _repository.AddRangeAsync(klients);
        await _repository.SaveChangesAsync();
    }
}