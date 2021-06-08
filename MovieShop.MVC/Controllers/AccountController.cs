using ApplicationCore.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShop.MVC.Controllers
{
    public class AccountController : Controller
    {
       [HttpGet]
        public IActionResult Register()
        {
            //.. show a vie with empty text boxes for name, dob, email, password
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserRegisterRequestModel model)
        {
            // check for model validation on server also
            if (ModelState.IsValid)
            {
                // save to database
            }
            // take name, dob, email... and save it to database.
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginRequestModel model)
        {
            if (ModelState.IsValid)
            {
                // login
            }

            return View();
        }
    }
}
