using BiuroPodrozyAPI.Entitties;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BiuroPodrozyAPI
{
    public class TravelAgencySeeder
    {
        private readonly TravelAgencyDbContext _dbContext;

        public TravelAgencySeeder(TravelAgencyDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Seed()
        {
            if(_dbContext.Database.CanConnect())
            {
                if(!_dbContext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbContext.Roles.AddRange(roles);
                    _dbContext.SaveChanges();
                }

                if(!_dbContext.TravelAgencies.Any())
                {
                    var travelAgency = GetTravelAgency();
                    _dbContext.TravelAgencies.AddRange(travelAgency);
                    _dbContext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                },
            };
            return roles;
        }

        private IEnumerable<TravelAgency> GetTravelAgency()
        {
            var travelAgencies = new List<TravelAgency>()
        {
            new TravelAgency
            {
                Name = "Adventure Expeditions",
                Description = "Specializing in thrilling adventures around the globe.",
                ContactEmail = "info@adventureexpeditions.com",
                ContactNumber = "+1234567890",
                Address = new Address
                {
                    City = "Adventure City",
                    Street = "123 Adventure St",
                    PostalCode = "12345"
                },
                Offers = new List<Offer>
                {
                    new Offer
                    {
                        OfferName = "Jungle Trekking in Amazon Rainforest",
                        OfferDescription = "Explore the Amazon rainforest with experienced guides.",
                        Destination = "Amazon Rainforest",
                        Price = 1500.00f,
                        TransportationAirportHotel = true,
                        StartDate = DateTime.Now.AddDays(30),
                        EndDate = DateTime.Now.AddDays(37)
                    },
                    new Offer
                    {
                        OfferName = "Mountaineering Expedition to Mount Everest",
                        OfferDescription = "Conquer the world's highest peak with expert climbers.",
                        Destination = "Mount Everest",
                        Price = 5000.00f,
                        TransportationAirportHotel = true,
                        StartDate = DateTime.Now.AddDays(90),
                        EndDate = DateTime.Now.AddDays(120)
                    }
                }
            },
            new TravelAgency
            {
                Name = "Relaxation Retreats",
                Description = "Escape to tranquil destinations for relaxation and rejuvenation.",
                ContactEmail = "info@relaxationretreats.com",
                ContactNumber = "+1987654321",
                Address = new Address
                {
                    City = "Peaceful Town",
                    Street = "456 Serenity Blvd",
                    PostalCode = "54321"
                },
                Offers = new List<Offer>
                {
                    new Offer
                    {
                        OfferName = "Yoga and Meditation Retreat in Bali",
                        OfferDescription = "Find inner peace and balance in the serene surroundings of Bali.",
                        Destination = "Bali, Indonesia",
                        Price = 2000.00f,
                        TransportationAirportHotel = true,
                        StartDate = DateTime.Now.AddDays(60),
                        EndDate = DateTime.Now.AddDays(67)
                    },
                    new Offer
                    {
                        OfferName = "Spa Getaway in Maldives",
                        OfferDescription = "Indulge in luxurious spa treatments amidst the breathtaking beauty of Maldives.",
                        Destination = "Maldives",
                        Price = 3000.00f,
                        TransportationAirportHotel = true,
                        StartDate = DateTime.Now.AddDays(120),
                        EndDate = DateTime.Now.AddDays(127)
                    }
                }
            },
            new TravelAgency
            {
                Name = "Cultural Explorations",
                Description = "Discover the rich heritage and diverse cultures of the world.",
                ContactEmail = "info@culturalexplorations.com",
                ContactNumber = "+1122334455",
                Address = new Address
                {
                    City = "Heritage City",
                    Street = "789 Heritage Ave",
                    PostalCode = "67890"
                },
                Offers = new List<Offer>
                {
                    new Offer
                    {
                        OfferName = "Historical Tour of Rome",
                        OfferDescription = "Explore the ancient ruins and landmarks of Rome.",
                        Destination = "Rome, Italy",
                        Price = 2500.00f,
                        TransportationAirportHotel = true,
                        StartDate = DateTime.Now.AddDays(45),
                        EndDate = DateTime.Now.AddDays(52)
                    },
                    new Offer
                    {
                        OfferName = "Culinary Journey in Thailand",
                        OfferDescription = "Embark on a gastronomic adventure through the vibrant street food scene of Thailand.",
                        Destination = "Thailand",
                        Price = 1800.00f,
                        TransportationAirportHotel = true,
                        StartDate = DateTime.Now.AddDays(75),
                        EndDate = DateTime.Now.AddDays(82)
                    }
                }
            }
            };
            return travelAgencies;
        }
    }
}
