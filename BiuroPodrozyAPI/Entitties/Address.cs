namespace BiuroPodrozyAPI.Entitties
{
    public class Address
    {
        public int Id { get; set; }
        public string City { get; set; }

        public string Street { get; set; }
        public string PostalCode { get; set; }
        public int TravelAgencyId { get; set; }

        public virtual TravelAgency TravelAgency { get; set; }
    }
}
