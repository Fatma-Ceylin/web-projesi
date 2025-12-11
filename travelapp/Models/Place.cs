using System.ComponentModel.DataAnnotations;

namespace travelapp.Models
{
    public class Place
    {
        [Key]
        public int PlaceId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; } // Resim yolu

        // Foreign Key: Hangi şehre ait?
        public int CityId { get; set; }
        public City City { get; set; }

        // Bir mekanın çokça yorumu olabilir
        public List<Feedback> Feedbacks { get; set; } = new List<Feedback>();
    }
}
