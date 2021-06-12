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
        private readonly IUserService _userService;

        public UserController(ICurrentUserService currentUserService, IUserService userService)
        {
            _currentUserService = currentUserService;
            _userService = userService; 
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> PurchasedMovies()
        {

            var userId = _currentUserService.UserId;
            // get the user id
            //
            // make a request to the database and get info from purchase table 
            // select * from Purchase where userid = @getfromcookie
            var purchasedMovies = await _userService.GetUserPurchasedMovies(userId);
            
            return View(purchasedMovies);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PurchaseMovie()
        {
            // get userid from CurrentUser and create a row in Purchase Table
            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            // get userid => get userDetails
            var userId = _currentUserService.UserId;
            var userProfile = await _userService.GetUserProfile(userId);

            return View(userProfile);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            // get userid => get userDetails
            var userId = _currentUserService.UserId;
            var userProfile = await _userService.GetUserProfile(userId);

            return View(userProfile);
        }

    }
}

