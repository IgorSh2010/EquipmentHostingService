namespace NewWebApplication2.Models
{
    public class ProductionFacility
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // Kod przestrzeni
        public string Name { get; set; } = string.Empty; // Nazwisko przestrzeni
        public decimal StandardArea { get; set; } // Standardowe pole przestrzeni

        // Łącza z kontraktami
        public ICollection<PlacementContract> PlacementContracts { get; set; } = new List<PlacementContract>();
    }
}
