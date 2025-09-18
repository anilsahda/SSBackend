using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SocietiesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SocietiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Societies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Society>>> GetSocieties()
        {
            return await _context.Societies
                                 .Include(s => s.Houses)
                                 .ToListAsync();
        }

        // GET: api/Societies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Society>> GetSociety(int id)
        {
            var society = await _context.Societies
                                        .Include(s => s.Houses)
                                        .FirstOrDefaultAsync(s => s.Id == id);

            if (society == null)
                return NotFound();

            return society;
        }

        // POST: api/Societies
        [HttpPost]
        public async Task<ActionResult<Society>> PostSociety(Society society)
        {
            _context.Societies.Add(society);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSociety), new { id = society.Id }, society);
        }

        // PUT: api/Societies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSociety(int id, Society society)
        {
            if (id != society.Id)
                return BadRequest("Society ID mismatch.");

            _context.Entry(society).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocietyExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Societies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSociety(int id)
        {
            var society = await _context.Societies.FindAsync(id);
            if (society == null)
                return NotFound();

            _context.Societies.Remove(society);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SocietyExists(int id)
        {
            return _context.Societies.Any(e => e.Id == id);
        }
    }
}
