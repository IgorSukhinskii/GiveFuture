using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SweetHome.Models
{
    public class Shelter
    {
        [Key]
        public int Id { get; protected set; }
        [Required]
        public string Name { get; set; }
        [StringLength(160)]
        public string OwnerName { get; set; }
        [StringLength(160)]
        public string Address { get; set; }
        public string Info { get; set; }
        public string VKGroup { get; set; }
        public string ImageUrl { get; set; }
        public string Url { get; set; }
        public IList<ShelterAnimal> Animals { get; set; }
        public IList<Phone> Phones { get; set; }
        public Shelter()
        {
            this.Animals = new List<ShelterAnimal>();
            this.Phones = new List<Phone>();
        }
    }
}