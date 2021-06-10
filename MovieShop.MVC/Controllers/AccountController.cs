using ApplicationCore.Models.Request;
using ApplicationCore.ServiceInterfaces;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
       [HttpGet]
        public IActionResult Register()
        {
            // show a view with empty text boxes for name, dob, email, password
            return View();
        }

       [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel model)
        {
            if (ModelState.IsValid)
            {
                //save to database
                var user = await _userService.RegisterUser(model);
                // redirect to Login 
            }
            // take name, dob, email, pasword from view and save it to database
            return View();
        } 
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

       
        [HttpGet]
        public IActionResult CreateMovie()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateMovie(MovieCreateRequest model)
        {
            if (ModelState.IsValid)
            {
                // create movie 
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel model)
        {
            var user = await _userService.Login(model.Email, model.Password);

            if(user == null)
            {
                return View();
            }

            return View();
        }
    }
}
