using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;

        public UserController(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetUserPurchasedMovies()
        {

            var userId = _currentUserService.UserId;
            // get the user id
            //
            // make a request to the database and get info from purchase table 
            // select * from Purchase where userid = @getfromcookie
            return View();
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PurchaseMovie()
        {
            // get userid from CurrentUser and create a row in Purchase Table
            return View();
        }
    }
}

