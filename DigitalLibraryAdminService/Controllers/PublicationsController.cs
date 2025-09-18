using Amazon.DynamoDBv2.DataModel;
using DigitalLibraryAdminService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibraryAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicationsController : ControllerBase
    {
        private readonly IDynamoDBContext _dbContext;

        public PublicationsController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddPublication")]
        public async Task<IActionResult> AddPublication([FromBody] Publication publication)
        {
            var allpublications = await _dbContext.ScanAsync<Publication>(new List<ScanCondition>()).GetRemainingAsync();
            publication.Id = allpublications.Any() ? allpublications.OrderByDescending(r => r.Id).First().Id + 1 : 1;
            await _dbContext.SaveAsync(publication);

            return Ok(publication);
        }

        [HttpGet("GetPublications")]
        public async Task<IActionResult> GetPublications()
        {
            return Ok(await _dbContext.ScanAsync<Publication>(new List<ScanCondition>()).GetRemainingAsync());
        }

        [HttpPut("UpdatePublication")]
        public async Task<IActionResult> UpdatePublication([FromBody] Publication Publication)
        {
            await _dbContext.SaveAsync(Publication);
            return Ok("Data updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublicationById(int id)
        {
            await _dbContext.DeleteAsync<Publication>(id);
            return Ok("Data deleted successfully!");
        }
    }

}
