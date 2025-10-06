using Microsoft.AspNetCore.Http;

namespace Zadanie5.Application.DTOs;

public class ImportDto
{
    public string Type { get; set; }
    public IFormFile? File { get; set; }
}