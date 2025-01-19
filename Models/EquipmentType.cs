namespace NewWebApplication2.Models
{
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // Kod typu urządenia (maszyny)
        public string Name { get; set; } = string.Empty; //Nazwa typu urządenia (maszyny)
        public decimal Area { get; set; } // Zajmowane pole typu urządenia (maszyny)

        // Łącza z kontraktami
        public ICollection<PlacementContract> PlacementContracts { get; set; } = new List<PlacementContract>();
    }
}
