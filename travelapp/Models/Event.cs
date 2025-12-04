namespace travelapp.Models
{
    public class Event
    {
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public double duration { get; set; }
        public City eventCity{get;set;}
    }
}