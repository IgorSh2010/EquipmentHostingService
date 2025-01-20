using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewWebApplication2.Data;
using Microsoft.EntityFrameworkCore;
using NewWebApplication2.Models.DTOs;
using NewWebApplication2.Models;

namespace NewWebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementContractsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PlacementContractsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlacementContract([FromBody] CreatePlacementContractDto dto)
        {
            //Walidacja danych 
            if (string.IsNullOrWhiteSpace(dto.ProductionFacilityCode) ||
                string.IsNullOrWhiteSpace(dto.EquipmentTypeCode) ||
                dto.Quantity <= 0)
            {
                return BadRequest("Data is incorrect.");
            }

            // Otrzymujemy obiekt produkcyjny
            var productionFacility = await _context.ProductionFacilities
                .FirstOrDefaultAsync(pf => pf.Code == dto.ProductionFacilityCode);

            if (productionFacility == null)
            {
                return NotFound("The production facility was not found." + dto.ProductionFacilityCode);
            }

            // Otrzymujemy rodzaj sprzętu (maszyny)
            var equipmentType = await _context.EquipmentTypes
                .FirstOrDefaultAsync(et => et.Code == dto.EquipmentTypeCode);

            if (equipmentType == null)
            {
                return NotFound("Equipment type not found.");
            }

            // Sprawdzanie, czy jest wystarczająca powierzchnia (pole)
            decimal usedArea = await _context.PlacementContracts
                .Where(pc => pc.ProductionFacilityId == productionFacility.Id)
                .SumAsync(pc => pc.Quantity * pc.EquipmentType.Area);

            decimal freeArea = productionFacility.StandardArea - usedArea;
            if (freeArea < dto.Quantity * equipmentType.Area)
            {
                return BadRequest("Not enough free space.");
            }

            // Dodajemy umowę
            var newContract = new PlacementContract
            {
                ProductionFacilityId = productionFacility.Id,
                EquipmentTypeId = equipmentType.Id,
                Quantity = dto.Quantity
            };

            _context.PlacementContracts.Add(newContract);
            await _context.SaveChangesAsync();

            return Ok("The contract has been successfully created.");
        }

        [HttpGet]
        public async Task<ActionResult<List<PlacementContractDto>>> GetPlacementContracts()
        {
            var contracts = await _context.PlacementContracts
                .Include(pc => pc.ProductionFacility)
                .Include(pc => pc.EquipmentType)
                .Select(pc => new PlacementContractDto
                {
                    ProductionFacilityName = pc.ProductionFacility.Name,
                    EquipmentTypeName = pc.EquipmentType.Name,
                    Quantity = pc.Quantity
                })
                .ToListAsync();

            return Ok(contracts);
        }
    }
}
