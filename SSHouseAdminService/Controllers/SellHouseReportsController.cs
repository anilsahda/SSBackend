using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellHouseReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SellHouseReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/SellHouseReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellHouseReport>>> GetSellHouseReports()
        {
            return await _context.SellHouseReports
                                 .Include(r => r.House)
                                 .ToListAsync();
        }

        // GET: api/SellHouseReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SellHouseReport>> GetSellHouseReport(int id)
        {
            var report = await _context.SellHouseReports
                                       .Include(r => r.House)
                                       .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                return NotFound();

            return report;
        }

        // POST: api/SellHouseReports
        [HttpPost]
        public async Task<ActionResult<SellHouseReport>> PostSellHouseReport(SellHouseReport report)
        {
            // Check if house exists
            var houseExists = await _context.Houses.AnyAsync(h => h.Id == report.HouseId);
            if (!houseExists)
                return BadRequest($"House with Id {report.HouseId} does not exist.");

            _context.SellHouseReports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSellHouseReport), new { id = report.Id }, report);
        }

        // PUT: api/SellHouseReports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSellHouseReport(int id, SellHouseReport report)
        {
            if (id != report.Id)
                return BadRequest("ID in URL does not match ID in request body.");

            var exists = await _context.SellHouseReports.AnyAsync(r => r.Id == id);
            if (!exists)
                return NotFound();

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw; // You can log this if needed
            }

            return NoContent();
        }

        // DELETE: api/SellHouseReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSellHouseReport(int id)
        {
            var report = await _context.SellHouseReports.FindAsync(id);
            if (report == null)
                return NotFound();

            _context.SellHouseReports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
