using System.ComponentModel.DataAnnotations;

namespace travelapp.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Event date is required")]
        public DateTime Date { get; set; }

        public string? ImageUrl { get; set; }
    }
}
