using Amazon.DynamoDBv2.DataModel;
using DigitalLibraryAdminService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibraryAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchesController : ControllerBase
    {
        private readonly IDynamoDBContext _dbContext;
        public BranchesController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost("AddBranch")]
        public async Task<IActionResult> AddBranch([FromBody] Branch Branch)
        {
            var allBranches = await _dbContext.ScanAsync<Branch>(new List<ScanCondition>()).GetRemainingAsync();
            Branch.Id = allBranches.Any() ? allBranches.OrderByDescending(r => r.Id).First().Id + 1 : 1;
            await _dbContext.SaveAsync(Branch);

            return Ok(Branch);
        }

        [HttpGet("GetBranches")]
        public async Task<IActionResult> GetBranches()
        {
            return Ok(await _dbContext.ScanAsync<Branch>(new List<ScanCondition>()).GetRemainingAsync());
        }

        [HttpPut("UpdateBranch")]
        public async Task<IActionResult> UpdateBranch([FromBody] Branch Branch)
        {
            await _dbContext.SaveAsync(Branch);
            return Ok("Data updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBranchById(int id)
        {
            await _dbContext.DeleteAsync<Branch>(id);
            return Ok("Data deleted successfully!");
        }
    }
}

