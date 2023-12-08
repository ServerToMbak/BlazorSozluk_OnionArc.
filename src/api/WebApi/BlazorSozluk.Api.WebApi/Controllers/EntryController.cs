using BlazorSozluk.Api.Application.Features.Queries.GetEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;
using BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail;
using BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;
using BlazorSozluk.Api.Application.Features.Queries.GetUserEntries;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryController : BaseController
    {
        private readonly IMediator _mediator;
        public EntryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] GetEntriesQuery query)
        {
            var result =await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetEntryDetailQuery(id,UserId));

            return Ok(result);
        }

        [HttpGet]
        [Route("Comments/{id}")]
        public async Task<IActionResult> GetEntryComments(Guid id, int page, int pageSize)
        {
            var result = await _mediator.Send(new GetEntryCommentsQuery(id,UserId,page,pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("userEntries")]
        //[Authorize]
        public async Task<IActionResult> GetUserEntries(string userName,Guid userId, int page, int pageSize)
        {
            if (UserId == Guid.Empty && string.IsNullOrEmpty(userName))
                userId = UserId.Value;

            var result = await _mediator.Send(new GetUserEntriesQuery(userId, userName, page, pageSize));

            return Ok(result);
        }

        [HttpGet]
        [Route("MainPageEntries")]
        public async Task<IActionResult> GetMainPageEntries(int page, int pageSize)
        {
            var result = await _mediator.Send(new GetMainPageEntriesQuery(UserId, page,pageSize));

            return Ok(result);
        }

        [HttpPost]
        [Route("CreateEntry")]
        [Authorize]
        public async Task<IActionResult> CreateEntry(CreateEntryCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;
            var result = await _mediator.Send(command);

            return Ok(result);  
        }

        [HttpPost]
        [Route("CreateEntryComment")]
        [Authorize]
        public async Task<IActionResult> CreateEntry(CreateEntryCommentCommand command)
        {
            if (!command.CreatedById.HasValue)
                command.CreatedById = UserId;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Route("Search")]
        public async Task<IActionResult> Search([FromQuery] SearchEntryQuery query)
        {
            var result = await _mediator.Send(query);

            return Ok(result);
        }
    }
}
