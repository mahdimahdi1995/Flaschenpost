using Flaschenpost.Models;
using Flaschenpost.Services;
using Microsoft.AspNetCore.Mvc;

namespace Flaschenpost.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlaschenpostController : ControllerBase
    {
        // GET: api/<FlaschenpostController>
        [HttpGet("getArticles")]
        public async Task<IEnumerable<Article>> GetArticles()
        {
            return await FlaschenpostApiService.GetArticles();
        }

        [HttpPost("sortAsc")]
        public async Task<IEnumerable<Article>> SortAsc([FromBody] IEnumerable<Article> articles)
        {
            return await FlaschenpostApiService.SortAscending(articles);
        }

        [HttpPost("sortDesc")]
        public async Task<IEnumerable<Article>> SortDesc([FromBody] IEnumerable<Article> articles)
        {
            return await FlaschenpostApiService.SortDescending(articles);
        }

        [HttpPost("filteredList")]
        public async Task<IEnumerable<Article>> GetFilteredList([FromBody] List<Article> articles)
        {
            return await FlaschenpostApiService.GetFilteredList(articles);
        }

    }
}

