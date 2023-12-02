using BlazorSozluk.Api.Application.Features.Commands.User.ConfirmEmail;
using BlazorSozluk.Api.Application.Features.Queries.GetUserDetail;
using BlazorSozluk.Common.Events.User;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(id));
            return Ok(user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserName(string userName)
        {
            var user = await _mediator.Send(new GetUserDetailQuery(Guid.Empty,userName));

            return Ok(user);
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


        [HttpPost]
        [Route("Confirm")]
        public async Task<IActionResult> ConfirmEmail(Guid id)
        {
            var guid = await _mediator.Send(new ConfirmEmailCommand() { ConfirmationId=id});

            return Ok(guid);
        }



        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            if (!command.UserId.HasValue)
                command.UserId = UserId;
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
