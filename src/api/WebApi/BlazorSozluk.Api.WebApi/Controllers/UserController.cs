using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginUserCommand command)
        {
            var res = await _mediator.Send(command);

            return Ok(res); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommand command)
        {
            var guid = await _mediator.Send(command);

            return Ok(guid);
        }


        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> UpdateUser(UpdateUserCommand command)
        {
            var guid = await _mediator.Send(command);

            return Ok(guid);
        }
    }
}
