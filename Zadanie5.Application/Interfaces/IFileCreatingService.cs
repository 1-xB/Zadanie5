namespace Zadanie5.Application.Interfaces;

public interface IFileCreatingService
{
    Task<(byte[] fileContent, string contentType, string fileName)> ExportKlientsToCsv();
    Task<(byte[] content, string, string)> ExportKlientsToXlsx();
}