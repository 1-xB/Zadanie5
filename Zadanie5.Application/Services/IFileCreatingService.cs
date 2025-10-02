namespace Zadanie5.Services.Services;

public interface IFileCreatingService
{
    Task<(byte[] fileContent, string contentType, string fileName)> ExportKlientsToCsv();
    Task<(byte[] content, string, string)> ExportKlientsToXlsx();
}