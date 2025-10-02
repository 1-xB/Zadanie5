using System.Text;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Zadanie5.Data;

namespace Zadanie5.Services.Services;

public class FileCreatingService
{
    private readonly DatabaseContext _context;

    public FileCreatingService(DatabaseContext databaseContext)
    {
        _context = databaseContext;
    }

    public async Task<(byte[] fileContent, string contentType, string fileName)> ExportKlientsToCsv()
    {
        var allKlients = await _context.Klienci.ToListAsync();
        var sb = new StringBuilder();
        sb.AppendLine("Name;Surname;Pesel;BithYear;Gender");
        foreach (var klient in allKlients)
            sb.AppendLine($"{klient.Name};{klient.Surname};{klient.Pesel};{klient.BirthYear};{klient.Gender}");

        var fileContent = Encoding.UTF8.GetBytes(sb.ToString());
        var contentType = "text/csv";
        var fileName = "klienci.csv";

        return (fileContent, contentType, fileName);
    }

    public async Task<(byte[] content, string, string)> ExportKlientsToXlsx()
    {
        var allKlients = await _context.Klienci.ToListAsync();

        using (var workbook = new XLWorkbook())
        {
            var worksheet = workbook.AddWorksheet("Klienci");
            worksheet.Cell(1, 1).Value = "Name";
            worksheet.Cell(1, 2).Value = "Surname";
            worksheet.Cell(1, 3).Value = "Pesel";
            worksheet.Cell(1, 4).Value = "BirthYear";
            worksheet.Cell(1, 5).Value = "Gender";

            var row = 2;
            foreach (var klient in allKlients)
            {
                worksheet.Cell(row, 1).Value = klient.Name;
                worksheet.Cell(row, 2).Value = klient.Surname;
                worksheet.Cell(row, 3).Value = klient.Pesel;
                worksheet.Cell(row, 4).Value = klient.BirthYear;
                worksheet.Cell(row, 5).Value = klient.Gender;

                row++;
            }

            using (var stream = new MemoryStream())
            {
                workbook.SaveAs(stream);
                var content = stream.ToArray();
                return (content,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    "klienci.xlsx");
            }
        }
    }
}