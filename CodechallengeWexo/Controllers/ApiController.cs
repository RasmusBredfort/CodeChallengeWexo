using CodechallengeWexo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using CodechallengeWexo.Models;

namespace CodechallengeWexo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public ApiController(ApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("movies")]
        public async Task<IActionResult> GetAllMoviesFromDb()
        {
            var movies = await _dbContext.Movies.ToListAsync();  // Query the database for all movies
            return Ok(movies);  // Return the list of movies as a JSON response
        }

        [HttpGet("series")]
        public async Task<IActionResult> GetAllSeriesFromDb()
        {
            var series = await _dbContext.Series.ToListAsync();  // Query the database for all series
            return Ok(series);  // Return the list of series as a JSON response
        }
    }
}
