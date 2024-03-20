using BiuroPodrozyAPI.Models;
using System.Collections.Generic;

namespace BiuroPodrozyAPI.Services
{
    public interface ITravelAgencyService
    {
        int Create(CreateTravelAgencyDto dto);
        IEnumerable<TravelAgencyDto> GetAll();
        TravelAgencyDto GetById(int id);
        void Delete(int id);
        void Update(int id, UpdateTravelAgencyDto dto);
        IEnumerable<TravelAgencyDto> GetTravelAgencyByCity(string city);
    }
}