using System.Text;
using ClosedXML.Excel;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zadanie5.Data;
using Zadanie5.Helpers;
using Zadanie5.Models;
using Zadanie5.Services.Services;

namespace Zadanie5.Controllers;

public class KlienciController : Controller
{
    private readonly PeselValidator _peselValidator;
    private readonly FileProcessingService _fileProcessingService;
    private readonly FileCreatingService _fileCreatingService;
    private readonly IKlientService  _klientService;
    public KlienciController(PeselValidator peselValidator, FileProcessingService fileProcessingService, FileCreatingService fileCreatingService, IKlientService klientService)
    {
        _peselValidator = peselValidator;
        _fileProcessingService = fileProcessingService;
        _fileCreatingService = fileCreatingService;
        _klientService = klientService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        var klienci = await _klientService.GetAllKlientsAsync();
        return View(klienci);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Create(Klient klient)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var (birthYear, gender) = _peselValidator.ParsePesel(klient.Pesel);
                klient.BirthYear = birthYear;
                klient.Gender = gender;
            
                await _klientService.CreateKlientAsync(klient);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Pesel", ex.Message);
            }
        }
        return View(klient);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        var klient = await _klientService.GetKlientByIdAsync(id);
        if (klient == null)
        {
            return NotFound();
        }
        return View(klient);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Klient klient)
    {
        if (ModelState.IsValid)
        {
            try
            {
                await _klientService.UpdateKlientAsync(klient);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Pesel", e.Message);
            }
        }
        return View(klient);
    }
    
    public async Task<IActionResult> Delete(int? id)
    {
        var klient = await _klientService.GetKlientByIdAsync(id);
        if (klient == null)
        {
            return NotFound();
        }
        return View(klient);
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var klient = await _klientService.GetKlientByIdAsync(id);
        if (klient == null)
        {
            return NotFound();
        }
        
        await _klientService.DeleteKlientAsync(klient);
        return RedirectToAction("Index");
    }
    
    public IActionResult Import()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Upload(Import import)
    {
        if (import.File == null || import.File.Length == 0)
        {
            ModelState.AddModelError("File", "Prosze wybrać plik.");
            return View("Import");
        }

        List<Klient> klienci;
        List<string> errors;

        if (import.Type == "CSV")
        {
            if (!import.File.FileName.EndsWith(".csv"))
            {
                ModelState.AddModelError("File", "Prosze wybrać plik CSV.");
                return View("Import");
            }

            (klienci, errors) = await _fileProcessingService.ProcessCsvFile(import.File);
        }
        else if (import.Type == "XLSX")
        {
            if (!import.File.FileName.EndsWith(".xlsx"))
            {
                ModelState.AddModelError("File", "Prosze wybrać plik XLSX.");
                return View("Import");
            }

            (klienci, errors) = await _fileProcessingService.ProcessXlsxFile(import.File);
        }
        else
        {
            ModelState.AddModelError("Type", "Nieznany typ importu.");
            return View("Import");
        }

        if (errors.Any())
        {
            foreach (var error in errors)
            {
                ModelState.AddModelError("File", error);
            }
            return View("Import");
        }

        await _klientService.ImportKlientsAsync(klienci);
        return RedirectToAction("Index");
    }
    

    public IActionResult Export()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Export(Export export)
    {
        try
        {
            if (export.Type == "CSV")
            {
                return await _fileCreatingService.ExportKlientsToCsv();
            }

            if (export.Type == "XLSX")
            {
                return await _fileCreatingService.ExportKlientsToXlsx();
            }

            ModelState.AddModelError("Type", "Nieznany typ eksportu");
            return View("Export");
        }
        catch (Exception e)
        {
            ModelState.AddModelError("Type", "Wystąpił błąd poczas tworzenia pliku do eksportu");
            return View("Export");
        }

    }
    
}