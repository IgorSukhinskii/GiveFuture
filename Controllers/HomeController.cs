using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SweetHome.Models;
using Microsoft.EntityFrameworkCore;
using SweetHome.Utils;

namespace SweetHome.Controllers
{
    public class HomeController : Controller
    {
        private SweetHomeContext context;
        private Random rng;
        public HomeController(SweetHomeContext context)
        {
            this.context = context;
            this.rng = new Random();
        }
        public IActionResult Index()
        {
            ViewBag.PageAction = "Index";
            var dogs = context.ShelterAnimals
                .Where(animal => animal.AnimalType == AnimalType.Dog)
                .Include(animal => animal.Shelter)
                .Include(animal => animal.Images)
                .Include(animal => animal.Phones)
                .ToList();
            var cats = context.ShelterAnimals
                .Where(animal => animal.AnimalType == AnimalType.Cat)
                .Include(animal => animal.Shelter)
                .Include(animal => animal.Images)
                .Include(animal => animal.Phones)
                .ToList();
            var twoDogs = dogs.OrderBy(_ => rng.Next()).Take(2).ToList();
            var twoCats = cats.OrderBy(_ => rng.Next()).Take(2).ToList();
            if(twoDogs.Count < 2 || twoCats.Count < 2)
            {
            	ViewBag.Animals = twoDogs.Concat(twoCats);
            }
            else
            {
            	ViewBag.Animals = new List<ShelterAnimal> { twoDogs[0], twoCats[0], twoCats[1], twoDogs[1] };
            }
            return View();
        }

        public IActionResult Volunteers()
        {
            ViewBag.PageAction = "Volunteers";
            return View();
        }

        public IActionResult Animals(
            [FromQuery(Name = "animal_id")] int? animalId,
            [FromQuery(Name = "shelter")] int? shelterId,
            [FromQuery(Name = "type")] AnimalType? animalType,
            [FromQuery] Color? color,
            [FromQuery(Name = "age_less")] int? ageLess,
            [FromQuery(Name = "size")] Size? animalSize,
            [FromQuery(Name = "gender")] Gender? animalGender,
            [FromQuery(Name = "q")] string searchQuery)
        {
            ViewBag.PageAction = "Animals";
            ViewBag.AnimalId = animalId;
            ViewBag.Type = null;
            ViewBag.Color = null;
            ViewBag.AgeLess = null;
            ViewBag.Size = null;
            ViewBag.Gender = null;
            ViewBag.All = true;
            IQueryable<Shelter> shelters = context.Shelters;
            ViewBag.Shelters = shelters.ToList();
            IQueryable<ShelterAnimal> animals = context.ShelterAnimals;
            if (searchQuery != null)
            {
                ViewBag.All = false;
                ViewBag.SearchQuery = searchQuery;
                animals = animals.Where(animal => animal.Name.ToLower().Contains(searchQuery.ToLower()) || animal.Shelter.Name.ToLower().Contains(searchQuery.ToLower()));
            }
            shelterId.IfNotNull(id => {
                ViewBag.ShelterId = id;
                ViewBag.All = false;
                animals = animals.Where(animal => animal.Shelter.Id == id);
            });
            animalType.IfNotNull(type => {
                ViewBag.Type = type;
                ViewBag.All = false;
                animals = animals.Where(animal => animal.AnimalType == type);
            });
            color.IfNotNull(col => {
                ViewBag.Color = col;
                ViewBag.All = false;
                animals = animals.Where(animal => animal.Color == col);
            });
            ageLess.IfNotNull(age => {
                ViewBag.AgeLess = age;
                ViewBag.All = false;
                DateTime birthDay = DateTime.UtcNow.AddYears(-age);
                animals = animals.Where(animal => animal.BirthDay.Check(bday => bday >= birthDay));
            });
            animalSize.IfNotNull(size => {
                ViewBag.Size = size;
                ViewBag.All = false;
                animals = animals.Where(animal => animal.Size == size);
            });
            animalGender.IfNotNull(gender => {
                ViewBag.Gender = gender;
                ViewBag.All = false;
                animals = animals.Where(animal => animal.Gender == gender);
            });
            var returns = animals
                .Include(animal => animal.Shelter)
                .Include(animal => animal.Images)
                .Include(animal => animal.Phones)
                .ToList();
            return View(returns);
        }

        public IActionResult Shelters()
        {
            ViewBag.PageAction = "Shelters";
            var shelters = context.Shelters.Include(shelter => shelter.Phones).ToList();
            return View(shelters);            
        }

        public IActionResult New()
        {
            ViewBag.PageAction = "New";
            var shelters = context.Shelters.ToList();
            ViewBag.Shelters = shelters;
            return View();
        }

        private string RemoveExtraText(string value)
        {
            var allowedChars = "01234567890,";
           
            return new string(value.Replace("+7","8").Where(c => allowedChars.Contains(c)).ToArray());
        }
        
        [HttpPost]
        public IActionResult AddAnimal(string animalName,
            string animalType, string info, string images,
            string shelterId, string ownerName, string phoneNumbers,
            string age)
        {
            var shelterIdNum = 1;
            if (shelterId != null){
                shelterIdNum = Int32.Parse(shelterId);
            }
            AnimalType animalTypeEnum;
            Enum.TryParse(animalType, out animalTypeEnum);
            if (animalName == null) {
                animalName = "";
            }
            if (info == null) {
                info = "";
            }
            if (images == null) {
                images = "";
            }
            
            DateTime? birthday = null;
            int months;
            if (Int32.TryParse(age, out months))
            {
                birthday = DateTime.UtcNow.AddMonths(- months);
            }
            phoneNumbers = RemoveExtraText(phoneNumbers);
            var imagesList = new List<string>(images.Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries))
                .Select(s => new Image { Url = s }).ToList();
            var phones = new List<string>(phoneNumbers.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries))
                .Select(s => new Phone { PhoneNumber = s }).ToList();
            var animal = new ShelterAnimal
            {
                Name = animalName.ToLower(),
                AnimalType = animalTypeEnum,
                OwnerName = ownerName,
                Info = info,
                BirthDay = birthday,
                Images = imagesList,
                Phones = phones,
                Created = DateTime.UtcNow,
                ShelterId = shelterIdNum
            };
            context.ShelterAnimals.Add(animal);
            context.SaveChanges();
            
            return RedirectToAction("New");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
