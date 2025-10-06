using Microsoft.AspNetCore.Http;
using Zadanie5.Domain.Entities;

namespace Zadanie5.Application.Interfaces;

public interface IFileProcessService
{
    Task<(List<Klient> klienci, List<string> errors)> ProcessCsvFile(IFormFile file);
    Task<(List<Klient> klienci, List<string> errors)> ProcessXlsxFile(IFormFile file);
}