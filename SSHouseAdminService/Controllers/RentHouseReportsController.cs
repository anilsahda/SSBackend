using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentHouseReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RentHouseReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/RentHouseReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentHouseReport>>> GetRentHouseReports()
        {
            return await _context.RentHouseReports
                .Include(r => r.House)
                .ToListAsync();
        }

        // GET: api/RentHouseReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RentHouseReport>> GetRentHouseReport(int id)
        {
            var report = await _context.RentHouseReports
                .Include(r => r.House)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                return NotFound();

            return report;
        }

        // POST: api/RentHouseReports
        [HttpPost]
        public async Task<ActionResult<RentHouseReport>> PostRentHouseReport(RentHouseReport rentHouseReport)
        {
            // Optional: Validate if House exists
            var houseExists = await _context.Houses.AnyAsync(h => h.Id == rentHouseReport.HouseId);
            if (!houseExists)
                return BadRequest($"House with Id {rentHouseReport.HouseId} does not exist.");

            _context.RentHouseReports.Add(rentHouseReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRentHouseReport), new { id = rentHouseReport.Id }, rentHouseReport);
        }

        // PUT: api/RentHouseReports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRentHouseReport(int id, RentHouseReport rentHouseReport)
        {
            if (id != rentHouseReport.Id)
                return BadRequest();

            _context.Entry(rentHouseReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentHouseReportExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/RentHouseReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRentHouseReport(int id)
        {
            var rentHouseReport = await _context.RentHouseReports.FindAsync(id);
            if (rentHouseReport == null)
                return NotFound();

            _context.RentHouseReports.Remove(rentHouseReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RentHouseReportExists(int id)
        {
            return _context.RentHouseReports.Any(e => e.Id == id);
        }
    }
}
