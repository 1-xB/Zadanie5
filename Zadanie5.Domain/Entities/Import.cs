using Microsoft.AspNetCore.Http;

namespace Zadanie5.Domain.Entities;

public class Import
{
    public string Type { get; set; }
    public IFormFile? File { get; set; }
}