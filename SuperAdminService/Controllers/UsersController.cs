using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            // check if email already exists
            var existingUsers = await _dbContext.ScanAsync<User>(new List<ScanCondition>{
                    new ScanCondition("Email", ScanOperator.Equal, user.Email)}).GetRemainingAsync();

            if (existingUsers.Any())
                return BadRequest("User already exists");

            var allUsers = await _dbContext.ScanAsync<User>(new List<ScanCondition>()).GetRemainingAsync();

            user.Id = allUsers.Any() ? allUsers.OrderByDescending(r => r.Id).First().Id + 1 : 1;
            await _dbContext.SaveAsync(user);

            return Ok(new
            {
                Message = "User Registered Successfully",
                User = user
            });
        }

        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _dbContext.ScanAsync<User>(new List<ScanCondition>()).GetRemainingAsync());
        }
    }
}