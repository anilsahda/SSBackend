using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSHouseAdminService.Data;
using SSHouseAdminService.Data.Entities;

namespace SSHouseAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberReportsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MemberReportsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/MemberReports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberReport>>> GetMemberReports()
        {
            return await _context.MemberReports
                .Include(r => r.Member)
                .ToListAsync();
        }

        // GET: api/MemberReports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MemberReport>> GetMemberReport(int id)
        {
            var report = await _context.MemberReports
                .Include(r => r.Member)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (report == null)
                return NotFound();

            return report;
        }

        // POST: api/MemberReports
        [HttpPost]
        public async Task<ActionResult<MemberReport>> PostMemberReport(MemberReport memberReport)
        {
            // Optional: Validate MemberId exists
            var memberExists = await _context.Members.AnyAsync(m => m.Id == memberReport.MemberId);
            if (!memberExists)
                return BadRequest("Invalid MemberId.");

            _context.MemberReports.Add(memberReport);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMemberReport), new { id = memberReport.Id }, memberReport);
        }

        // PUT: api/MemberReports/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMemberReport(int id, MemberReport memberReport)
        {
            if (id != memberReport.Id)
                return BadRequest();

            _context.Entry(memberReport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MemberReportExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/MemberReports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMemberReport(int id)
        {
            var memberReport = await _context.MemberReports.FindAsync(id);
            if (memberReport == null)
                return NotFound();

            _context.MemberReports.Remove(memberReport);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MemberReportExists(int id)
        {
            return _context.MemberReports.Any(e => e.Id == id);
        }
    }
}
