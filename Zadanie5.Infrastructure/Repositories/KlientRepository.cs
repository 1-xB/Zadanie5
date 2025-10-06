using Microsoft.EntityFrameworkCore;
using Zadanie5.Data;
using Zadanie5.Core.Interfaces;
using Zadanie5.Core.Models;

namespace Zadanie5.Infrastructure.Repositories;

public class KlientRepository : IKlientRepository
{
    private readonly DatabaseContext _context;

    public KlientRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Klient>> GetAllAsync()
    {
        return await _context.Klienci.ToListAsync();
    }

    public async Task<Klient?> GetByIdAsync(int id)
    {
        return await _context.Klienci.FirstOrDefaultAsync(k => k.Id == id);
    }

    public async Task AddAsync(Klient klient)
    {
        await _context.Klienci.AddAsync(klient);
    }

    public async Task AddRangeAsync(List<Klient> klients)
    {
        await _context.Klienci.AddRangeAsync(klients);
    }

    public async Task UpdateAsync(Klient klient)
    {
        _context.Klienci.Update(klient);
    }

    public async Task DeleteAsync(int id)
    {
        var klient = await GetByIdAsync(id);
        if (klient != null) _context.Klienci.Remove(klient);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}