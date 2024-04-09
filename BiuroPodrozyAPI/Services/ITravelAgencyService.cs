using BiuroPodrozyAPI.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace BiuroPodrozyAPI.Services
{
    public interface ITravelAgencyService
    {
        int Create(CreateTravelAgencyDto dto, int userId);
        IEnumerable<TravelAgencyDto> GetAll();
        TravelAgencyDto GetById(int id);
        void Delete(int id, ClaimsPrincipal user);
        void Update(int id, UpdateTravelAgencyDto dto, ClaimsPrincipal user);
        IEnumerable<TravelAgencyDto> GetTravelAgencyByCity(string city);
    }
}