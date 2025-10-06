using Zadanie5.Application.DTOs;
using Zadanie5.Application.Interfaces;
using Zadanie5.Application.Mappers;
using Zadanie5.Domain.Entities;
using Zadanie5.Domain.Interfaces;


namespace Zadanie5.Application.Services;

public class KlientService : IKlientService
{
    private readonly IPeselValidator _peselValidation;
    private readonly IKlientRepository _repository;

    public KlientService(IKlientRepository repository, IPeselValidator peselValidation)
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
        if (id == null) return null;
        return await _repository.GetByIdAsync(id.Value);
    }

    public async Task CreateKlientAsync(KlientDto klient)
    {
        await _repository.AddAsync(KlientMapper.ToEntity(klient, _peselValidation));
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateKlientAsync(int id, KlientDto klientDto)
    {
        var klient = KlientMapper.ToEntity(klientDto, _peselValidation);
        klient.Id = id; 
    
        await _repository.UpdateAsync(klient);
        await _repository.SaveChangesAsync();
    }


    public async Task DeleteKlientAsync(Klient klient)
    {
        await _repository.DeleteAsync(klient.Id);
        await _repository.SaveChangesAsync();
    }

    public async Task ImportKlientsAsync(List<KlientDto> klientsDto)
    {
        var klients = klientsDto.Select(x => KlientMapper.ToEntity(x, _peselValidation)).ToList();
        await _repository.AddRangeAsync(klients);
        await _repository.SaveChangesAsync();
    }
}