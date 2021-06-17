using ApplicationCore.Models.Request;
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
        [Route("{id:int}/purchases")]
        public async Task<IActionResult> GetUserPurchasedMovies(int id)
        {
            if (_currentUserService.UserId != id)
            {
                return Unauthorized("please send correct id");
            }
            // get userid from token and compare with id that is passed in the URL, then if they equal call the service
            // get all movies purchased by user id
            // we need to check if the client who is calling this method is send a valid jwt

            var purchasedMovies = await _userService.GetUserPurchasedMovies(id);

            if (purchasedMovies == null)
            {
                return BadRequest("No purchased movies found.");
            }

            return Ok(purchasedMovies);
        }

        [Authorize]
        [HttpPost("purchase")]
        public async Task<ActionResult> PurchaseMovie(int id)
        {
            var userId = _currentUserService.UserId.GetValueOrDefault();
            var purchaseRequest = new PurchaseRequestModel()
            {
                UserId = userId,
                MovieId = id
            };

            await _userService.PurchaseMovie(purchaseRequest);

            return Ok();
        }

        [Authorize]
        [HttpGet]
        [Route("ViewProfile")]
        public async Task<IActionResult> ViewProfile()
        {
            var userId = _currentUserService.UserId.GetValueOrDefault();

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
            var userId = _currentUserService.UserId.GetValueOrDefault();
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
            var userId = _currentUserService.UserId.GetValueOrDefault();
            if (userId != model.Id)
            {
                return Unauthorized("please send correct id");
            }
            if (ModelState.IsValid)
            {
                await _userService.EditUserProfile(model);
                return Ok();
            }

            return BadRequest("Please check input");
        }
    }
}
