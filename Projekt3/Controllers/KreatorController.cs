using Microsoft.AspNetCore.Mvc;
using Projekt3.Data;
using Projekt3.Models;

namespace Projekt3.Controllers
{
    public class KreatorController : Controller
    {
        private readonly AppDbContext _context;

        public KreatorController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var pokoje = _context.Pokoje.ToList();
            return View(pokoje);
        }

        [HttpPost]
        public IActionResult Index(string nazwa, int szacowanaWielkosc, string opis, string thumbnailUrl,
                                   double? latitude, double? longitude, DateTime? dataPomiaru)
        {
            var pokoj = new Pokoj
            {
                Nazwa = nazwa,
                SzacowanaWielkosc = szacowanaWielkosc,
                Opis = opis,
                ThumbnailUrl = thumbnailUrl
            };

            if (latitude.HasValue && longitude.HasValue && dataPomiaru.HasValue)
            {
                pokoj.PunktyLokacji.Add(new PunktLokacji
                {
                    Latitude = latitude.Value,
                    Longitude = longitude.Value,
                    DataPomiaru = dataPomiaru.Value
                });
            }

            _context.Pokoje.Add(pokoj);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var pokoj = _context.Pokoje
                .Where(p => p.Id == id)
                .FirstOrDefault();

            if (pokoj == null) return NotFound();
            return View(pokoj);
        }

        [HttpPost]
        public IActionResult Edit(Pokoj pokoj)
        {
            if (!ModelState.IsValid) return View(pokoj);

            _context.Pokoje.Update(pokoj);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var pokoj = _context.Pokoje.Find(id);
            if (pokoj == null) return NotFound();

            _context.Pokoje.Remove(pokoj);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
