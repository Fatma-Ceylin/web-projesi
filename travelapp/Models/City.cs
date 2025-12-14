using System.ComponentModel.DataAnnotations;
//model with some validation rules
namespace travelapp.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Plate code is required")]
        [Range(1, 81, ErrorMessage = "Plate code must be between 1 and 81")]
        public int plateCode { get; set; }
        [Required(ErrorMessage = "City name is required")]
        [StringLength(50, MinimumLength = 2)]
        public string name { get; set; }
        [StringLength(300, ErrorMessage = "Description max 300 characters")]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
        public List<Place> Places { get; set; } = new List<Place>();
    }
}
