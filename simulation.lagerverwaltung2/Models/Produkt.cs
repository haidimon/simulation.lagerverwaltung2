using Microsoft.AspNetCore.Mvc;

namespace simulation.lagerverwaltung2.Models
{
    public class Produkt
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Anzahl { get; set; }

        public int KategorieId { get; set; }
        public Kategorie? Kategorie { get; set; }

        public int LagerId { get; set; }
        public Lager? Lager { get; set; }
    }
}
