﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcDemo.Models;
using System.Diagnostics;

namespace MvcDemo.Controllers
{
    public class HomeController : Controller
    {
      

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }


        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}