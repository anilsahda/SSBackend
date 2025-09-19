using Amazon.DynamoDBv2.DataModel;
using DigitalLibraryAdminService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibraryAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueBookController : ControllerBase
    {
        private readonly IDynamoDBContext _dbContext;
        public IssueBookController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("AddIssueBook")]
        public async Task<IActionResult> AddIssueBook([FromBody] IssueBook IssueBook)
        {
            var allIssueBooks = await _dbContext.ScanAsync<IssueBook>(new List<ScanCondition>()).GetRemainingAsync();
            IssueBook.Id = allIssueBooks.Any() ? allIssueBooks.OrderByDescending(r => r.Id).First().Id + 1 : 1;
            await _dbContext.SaveAsync(IssueBook);

            return Ok(IssueBook);
        }

        [HttpGet("GetIssueBooks")]
        public async Task<IActionResult> GetIssueBooks()
        {
            return Ok(await _dbContext.ScanAsync<IssueBook>(new List<ScanCondition>()).GetRemainingAsync());
        }

        [HttpPut("UpdateIssueBook")]
        public async Task<IActionResult> UpdateIssueBook([FromBody] IssueBook IssueBook)
        {
            await _dbContext.SaveAsync(IssueBook);
            return Ok("Data updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssueBookById(int id)
        {
            await _dbContext.DeleteAsync<IssueBook>(id);
            return Ok("Data deleted successfully!");
        }
    }
}

