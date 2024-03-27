using BiuroPodrozyAPI.Models;
using BiuroPodrozyAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BiuroPodrozyAPI.Controllers
{
    [Route("travelagency/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(Microsoft.AspNetCore.Identity.SignInManager<Entitties.User> @object, IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody]RegisterUserDto dto)
        {
            _accountService.RegisterUser(dto);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginDto dto)
        {
            string token = _accountService.GenerateJwt(dto);
            return Ok(token);
        }
    }
}
