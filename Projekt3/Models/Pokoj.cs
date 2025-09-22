using System.Collections.Generic;

namespace Projekt3.Models
{
    public class Pokoj
    {
        public int Id { get; set; }
        public string Nazwa { get; set; } = string.Empty;
        public int SzacowanaWielkosc { get; set; }
        public string Opis { get; set; } = string.Empty;

        
        public string? ThumbnailUrl { get; set; }

        
        public List<PunktLokacji> PunktyLokacji { get; set; } = new();
    }
}
