using System.ComponentModel.DataAnnotations; // Key niteliği için gerekli

namespace travelapp.Models
{
    public class City
    {
        // CityId: Primary Key olarak tanımlandı
        [Key]
        public int CityId { get; set; } // Property olmalı

        // Name: Public property olmalı
        public string Name { get; set; }

        // CityUrl: Public property olmalı
        public string? CityUrl { get; set; }

        // İlişki (Navigation Property)
        public List<Place> Places { get; set; } = new();
    }
}