using BlazorSozluk.Api.Application.Features.Commands.Entry.CreateFav;
using BlazorSozluk.Api.Application.Features.Commands.Entry.DeleteFav;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.CreateFav;
using BlazorSozluk.Api.Application.Features.Commands.EntryComment.DeleteFav;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorSozluk.Api.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FavoriteController : BaseController
{
    private IMediator mediator;

    public FavoriteController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpPost]
    [Route("Entry/{entryId}")]
    public async Task<IActionResult> CreateEntryFav(Guid EntryId)
    {
        var result = await mediator.Send(new CreateEntryFavCommand(EntryId,UserId));


        return Ok(result);
    }

    [HttpPost]
    [Route("EntryComment/{EntryCommentId}")]
    public async Task<IActionResult> CreateEntryCommentFav(Guid EntryCommentId)
    {
        var result = await mediator.Send(new CreateEntryCommentFavCommand(EntryCommentId, UserId.Value));

        return Ok(result);
    }



    [HttpPost]
    [Route("DeleteEntryFav/{entryId}")]
    public async Task<IActionResult> DeleteEntryFav(Guid EntryId)
    {
        var result = await mediator.Send(new DeleteEntryFavCommand(EntryId, UserId.Value));


        return Ok(result);
    }

    [HttpPost]
    [Route("DeleteEntryCommentfav/{EntryCommentId}")]
    public async Task<IActionResult> DeleteEntryCommentFav(Guid EntryCommentId)
    {
        var result = await mediator.Send(new DeleteEntryCommentFavCommand(EntryCommentId, UserId.Value));


        return Ok(result);
    }


}
