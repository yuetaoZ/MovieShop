using ApplicationCore.Models.Response;
using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
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
    public class UserController : ControllerBase
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
        [Route("PurchasedMovies")]
        public async Task<IActionResult> PurchasedMovies()
        {
            var userId = _currentUserService.UserId;

            var purchasedMovies = await _userService.GetUserPurchasedMovies(userId);
            
            if (purchasedMovies.Any())
            {
                return Ok(purchasedMovies);
            }

            return BadRequest("No movies found");
        }

        [Authorize]
        [HttpGet]
        [Route("ViewProfile")]
        public async Task<IActionResult> ViewProfile()
        {
            var userId = _currentUserService.UserId;

            var userProfile = await _userService.GetUserProfile(userId);

            if (userProfile != null)
            {
                return Ok(userProfile);
            }

            return BadRequest("No user profile found");
        }

        [Authorize]
        [HttpGet]
        [Route("EditProfile")]
        public async Task<IActionResult> EditProfile()
        {
            var userId = _currentUserService.UserId;
            var userProfile = await _userService.GetUserProfile(userId);

            if (userProfile != null)
            {
                return Ok(userProfile);
            }

            return BadRequest("No user profile found");
        }

        [Authorize]
        [HttpPost]
        [Route("EditProfile")]
        public async Task<IActionResult> EditProfile([FromBody] UserProfileResponseModel model)
        {
            if (ModelState.IsValid)
            {
                await _userService.EditUserProfile(model);
                return Ok();
            }

            return BadRequest("Please check input");
        }
    }
}
