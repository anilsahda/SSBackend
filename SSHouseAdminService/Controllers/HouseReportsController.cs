using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HouseReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HouseReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/HouseReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HouseReport>>> GetHouseReports()
        {
            return await _context.HouseReports
                .Include(r => r.House)
                .OrderByDescending(r => r.ReportDate)
                .ToListAsync();
        }

        // GET: api/HouseReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HouseReport>> GetHouseReport(int id)
        {
            var report = await _context.HouseReports
                .Include(r => r.House)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                return NotFound();

            return report;
        }

        // POST: api/HouseReports
        [HttpPost]
        public async Task<ActionResult<HouseReport>> CreateHouseReport(HouseReport report)
        {
            // Optional: Validate if House exists
            var houseExists = await _context.Houses.AnyAsync(h => h.Id == report.HouseId);
            if (!houseExists)
                return BadRequest("Invalid HouseId");

            report.ReportDate = DateTime.UtcNow;

            _context.HouseReports.Add(report);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHouseReport), new { id = report.Id }, report);
        }

        // DELETE: api/HouseReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHouseReport(int id)
        {
            var report = await _context.HouseReports.FindAsync(id);
            if (report == null)
                return NotFound();

            _context.HouseReports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
