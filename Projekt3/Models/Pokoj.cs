using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Projekt3.Models
{
    public class Pokoj
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public int SzacowanaWielkosc { get; set; }
        public string Opis { get; set; } = string.Empty;
        
        public string? ThumbnailUrl { get; set; }
        
        
        public string? UserId { get; set; }
        
        
        [ForeignKey("UserId")]
        public IdentityUser? User { get; set; }
        
        public List<PunktLokacji> PunktyLokacji { get; set; } = new();
    }
}
