using ApplicationCore.Models.Request;
using ApplicationCore.Models.Response;
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

            var userId = _currentUserService.UserId.GetValueOrDefault();
            // get the user id
            //
            // make a request to the database and get info from purchase table 
            // select * from Purchase where userid = @getfromcookie
            var purchasedMovies = await _userService.GetUserPurchasedMovies(userId);
            
            return View(purchasedMovies);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PurchaseMovie(int id)
        {
            // get userid from CurrentUser and create a row in Purchase Table
            var userId = _currentUserService.UserId.GetValueOrDefault();
            var purchaseRequest = new PurchaseRequestModel()
            {
                UserId = userId,
                MovieId = id
            };

            await _userService.PurchaseMovie(purchaseRequest);

            return View();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ViewProfile()
        {
            var userId = _currentUserService.UserId.GetValueOrDefault();
            var userProfile = await _userService.GetUserProfile(userId);

            return View(userProfile);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var userId = _currentUserService.UserId.GetValueOrDefault();
            var userProfile = await _userService.GetUserProfile(userId);

            return View(userProfile);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditProfile(UserProfileResponseModel model)
        {
            if (ModelState.IsValid)
            {
                //save to database
                await _userService.EditUserProfile(model);
                // redirect to Login 
            }
            // take name, dob, email, pasword from view and save it to database
            return RedirectToAction("ViewProfile");
        }

    }
}

