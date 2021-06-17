using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        public MoviesController(IMovieService movieService, IGenreService genreService)
        {
            _movieService = movieService;
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var MovieDetails = await _movieService.GetMovieDetailsById(id);
            return View(MovieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> TopRatedMovies()
        {
            var response = await _movieService.GetTopRatedMovies();
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Genre(int id)
        {
            var moviecards = await _genreService.GetMoviesByGenreId(id);
            return View(moviecards);
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            var reviews = await _movieService.GetReviesByMovieId(id);
            return View(reviews);
        }
    }
}
