using Microsoft.AspNetCore.Mvc;
using Zadanie5.Application.DTOs;
using Zadanie5.Application.Interfaces;
using Zadanie5.Application.Mappers;
using Zadanie5.Domain.Entities;

namespace Zadanie5.Web.Controllers;

public class KlienciController : Controller
{
    private readonly IFileCreatingService _fileCreatingService;
    private readonly IFileProcessService _fileProcessService;
    private readonly IKlientService _klientService;

    public KlienciController(IFileProcessService fileProcessService, IFileCreatingService fileCreatingService,
        IKlientService klientService)
    {
        _fileProcessService = fileProcessService;
        _fileCreatingService = fileCreatingService;
        _klientService = klientService;
    }

    // GET
    public async Task<IActionResult> Index()
    {
        var klienci = await _klientService.GetAllKlientsAsync();
        return View(klienci.Select(x => CompleteKlientMapper.ToDto(x)).ToList());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(KlientDto klient)
    {
        if (ModelState.IsValid)
            try
            {
                await _klientService.CreateKlientAsync(klient);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("Pesel", ex.Message);
            }

        return View(klient);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return BadRequest();
        var klient = await _klientService.GetKlientByIdAsync(id);
        if (klient == null) return NotFound();
        return View(KlientMapper.ToDto(klient));
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int? id, KlientDto klient)
    {
        if (id == null) return BadRequest();
        
        if (ModelState.IsValid)
            try
            {
                await _klientService.UpdateKlientAsync(id.Value, klient);
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Pesel", e.Message);
            }

        return View(klient);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return BadRequest();
        var klient = await _klientService.GetKlientByIdAsync(id);
        if (klient == null) return NotFound();
        return View(CompleteKlientMapper.ToDto(klient));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var klient = await _klientService.GetKlientByIdAsync(id);
        if (klient == null) return NotFound();

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

            (klienci, errors) = await _fileProcessService.ProcessCsvFile(import.File);
        }
        else if (import.Type == "XLSX")
        {
            if (!import.File.FileName.EndsWith(".xlsx"))
            {
                ModelState.AddModelError("File", "Prosze wybrać plik XLSX.");
                return View("Import");
            }

            (klienci, errors) = await _fileProcessService.ProcessXlsxFile(import.File);
        }
        else
        {
            ModelState.AddModelError("Type", "Nieznany typ importu.");
            return View("Import");
        }

        if (errors.Any())
        {
            foreach (var error in errors) ModelState.AddModelError("File", error);
            return View("Import");
        }

        var klienciDto = klienci.Select(KlientMapper.ToDto).ToList();
        await _klientService.ImportKlientsAsync(klienciDto);
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
                var (content, contentType, fileName) = await _fileCreatingService.ExportKlientsToCsv();
                return File(content, contentType, fileName);
            }

            if (export.Type == "XLSX")
            {
                var (content, contentType, fileName) = await _fileCreatingService.ExportKlientsToXlsx();
                return File(content, contentType, fileName);
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