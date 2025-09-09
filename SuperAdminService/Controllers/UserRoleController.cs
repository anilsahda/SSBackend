using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using SuperAdminService.Data.Entities;

[ApiController]
[Route("api/[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IDynamoDBContext _dbContext;

    public UserRoleController(IDynamoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("assign")]
    public async Task<IActionResult> AssignRole([FromBody] UserRole userRole)
    {
        var allUserRoles = await _dbContext.ScanAsync<UserRole>(new List<ScanCondition>()).GetRemainingAsync();
        userRole.Id = allUserRoles.Any() ? allUserRoles.OrderByDescending(ur => ur.Id).First().Id + 1 : 1;
        await _dbContext.SaveAsync(userRole);

        return Ok(userRole);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllUserRoles()
    {
        return Ok(await _dbContext.ScanAsync<UserRole>(new List<ScanCondition>()).GetRemainingAsync());
    }
}
