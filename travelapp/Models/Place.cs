using System.ComponentModel.DataAnnotations;
//model with some validation rules
namespace travelapp.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CityId { get; set; }
        public City? City { get; set; }
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();

    }
}