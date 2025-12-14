// File: Models/Feedback.cs
using System;
using System.ComponentModel.DataAnnotations;
//model with some validation rules
namespace travelapp.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Required]
        public string Comment { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public int Rating { get; set; }

    }
}
