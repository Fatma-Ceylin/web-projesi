using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using travelapp.Models;
//model with some validation rules
public class Booking
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public AppUser User { get; set; }
    public int EventId { get; set; }
    public Event Event { get; set; }

    public DateTime BookingDate { get; set; } = DateTime.Now;
}
