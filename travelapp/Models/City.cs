using System.ComponentModel.DataAnnotations;

namespace travelapp.Models
{
    public class City
    {
        [Key]
        public int Id { get; set; }

        public int plateCode { get; set; }

        public string name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
