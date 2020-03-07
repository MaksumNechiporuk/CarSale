﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CarSale.Models;
using Microsoft.AspNetCore.Authorization;

namespace CarSale.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]

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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
