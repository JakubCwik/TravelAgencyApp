using BiuroPodrozyAPI.Models;
using BiuroPodrozyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BiuroPodrozyAPI.Controllers
{
    [Route("travelagency/{travelAgencyId}/offer")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly iOfferService _offerService;

        public OfferController(iOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int travelAgencyId)
        {
            _offerService.RemoveAll(travelAgencyId);

            return NoContent();
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int travelAgencyId, [FromBody] CreateOfferDto dto)
        {
            var newOfferId = _offerService.Create(travelAgencyId, dto);

            return Created($"travelagency/{travelAgencyId}/offer/{newOfferId}", null);
        }

        [HttpGet("{offerId}")]
        public ActionResult<OfferDto> Get([FromRoute] int travelAgencyId, [FromRoute] int offerId)
        {
            OfferDto offer = _offerService.GetById(travelAgencyId, offerId);
            return Ok(offer);
        }

        [HttpGet("sortedbyprice")]
        public ActionResult<List<OfferDto>> GetSortedByPrice([FromRoute] int travelAgencyId)
        {
            var result = _offerService.GetAllSortedByPrice(travelAgencyId);
            return Ok(result);

        }

        [HttpPost("bydestination")]
        public ActionResult<List<OfferDto>> GetByDestination([FromRoute] int travelAgencyId, [FromBody] OfferDto offerModel)
        {
            var result = _offerService.GetByDestination(travelAgencyId, offerModel.Destination);
            return Ok(result);
        }

        [HttpGet]
        public ActionResult<List<OfferDto>> Get([FromRoute] int travelAgencyId)
        {
            var result = _offerService.GetAll(travelAgencyId);
            return Ok(result);
        }
    }
}
