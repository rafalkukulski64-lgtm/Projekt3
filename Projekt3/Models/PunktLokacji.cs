using System;

namespace Projekt3.Models
{
    public class PunktLokacji
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime DataPomiaru { get; set; }

        public int PokojId { get; set; }
        public Pokoj? Pokoj { get; set; }
    }
}
