using System.ComponentModel.DataAnnotations;

namespace SweetHome.Models
{
    public class Image
    {
        [Key]
        public int Id { get; protected set; }
        public string Comment { get; set; }
        [Required]
        public string Url { get; set; }
        public int? AnimalId { get; set; }
        public ShelterAnimal Animal { get; set; }
    }
}
