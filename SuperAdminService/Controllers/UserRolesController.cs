using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using SuperAdminService.Data.Entities;

[ApiController]
[Route("api/[controller]")]
public class UserRolesController : ControllerBase
{
    private readonly IDynamoDBContext _dbContext;

    public UserRolesController(IDynamoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("AddUserRole")]
    public async Task<IActionResult> AddRole([FromBody] UserRole userRole)
    {
        var allUserRoles = await _dbContext.ScanAsync<UserRole>(new List<ScanCondition>()).GetRemainingAsync();
        userRole.Id = allUserRoles.Any() ? allUserRoles.OrderByDescending(ur => ur.Id).First().Id + 1 : 1;
        await _dbContext.SaveAsync(userRole);

        return Ok(userRole);
    }

    [HttpGet("GetUserRoles")]
    public async Task<IActionResult> GetUserRoles()
    {
        return Ok(await _dbContext.ScanAsync<UserRole>(new List<ScanCondition>()).GetRemainingAsync());
    }

    [HttpPut("UpdateUserRole")]
    public async Task<IActionResult> UpdateRole([FromBody] UserRole userRole)
    {
        await _dbContext.SaveAsync(userRole);
        return Ok("Data updated successfully!");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserRoleById(int id)
    {
        await _dbContext.DeleteAsync<UserRole>(id);
        return Ok("Data deleted successfully!");
    }
}