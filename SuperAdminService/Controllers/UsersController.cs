using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using SuperAdminService.Data.Entities;

namespace SuperAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IDynamoDBContext _dbContext;

        public UsersController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            var allUsers = await _dbContext.ScanAsync<User>(new List<ScanCondition>()).GetRemainingAsync();

            user.Id = allUsers.Any() ? allUsers.OrderByDescending(r => r.Id).First().Id + 1 : 1;
            await _dbContext.SaveAsync(user);

            return Ok(user);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _dbContext.ScanAsync<User>(new List<ScanCondition>()).GetRemainingAsync());
        }
    }
}