using AutoMapper;
using BiuroPodrozyAPI.Authorization;
using BiuroPodrozyAPI.Entitties;
using BiuroPodrozyAPI.Exceptions;
using BiuroPodrozyAPI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BiuroPodrozyAPI.Services
{
    public class TravelAgencyService : ITravelAgencyService
    {
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TravelAgencyService> _logger;
        private readonly IAuthorizationService _authorizationService;

        public TravelAgencyService(TravelAgencyDbContext dbContext, IMapper mapper, ILogger<TravelAgencyService> logger, IAuthorizationService authenticationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authenticationService;
        }

        public void Delete(int id, ClaimsPrincipal user)
        {
            var travelAgencyById = _dbContext
               .TravelAgencies
               .FirstOrDefault(x => x.Id == id);

            if(travelAgencyById is null)
            {
                throw new NotFoundException("Travel agency not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(user, travelAgencyById,
                new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
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

        public int Create(CreateTravelAgencyDto dto, int userId)
        {
            var travelAgency = _mapper.Map<TravelAgency>(dto);
            travelAgency.CreatedById = userId;
            _dbContext.TravelAgencies.Add(travelAgency);
            _dbContext.SaveChanges();

            return travelAgency.Id;
        }

        public void Update(int id, UpdateTravelAgencyDto dto, ClaimsPrincipal user)
        {
            var travelAgency = _dbContext
              .TravelAgencies
              .FirstOrDefault(x => x.Id == id);

            if (travelAgency is null)
                throw new NotFoundException("Travel agency not found");


            var authorizationResult = _authorizationService.AuthorizeAsync(user, travelAgency, 
                new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if(!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            travelAgency.Name = dto.Name;
            travelAgency.Description = dto.Description;
            travelAgency.ContactEmail = dto.ContactEmail;
            travelAgency.ContactNumber = dto.ContactNumber;

            _dbContext.SaveChanges();
        }
    }
}
