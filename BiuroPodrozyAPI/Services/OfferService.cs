using AutoMapper;
using BiuroPodrozyAPI.Entitties;
using BiuroPodrozyAPI.Exceptions;
using BiuroPodrozyAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace BiuroPodrozyAPI.Services
{
    public interface iOfferService
    {
        int Create(int travelAgencyId, CreateOfferDto dto);
        OfferDto GetById(int travelAgencyId, int offerId);
        List<OfferDto> GetAll(int travelAgencyId);
        List<OfferDto> GetAllSortedByPrice(int travelAgencyId);
        List<OfferDto> GetByDestination(int travelAgencyId, string destination);
        void RemoveAll(int travelAgencyId);
    }


    public class OfferService : iOfferService
    {
        private readonly TravelAgencyDbContext _context;
        private readonly IMapper _mapper;

        public OfferService(TravelAgencyDbContext context, IMapper mapper)
        {
            _context = context;

            _mapper = mapper;
        }
        public int Create(int travelAgencyId, CreateOfferDto dto)
        {
            var travelAgency = GetTravelAgencyById(travelAgencyId);

            var offerEntity = _mapper.Map<Offer>(dto);

            offerEntity.TravelAgencyId = travelAgencyId;

            _context.Offers.Add(offerEntity);
            _context.SaveChanges();

            return offerEntity.Id;
        }

        public OfferDto GetById(int travelAgencyId, int offerId)
        {
            var travelAgency = GetTravelAgencyById(travelAgencyId);

            var offer = _context.Offers.FirstOrDefault(o => o.Id == offerId);
            if(offer is null || offer.TravelAgencyId != travelAgencyId)
            {
                throw new NotFoundException("Offer not found");
            }

            var offerDto = _mapper.Map<OfferDto>(offer);

            return offerDto;
        }

        public List<OfferDto> GetAll(int travelAgencyId)
        {
            var travelAgency = GetTravelAgencyById(travelAgencyId);
            var offerDtos = _mapper.Map<List<OfferDto>>(travelAgency.Offers);

            return offerDtos;
        }

        public List<OfferDto> GetAllSortedByPrice(int travelAgencyId)
        {
            var travelAgency = GetTravelAgencyById(travelAgencyId);
            var offerDtos = _mapper.Map<List<OfferDto>>(travelAgency.Offers.OrderBy(o => o.Price));
            return offerDtos;
        }

        public List<OfferDto> GetByDestination(int travelAgencyId, string destination)
        {
            var travelAgency = GetTravelAgencyById(travelAgencyId);
            var offerDtos = _mapper.Map<List<OfferDto>>(travelAgency.Offers.Where(o => o.Destination == destination));
            return offerDtos;
        }

        public void RemoveAll(int  travelAgencyId)
        {
            var travelAgency = GetTravelAgencyById(travelAgencyId);

            _context.RemoveRange(travelAgency.Offers);
            _context.SaveChanges();
        }

        private TravelAgency GetTravelAgencyById(int travelAgencyId)
        {
            var travelAgency = _context.TravelAgencies
                .Include(o => o.Offers)
                .FirstOrDefault(t => t.Id == travelAgencyId);

            if (travelAgency is null)
            {
                throw new NotFoundException("Travel Agency not found");
            }

            return travelAgency;
        }
    }
}
