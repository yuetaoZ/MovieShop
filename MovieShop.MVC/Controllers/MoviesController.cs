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
        public IActionResult Details(int id)
        {
            var movie = _movieService.GetMovieDetailsById(id);
            return View(movie);
        }

        [HttpGet]
        public IActionResult TopRatedMovies()
        {
            return View();
        }
    }
}
