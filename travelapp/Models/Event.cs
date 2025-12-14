using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
//model with some validation rules
namespace travelapp.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Event name is required")]
        [StringLength(100)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen bir şehir seçiniz.")]
        public int? CityId { get; set; }
        [ValidateNever] 
        public City City { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Event date is required")]
        [FutureDate]
        public DateTime Date { get; set; }

        public string? ImageUrl { get; set; }
    }
}
