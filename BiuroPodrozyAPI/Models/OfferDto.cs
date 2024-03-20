using System;

namespace BiuroPodrozyAPI.Models
{
    public class OfferDto
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public string Destination { get; set; }
        public float Price { get; set; }
        public bool TransportationAirportHotel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
