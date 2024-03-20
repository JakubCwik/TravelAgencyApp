using System.Collections.Generic;

namespace BiuroPodrozyAPI.Models
{
    public class TravelAgencyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ContactEmail { get; set; }
        public string ContactNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public List<OfferDto> Offers { get; set; }
    }
}
