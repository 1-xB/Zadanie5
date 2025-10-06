using System.Globalization;
using ClosedXML.Excel;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Zadanie5.Application.Interfaces;
using Zadanie5.Domain.Entities;

namespace Zadanie5.Application.Services;

public class FileProcessService : IFileProcessService
{
    private readonly IPeselValidator _peselValidator;

    public FileProcessService(IPeselValidator peselValidator)
    {
        _peselValidator = peselValidator;
    }

    public async Task<(List<Klient> klienci, List<string> errors)> ProcessCsvFile(IFormFile file)
    {
        var klienci = new List<Klient>();
        var errors = new List<string>();

        try
        {
            using var stream = file.OpenReadStream();
            using var reader = new StreamReader(stream);
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";"
            };
            using var csv = new CsvReader(reader, config);

            await csv.ReadAsync();
            csv.ReadHeader();

            var validationResult = ValidateHeaders(csv.HeaderRecord, new[] { "Name", "Surname", "Pesel" });
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors);
                return (klienci, errors);
            }

            var records = csv.GetRecords<KlientImport>();
            var rowNumber = 2;

            foreach (var record in records)
            {
                try
                {
                    var (birthYear, gender) = _peselValidator.ParsePesel(record.Pesel);
                    klienci.Add(new Klient
                    {
                        Name = record.Name,
                        Surname = record.Surname,
                        Pesel = record.Pesel,
                        BirthYear = birthYear,
                        Gender = gender
                    });
                }
                catch (Exception e)
                {
                    errors.Add($"Błąd w linii {rowNumber}: {e.Message}");
                }

                rowNumber++;
            }
        }
        catch (Exception ex)
        {
            errors.Add($"Błąd podczas przetwarzania pliku: {ex.Message}");
        }

        return (klienci, errors);
    }

    public async Task<(List<Klient> klienci, List<string> errors)> ProcessXlsxFile(IFormFile file)
    {
        var klienci = new List<Klient>();
        var errors = new List<string>();

        try
        {
            using var stream = file.OpenReadStream();
            using var workbook = new XLWorkbook(stream);

            var ws = workbook.Worksheet(1);
            var headerRow = ws.Row(1);

            var validationResult = ValidateXlsxHeaders(headerRow, new[] { "Name", "Surname", "Pesel" });
            if (!validationResult.IsValid)
            {
                errors.AddRange(validationResult.Errors);
                return (klienci, errors);
            }

            var rows = ws.RowsUsed().Skip(1);
            var rowNumber = 2;

            foreach (var row in rows)
            {
                try
                {
                    var name = row.Cell(1).GetString();
                    var surname = row.Cell(2).GetString();
                    var pesel = row.Cell(3).GetString();

                    var (birthYear, gender) = _peselValidator.ParsePesel(pesel);
                    klienci.Add(new Klient
                    {
                        Name = name,
                        Surname = surname,
                        Pesel = pesel,
                        BirthYear = birthYear,
                        Gender = gender
                    });
                }
                catch (Exception e)
                {
                    errors.Add($"Błąd w linii {rowNumber}: {e.Message}");
                }

                rowNumber++;
            }
        }
        catch (Exception ex)
        {
            errors.Add($"Błąd podczas przetwarzania pliku: {ex.Message}");
        }

        return (klienci, errors);
    }

    private (bool IsValid, List<string> Errors) ValidateHeaders(string[] headers, string[] required)
    {
        var errors = new List<string>();
        foreach (var header in required)
            if (headers == null || !headers.Contains(header))
                errors.Add($"Brakuje header: {header}");
        return (errors.Count == 0, errors);
    }

    private (bool IsValid, List<string> Errors) ValidateXlsxHeaders(IXLRow headerRow, string[] required)
    {
        var errors = new List<string>();
        for (var i = 0; i < required.Length; i++)
        {
            var headerValue = headerRow.Cell(i + 1).GetString();
            if (!required.Contains(headerValue))
                errors.Add($"Brakuje header: {required[i]}");
        }

        return (errors.Count == 0, errors);
    }
}