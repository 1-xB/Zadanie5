using Microsoft.AspNetCore.Http;
using Zadanie5.Core.Models;

namespace Zadanie5.Services.Services;

public interface IFileProcessService
{
    Task<(List<Klient> klienci, List<string> errors)> ProcessCsvFile(IFormFile file);
    Task<(List<Klient> klienci, List<string> errors)> ProcessXlsxFile(IFormFile file);
}