using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShop.MVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Services;
using ApplicationCore.ServiceInterfaces;

namespace MovieShop.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            // _movieService should have an instance of a class that implements IMovieService
            _movieService = movieService;
        }

        public async Task< IActionResult> Index()
        {
            // We need to go to database and display top revenue movies
            // thin controllers.....

            var movies = await _movieService.GetTopRevenueMovies();

            // send the data to the view so that the view can display the top movies
            // 1. passing the data from my Controller to my view using strongly typed Models *****
            // 2. ViewBag
            // 3. ViewData

            ViewBag.MoviesCount = movies.Count;
            ViewBag.PageTitle = "Top Revenue Movies";

            ViewData["MyCustomData"] = "Some Information";

            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
