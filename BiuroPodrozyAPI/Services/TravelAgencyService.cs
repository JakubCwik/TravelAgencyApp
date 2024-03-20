using AutoMapper;
using BiuroPodrozyAPI.Entitties;
using BiuroPodrozyAPI.Exceptions;
using BiuroPodrozyAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace BiuroPodrozyAPI.Services
{
    public class TravelAgencyService : ITravelAgencyService
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TravelAgencyService> _logger;

        public TravelAgencyService(TravelAgencyDbContext dbContext, IMapper mapper, ILogger<TravelAgencyService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public void Delete(int id)
        {
            var travelAgencyById = _dbContext
               .TravelAgencies
               .FirstOrDefault(x => x.Id == id);

            if(travelAgencyById is null)
            {
                throw new NotFoundException("Travel agency not found");
            }

            _dbContext.TravelAgencies.Remove(travelAgencyById);
            _dbContext.SaveChanges();
        }

        public TravelAgencyDto GetById(int id)
        {
            var travelAgencyById = _dbContext
               .TravelAgencies
               .Include(t => t.Address)
               .Include(t => t.Offers)
               .FirstOrDefault(x => x.Id == id);

            if (travelAgencyById is null)
            {
                throw new NotFoundException("Travel agency not found");
            }

            var travelAgencyDto = _mapper.Map<TravelAgencyDto>(travelAgencyById);
            return travelAgencyDto;
        }

        public IEnumerable<TravelAgencyDto> GetTravelAgencyByCity(string city)
        {
            var travelAgency = _dbContext
               .TravelAgencies
               .Include(t => t.Address)
               .Include(t => t.Offers)
               .Where(a => a.Address.City == city)
               .ToList();

            if(travelAgency is null)
            {
                throw new NotFoundException("Travel agency not found");
            }

            var travelAgencyDtos = _mapper.Map<List<TravelAgencyDto>>(travelAgency);
            return travelAgencyDtos;
        }

        public IEnumerable<TravelAgencyDto> GetAll()
        {
            var travelAgency = _dbContext
               .TravelAgencies
               .Include(t => t.Address)
               .Include(t => t.Offers)
               .ToList();

            var travelAgencyDtos = _mapper.Map<List<TravelAgencyDto>>(travelAgency);

            return travelAgencyDtos;
        }

        public int Create(CreateTravelAgencyDto dto)
        {
            var travelAgency = _mapper.Map<TravelAgency>(dto);
            _dbContext.TravelAgencies.Add(travelAgency);
            _dbContext.SaveChanges();

            return travelAgency.Id;
        }

        public void Update(int id, UpdateTravelAgencyDto dto)
        {
            var travelAgency = _dbContext
              .TravelAgencies
              .FirstOrDefault(x => x.Id == id);

            if (travelAgency is null)
                throw new NotFoundException("Travel agency not found");

            travelAgency.Name = dto.Name;
            travelAgency.Description = dto.Description;
            travelAgency.ContactEmail = dto.ContactEmail;
            travelAgency.ContactNumber = dto.ContactNumber;

            _dbContext.SaveChanges();
        }
    }
}
