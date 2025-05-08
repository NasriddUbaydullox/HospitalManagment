using HospitalManagment_V2.Mediator.Auth.SignIn;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalManagment_V2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator _mediator) : ControllerBase
    {
        [HttpPost("/sign-in")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequestDto request)
        {
            return Ok(await _mediator.Send(new SignInCommand(request)));
        }
    }
}
