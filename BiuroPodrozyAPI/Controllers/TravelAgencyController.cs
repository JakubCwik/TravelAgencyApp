using AutoMapper;
using BiuroPodrozyAPI.Entitties;
using BiuroPodrozyAPI.Models;
using BiuroPodrozyAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace BiuroPodrozyAPI.Controllers
{
    [Route("travelagency")]
    [ApiController]
   // [Authorize]
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
             _travelAgencyService.Delete(id, User);

            return NoContent();
        }

        [HttpPost]
        //[Authorize(Roles = "Admin,Manager")]
        public ActionResult CreateTravelAgency([FromBody]CreateTravelAgencyDto dto)
        {
            var userId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _travelAgencyService.Create(dto, userId);

            return Created($"/travelagency/{id}", null);
        }

        [HttpGet]
        //[Authorize(Policy = "Atleast18")]
        public ActionResult<IEnumerable<TravelAgencyDto>> GetAll()
        {
            var travelAgencyDtos = _travelAgencyService.GetAll();

            return Ok(travelAgencyDtos);
        }

        [HttpGet("{id}")]
        //[AllowAnonymous]
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
            _travelAgencyService.Update(id, dto, User);

            return Ok();
        }

    }
}
