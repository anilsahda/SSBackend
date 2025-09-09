using Amazon.DynamoDBv2.DataModel;
using Microsoft.AspNetCore.Mvc;
using SuperAdminService.Data.Entities;

[ApiController]
[Route("api/[controller]")]
public class RoleController : ControllerBase
{
    private readonly IDynamoDBContext _dbContext;

    public RoleController(IDynamoDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddRole([FromBody] Role role)
    {
        var allRoles = await _dbContext.ScanAsync<Role>(new List<ScanCondition>()).GetRemainingAsync();
        role.Id = allRoles.Any() ? allRoles.OrderByDescending(r => r.Id).First().Id + 1 : 1;
        await _dbContext.SaveAsync(role);

        return Ok(role);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetAllRoles()
    {
        return Ok(await _dbContext.ScanAsync<Role>(new List<ScanCondition>()).GetRemainingAsync());
    }
}
