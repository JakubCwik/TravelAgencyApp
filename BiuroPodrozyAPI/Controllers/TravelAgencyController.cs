using AutoMapper;
using BiuroPodrozyAPI.Entitties;
using BiuroPodrozyAPI.Models;
using BiuroPodrozyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BiuroPodrozyAPI.Controllers
{
    [Route("travelagency")]
    [ApiController]
    public class TravelAgencyController :  ControllerBase
    {
        private readonly ITravelAgencyService _travelAgencyService;
        private readonly TravelAgencyDbContext _dbContext;
        private readonly IMapper _mapper;

        public TravelAgencyController(ITravelAgencyService travelAgencyService)
        {
            _travelAgencyService = travelAgencyService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
             _travelAgencyService.Delete(id);

            return NoContent();
        }

        [HttpPost]
        public ActionResult CreateTravelAgency([FromBody]CreateTravelAgencyDto dto)
        {
            var id = _travelAgencyService.Create(dto);

            return Created($"/travelagency/{id}", null);
        }

        [HttpGet]
        public ActionResult<IEnumerable<TravelAgencyDto>> GetAll()
        {
            var travelAgencyDtos = _travelAgencyService.GetAll();

            return Ok(travelAgencyDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<TravelAgencyDto> GetTravelAgencyById([FromRoute] int id)
        {
            var travelAgency = _travelAgencyService.GetById(id);

            return Ok(travelAgency);
        }

        [HttpGet("city/{city}")]
        public ActionResult<IEnumerable<TravelAgencyDto>> GetFilterByCity([FromRoute] string city)
        {
            var travelAgencyDtos = _travelAgencyService.GetTravelAgencyByCity(city);
            return Ok(travelAgencyDtos);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UpdateTravelAgencyDto dto, [FromRoute]int id)
        {
            _travelAgencyService.Update(id, dto);

            return Ok();
        }

    }
}
