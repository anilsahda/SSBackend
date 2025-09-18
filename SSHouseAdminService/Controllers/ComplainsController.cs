using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComplainsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ComplainsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Complains
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Complain>>> GetAllComplaints()
        {
            return await _context.Complains
                .Include(c => c.House)
                .Include(c => c.Member)
                .OrderByDescending(c => c.ComplainDate)
                .ToListAsync();
        }

        // GET: api/Complains/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Complain>> GetComplain(int id)
        {
            var complain = await _context.Complains
                .Include(c => c.House)
                .Include(c => c.Member)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (complain == null)
                return NotFound();

            return complain;
        }

        // GET: api/Complains/unresolved
        [HttpGet("unresolved")]
        public async Task<ActionResult<IEnumerable<Complain>>> GetUnresolvedComplaints()
        {
            return await _context.Complains
                .Include(c => c.House)
                .Include(c => c.Member)
                .Where(c => !c.IsResolved)
                .OrderByDescending(c => c.ComplainDate)
                .ToListAsync();
        }

        // POST: api/Complains
        [HttpPost]
        public async Task<ActionResult<Complain>> CreateComplain(Complain complain)
        {
            // Optional: validate House exists
            var houseExists = await _context.Houses.AnyAsync(h => h.Id == complain.HouseId);
            if (!houseExists)
                return BadRequest("Invalid HouseId");

            if (complain.MemberId.HasValue)
            {
                var memberExists = await _context.Members.AnyAsync(m => m.Id == complain.MemberId);
                if (!memberExists)
                    return BadRequest("Invalid MemberId");
            }

            complain.ComplainDate = DateTime.UtcNow;

            _context.Complains.Add(complain);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetComplain), new { id = complain.Id }, complain);
        }

        // PUT: api/Complains/5/resolve
        [HttpPut("{id}/resolve")]
        public async Task<IActionResult> ResolveComplain(int id)
        {
            var complain = await _context.Complains.FindAsync(id);
            if (complain == null)
                return NotFound();

            if (complain.IsResolved)
                return BadRequest("Complain is already resolved.");

            complain.IsResolved = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
