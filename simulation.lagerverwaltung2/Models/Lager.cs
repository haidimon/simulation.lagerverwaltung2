namespace simulation.lagerverwaltung2.Models
{
    public class Lager
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Produkt> Produkte { get; set; } = new List<Produkt>();

    }
}
