using System;
using Microsoft.AspNetCore.Mvc;
using SweetHome.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SweetHome.Utils;
using System.Threading.Tasks;

namespace SweetHome.Controllers
{
    public class SheltersController : Controller
    {
        private SweetHomeContext context;
        private IEmailSender emailSender;
        public SheltersController(SweetHomeContext context, IEmailSender emailSender)
        {
            this.context = context;
            this.emailSender = emailSender;
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
        [HttpPost]
        public async Task<IActionResult> SendEmail(string shelterName, string name, string email, string subject, string text) {
            await emailSender.SendEmail("si1en7ium@gmail.com", "si1en7ium@gmail.com", subject, name + " " + email + " " + text);
            System.Console.WriteLine(shelterName);
            System.Console.WriteLine(name);
            System.Console.WriteLine(email);
            System.Console.WriteLine(subject);
            System.Console.WriteLine(text);
            return RedirectToAction("Shelter", new { shelterName = shelterName });
        }
    }
       
}
