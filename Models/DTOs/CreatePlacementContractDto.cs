namespace NewWebApplication2.Models.DTOs
{
    public class CreatePlacementContractDto
    {
        public string ProductionFacilityCode { get; set; } 
        public string EquipmentTypeCode { get; set; }     
        public int Quantity { get; set; }
    }
}
