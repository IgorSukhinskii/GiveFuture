using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace SweetHome.Models
{
    public enum PlaceType
    {
        Shelter,
        HomeShelter,
        Home
    }
    public enum AnimalType
    {
        Dog,
        Cat,
        Other
    }
    public enum Color
    {
        Dog,
        Blond,	
        Varicoloured,
        Dark       
    }
    public enum Size
    {
        Cat,
        Small,	
        Medium,
        Large        
    }
    public enum Gender
    {
        Male,
        Female,
        Unknown
    }
    public class ShelterAnimal
    {
        [Key]
        public int Id { get; protected set; }
        [Required]
        public string Name { get; set; }
        [Required]        
        public AnimalType AnimalType { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BirthDay { get; set; }
        [Required]
        public PlaceType PlaceType { get; set; }
        public int ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        [Display(Name = "Полностью здоров")]
        public bool IsHealthy { get; set; }
        [Display(Name = "Подходит для содержания в квартире")]
        public bool IsForFlat { get; set; }
        [Display(Name = "Подходит для содержания в частном доме")]
        public bool IsForHome { get; set; }
        [Display(Name = "Приучен к выгулу/туалету")]
        public bool Toilet { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }
        public string Info { get; set; }
        public bool IsHappy { get; set; }
        [Required]
        [DataType(DataType.DateTime)]     
        public DateTime Created { get; set; }
        public Gender Gender { get; set; }
        public string OwnerName { get; set; }
        public IList<Phone> Phones { get; set; }
        public IList<Image> Images { get; set; }
        public ShelterAnimal()
        {
            this.IsHappy = false;
            this.Images = new List<Image>();
            this.Phones = new List<Phone>();
        }
    }
}