using System;
using Microsoft.AspNetCore.Mvc;
using SweetHome.Models;

namespace SweetHome.Controllers
{
    public class SheltersController : Controller
    {
        private SweetHomeContext context;
        public SheltersController(SweetHomeContext context)
        {
            this.context = context;
        }

        public IActionResult Shelter(string shelter)
        {
            return View(shelter);
        }
    }
       
}
