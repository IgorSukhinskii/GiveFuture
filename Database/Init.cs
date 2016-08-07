using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using SweetHome.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SweetHome.Database
{
    public class Init
    {

       private SweetHomeContext context;
        public Init()
        {
            var options = new DbContextOptionsBuilder<SweetHomeContext>().UseNpgsql(@"Server=localhost;Database=give_future;Username=give_future;Password=1q2w3e4r").Options;
            this.context = new SweetHomeContext(options);
        }
        private void ParseShelters(string fileContents)
        {
            var shelters = fileContents.Split(new string[]{"\n\n\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(shelterString => 
                {
                    var shelterFields = shelterString.Split(new char[]{'\n'});
                    return new Shelter
                    {
                        Name = shelterFields[0],
                        Address = shelterFields[1],
                        Phones = shelterFields[2].Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries).Select(s => new Phone { PhoneNumber = s }).ToList(),
						Info = shelterFields[3],
						VKGroup = shelterFields[4],
						ImageUrl = shelterFields[5],
                        Url = shelterFields[6]
                    };
                });
            Console.WriteLine("Finished parsing file");
            Console.WriteLine("Saving entries to the database...");
            foreach(var shelter in shelters)
            {
                context.Shelters.Add(shelter);
            }
            context.SaveChanges();
            Console.WriteLine("Finished saving items to the database");
        }
        private void ParseAnimals(string fileContents)
        {
            var animals = fileContents.Split(new string[]{"\n\n\n\n"}, StringSplitOptions.RemoveEmptyEntries)
                .Select(animalString =>
                {
                    var animalFields = animalString.Split(new string[]{"\n\n"}, StringSplitOptions.RemoveEmptyEntries);
                    
                    DateTime? birthday = null;
                    int months;
                    
                    if (Int32.TryParse(animalFields[3], out months))
                    {
                        birthday = DateTime.UtcNow.AddMonths(- months);
                    }
                    string type = animalFields[1];
                    AnimalType animalType;
                    if (type == "0") {
                        animalType = AnimalType.Dog;
                    } else {
                        animalType = AnimalType.Cat;
                    }   
                   
                    Gender gender;                    
                    gender = Gender.Unknown;
                    IList<String> Images = new List<String>(animalFields[5].Split(new char[]{'\n'}, StringSplitOptions.RemoveEmptyEntries));
                    //  Images.RemoveAt(Images.Count-1);
                    return new ShelterAnimal
                    {
                        Name = animalFields[2].ToLower(),
                        AnimalType = animalType,
                        BirthDay = birthday,
                        Gender = gender,
                        Info = animalFields[4],
                        Images = Images.Select(s => new Image { Url = s }).ToList(),
                        Created = DateTime.UtcNow,
                        ShelterId = Int32.Parse(animalFields[0])
                    };
                });
            Console.WriteLine("Finished parsing file");
            Console.WriteLine("Saving entries to the database...");
            foreach(var animal in animals)
            {
                try
                {
                    context.ShelterAnimals.Add(animal);
                    context.SaveChanges();
                    Console.WriteLine("Saved " + animal.Name);
                }
                catch (System.Exception e)
                {
                    Console.WriteLine(e.InnerException.Message);
                }
            }
            Console.WriteLine("Finished saving items to the database");
        }
        public void Initialize(string[] args)
        {
            Console.WriteLine("Started");
            Console.WriteLine("Reading configuration...");
            var config = new ConfigurationBuilder()
                            .AddCommandLine(args).Build();
            Console.WriteLine("Finished reading configuration");

            string filename = config["file"];
            string type = config["type"];
            
            string line = null;
            Console.WriteLine("Reading file...");
            using (StreamReader sr = File.OpenText(filename))
                line = sr.ReadToEnd();
            Console.WriteLine("Finished reading file");
            Console.WriteLine("Parsing file...");
            string command = config["command"];
            if (command == "animals")
            {
                ParseAnimals(line);

            }
            else if (command == "shelters")
            {
                ParseShelters(line);
            }
            else
            {
                Console.WriteLine("Unknown command");
            }
        }
    }
}