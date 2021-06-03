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
        public IActionResult Index(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetAccountInfo()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login()
        {
            return View();
        }
    }
}
