namespace simulation.lagerverwaltung2.Models
{
    public class Kategorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Produkt> Produkte { get; set; } = new List<Produkt>();
    }
}
