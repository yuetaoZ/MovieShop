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
        public MoviesController(IMovieService service)
        {
            _movieService = service;
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
            return View();
        }
    }
}
