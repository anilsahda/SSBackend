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

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromForm] User user)
        {
            await _dbContext.SaveAsync(user);
            return Ok("Data updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserById(int id)
        {
            await _dbContext.DeleteAsync<User>(id);
            return Ok("Data deleted successfully!");
        }
    }
}