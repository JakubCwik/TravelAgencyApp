using System;

namespace BiuroPodrozyAPI.Entitties
{
    public class Offer
    {
        public int Id { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set;}
        public string Destination { get; set; }
        public float Price { get; set; }
        public bool TransportationAirportHotel { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TravelAgencyId { get; set; }
        public virtual TravelAgency TravelAgency { get; set; }

    }
    
}
