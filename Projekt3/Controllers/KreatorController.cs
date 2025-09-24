using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; 
using Projekt3.Data;
using Projekt3.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Projekt3.Controllers
{
    [Authorize] 
    public class KreatorController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public KreatorController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);
            var pokoje = _context.Pokoje
                .Include(p => p.PunktyLokacji) 
                .Where(p => p.UserId == userId) 
                .ToList();
            return View(pokoje);
        }

        [HttpPost]
        public IActionResult Index(string nazwa, int szacowanaWielkosc, string opis, string thumbnailUrl,
                                   double? latitude, double? longitude, DateTime? dataPomiaru)
        {
            var userId = _userManager.GetUserId(User);

            var pokoj = new Pokoj
            {
                Nazwa = nazwa,
                SzacowanaWielkosc = szacowanaWielkosc,
                Opis = opis,
                ThumbnailUrl = thumbnailUrl,
                UserId = userId
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
            var userId = _userManager.GetUserId(User);
            var pokoj = _context.Pokoje
                .Include(p => p.PunktyLokacji) 
                .FirstOrDefault(p => p.Id == id && p.UserId == userId); 

            if (pokoj == null) return NotFound();
            return View(pokoj);
        }

        [HttpPost]
        public IActionResult Edit(Pokoj pokoj)
        {
            if (!ModelState.IsValid) return View(pokoj);

            var userId = _userManager.GetUserId(User);
            var istniejacyPokoj = _context.Pokoje
                .Include(p => p.PunktyLokacji)
                .FirstOrDefault(p => p.Id == pokoj.Id);
            
            if (istniejacyPokoj == null || istniejacyPokoj.UserId != userId)
            {
                return NotFound();
            }
            
            
            pokoj.UserId = userId;
            
            
            istniejacyPokoj.Nazwa = pokoj.Nazwa;
            istniejacyPokoj.SzacowanaWielkosc = pokoj.SzacowanaWielkosc;
            istniejacyPokoj.Opis = pokoj.Opis;
            istniejacyPokoj.ThumbnailUrl = pokoj.ThumbnailUrl;
            
            
            if (pokoj.PunktyLokacji != null)
            {
                foreach (var punkt in pokoj.PunktyLokacji)
                {
                    var istniejacyPunkt = istniejacyPokoj.PunktyLokacji
                        .FirstOrDefault(p => p.Id == punkt.Id);
                    
                    if (istniejacyPunkt != null)
                    {
                        
                        istniejacyPunkt.Latitude = punkt.Latitude;
                        istniejacyPunkt.Longitude = punkt.Longitude;
                        istniejacyPunkt.DataPomiaru = punkt.DataPomiaru;
                    }
                }
            }
            
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var userId = _userManager.GetUserId(User);
            var pokoj = _context.Pokoje
                .Include(p => p.PunktyLokacji)
                .FirstOrDefault(p => p.Id == id && p.UserId == userId);
            
            if (pokoj == null) return NotFound();

            if (pokoj.PunktyLokacji.Any())
            {
                TempData["ErrorMessage"] = "Nie można usunąć pokoju, ponieważ ma przypisane punkty lokacji. Usuń najpierw wszystkie punkty lokacji.";
                return RedirectToAction("Index");
            }

            _context.Pokoje.Remove(pokoj);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeletePunktLokacji(int punktId, int pokojId)
        {
            var userId = _userManager.GetUserId(User);
            var pokoj = _context.Pokoje.FirstOrDefault(p => p.Id == pokojId && p.UserId == userId);
            
            if (pokoj == null) return NotFound();

            var punkt = _context.PunktyLokacji.FirstOrDefault(p => p.Id == punktId && p.PokojId == pokojId);
            if (punkt != null)
            {
                _context.PunktyLokacji.Remove(punkt);
                _context.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = pokojId });
        }

        [HttpPost]
        public IActionResult AddPunktLokacji(int pokojId, double latitude, double longitude, DateTime dataPomiaru)
        {
            var userId = _userManager.GetUserId(User);
            var pokoj = _context.Pokoje.FirstOrDefault(p => p.Id == pokojId && p.UserId == userId);
            
            if (pokoj == null) return NotFound();

            var nowyPunkt = new PunktLokacji
            {
                PokojId = pokojId,
                Latitude = latitude,
                Longitude = longitude,
                DataPomiaru = dataPomiaru
            };

            _context.PunktyLokacji.Add(nowyPunkt);
            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = pokojId });
        }
    }
}
