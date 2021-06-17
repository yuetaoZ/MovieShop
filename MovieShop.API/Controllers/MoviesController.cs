using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;

        public MoviesController(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        [HttpGet]
        [Route("toprevenue")]
        // api/movies/toprevenue
        public async Task<IActionResult> GetHighestGrossingMovies()
        {
            var movies = await _movieService.GetTopRevenueMovies();

            if (movies.Any())
            {
                return Ok(movies);
            }
            return NotFound("No movies found");
        }
        
        [HttpGet]
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _movieService.GetMovieDetailsById(id);

            if (movieDetails == null)
            {
                return NotFound("No movie details found.");
            }

            return Ok(movieDetails);
        }

        [HttpGet]
        [Route("Genre")]
        public async Task<IActionResult> Genre(int id)
        {
            var moviecards = await _genreService.GetMoviesByGenreId(id);

            if (moviecards == null)
            {
                return NotFound("No movies found for Genre");
            }

            return Ok(moviecards);
        }

        [HttpGet]
        [Route("Reviews")]
        public async Task<IActionResult> Reviews(int id)
        {
            var reviews = await _movieService.GetReviesByMovieId(id);

            if (reviews == null)
            {
                return NotFound("No reviews found for movie.");
            }

            return Ok(reviews);
        }

        [HttpGet]
        [Route("TopRated")]
        public async Task<IActionResult> Toprated()
        {
            var movies = await _movieService.GetTopRatedMovies();

            if (movies == null)
            {
                return NotFound("No movies found.");
            }

            return Ok(movies);
        }
    }
}
