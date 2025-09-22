using Amazon.DynamoDBv2.DataModel;
using DigitalLibraryAdminService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibraryAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDynamoDBContext _dbContext;
        public StudentsController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddStudent")]
        public async Task<IActionResult> AddStudent([FromBody] Student Student)
        {
            var allStudent = await _dbContext.ScanAsync<Student>(new List<ScanCondition>()).GetRemainingAsync();
            Student.Id = allStudent.Any() ? allStudent.OrderByDescending(r => r.Id).First().Id + 1 : 1;
            await _dbContext.SaveAsync(Student);

            return Ok(Student);
        }

        [HttpGet("GetStudent")]
        public async Task<IActionResult> GetStudent()
        {
            return Ok(await _dbContext.ScanAsync<Student>(new List<ScanCondition>()).GetRemainingAsync());
        }

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent([FromBody] Student Student)
        {
            await _dbContext.SaveAsync(Student);
            return Ok("Data updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentById(int id)
        {
            await _dbContext.DeleteAsync<Student>(id);
            return Ok("Data deleted successfully!");
        }
    }
}

