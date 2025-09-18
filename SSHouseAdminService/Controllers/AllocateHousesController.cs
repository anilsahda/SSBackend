using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AllocateHousesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AllocateHousesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AllocateHouses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AllocateHouse>>> GetAllocations()
        {
            return await _context.AllocateHouses
                .Include(a => a.House)
                .Include(a => a.Member)
                .ToListAsync();
        }

        // GET: api/AllocateHouses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AllocateHouse>> GetAllocation(int id)
        {
            var allocation = await _context.AllocateHouses
                .Include(a => a.House)
                .Include(a => a.Member)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (allocation == null)
                return NotFound();

            return allocation;
        }

        // POST: api/AllocateHouses
        // Body should contain HouseId and MemberId (and optionally AllocationDate)
        [HttpPost]
        public async Task<ActionResult<AllocateHouse>> CreateAllocation(AllocateHouse allocation)
        {
            // Optional: check if House and Member exist
            var houseExists = await _context.Houses.AnyAsync(h => h.Id == allocation.HouseId);
            var memberExists = await _context.Members.AnyAsync(m => m.Id == allocation.MemberId);

            if (!houseExists || !memberExists)
                return BadRequest("Invalid HouseId or MemberId");

            allocation.AllocationDate = allocation.AllocationDate == default
                ? DateTime.UtcNow
                : allocation.AllocationDate;

            _context.AllocateHouses.Add(allocation);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAllocation), new { id = allocation.Id }, allocation);
        }

        // PUT: api/AllocateHouses/5/release
        // Sets the ReleaseDate to now to mark allocation released
        [HttpPut("{id}/release")]
        public async Task<IActionResult> ReleaseAllocation(int id)
        {
            var allocation = await _context.AllocateHouses.FindAsync(id);
            if (allocation == null)
                return NotFound();

            if (allocation.ReleaseDate != null)
                return BadRequest("Allocation already released.");

            allocation.ReleaseDate = DateTime.UtcNow;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
