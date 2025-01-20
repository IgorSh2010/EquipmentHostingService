namespace NewWebApplication2.Models
{
    public class EquipmentType
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // Kod rodzaju sprzętu (maszyny)
        public string Name { get; set; } = string.Empty; //Nazwa rodzaju sprzętu (maszyny)
        public decimal Area { get; set; } // Zajmowane pole rodzaju sprzętu (maszyny)

        // Łącza z kontraktami
        public ICollection<PlacementContract> PlacementContracts { get; set; } = new List<PlacementContract>();
    }
}
