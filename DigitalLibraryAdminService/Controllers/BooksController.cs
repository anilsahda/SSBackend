using Amazon.DynamoDBv2.DataModel;
using DigitalLibraryAdminService.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DigitalLibraryAdminService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IDynamoDBContext _dbContext;

        public BooksController(IDynamoDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook([FromBody] Book Book)
        {
            var allBooks = await _dbContext.ScanAsync<Book>(new List<ScanCondition>()).GetRemainingAsync();
            Book.Id = allBooks.Any() ? allBooks.OrderByDescending(r => r.Id).First().Id + 1 : 1;
            await _dbContext.SaveAsync(Book);

            return Ok(Book);
        }

        [HttpGet("GetBooks")]
        public async Task<IActionResult> GetBooks()
        {
            return Ok(await _dbContext.ScanAsync<Book>(new List<ScanCondition>()).GetRemainingAsync());
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook([FromBody] Book Book)
        {
            await _dbContext.SaveAsync(Book);
            return Ok("Data updated successfully!");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookById(int id)
        {
            await _dbContext.DeleteAsync<Book>(id);
            return Ok("Data deleted successfully!");
        }
    }

}
   
