using AutoMapper;
using BiuroPodrozyAPI.Entitties;
using BiuroPodrozyAPI.Models;

namespace BiuroPodrozyAPI
{
    public class TravelAgencyMappingProfile : Profile
    {
        public TravelAgencyMappingProfile()
        {
            CreateMap<TravelAgency, TravelAgencyDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Address.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Address.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Address.PostalCode));

            CreateMap<Offer, OfferDto>();

            CreateMap<CreateTravelAgencyDto, TravelAgency>()
                .ForMember(t => t.Address, c => c.MapFrom(dto => new Address() 
                { City = dto.City, PostalCode = dto.PostalCode, Street = dto.Street }));

            CreateMap<CreateOfferDto, Offer>();
        }
    }
}
