using System;
using Microsoft.AspNetCore.Mvc;
using SweetHome.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SweetHome.Controllers
{
    public class SheltersController : Controller
    {
        private SweetHomeContext context;
        public SheltersController(SweetHomeContext context)
        {
            this.context = context;
        }

        public IActionResult Shelter(string shelterName)
        {
        	var animals = context.ShelterAnimals
        		.Where(animal => animal.Shelter.Id == 4) // TODO: ADD ACTUAL SHELTER SELECTION LOGIC
        		.Include(animal => animal.Shelter)
        		.Include(animal => animal.Images)
				.Include(animal => animal.Phones)
        		.ToList();
            return View(animals);
        }
    }
       
}
