using System;
using System.ComponentModel.DataAnnotations;

namespace SweetHome.Models
{
    public class Phone
    {
        [Key]
        public int Id { get; protected set; }
        public string Name { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public int? AnimalId { get; set; } 
        public ShelterAnimal Animal { get; set; }
        public int? ShelterId { get; set; }
        public Shelter Shelter { get; set; }
        public string FormattedPhone { get {
            return String.Format("{0:#-###-###-####}", double.Parse(PhoneNumber));
        }}
    }
}
