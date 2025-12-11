using System.ComponentModel.DataAnnotations;

namespace travelapp.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        // Yorumu yapan kişi (Identity'den gelecek)
        public string? UserName { get; set; }

        [Required]
        public string Comment { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; } // 1-5 arası puan

        public DateTime Date { get; set; } = DateTime.Now;

        // Foreign Key: Hangi mekana ait?
        public int PlaceId { get; set; }
        public Place Place { get; set; }
    }
}
