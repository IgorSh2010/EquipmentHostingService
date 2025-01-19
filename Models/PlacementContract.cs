namespace NewWebApplication2.Models
{
    public class PlacementContract
    {
        public int Id { get; set; }

        // Zewnętrzny klucz na ProductionFacility
        public int ProductionFacilityId { get; set; }
        public ProductionFacility ProductionFacility { get; set; } = null!;

        // Zewnętrzny klucz na EquipmentType
        public int EquipmentTypeId { get; set; }
        public EquipmentType EquipmentType { get; set; } = null!;

        // Ilość urządzeń
        public int Quantity { get; set; }
    }
}
