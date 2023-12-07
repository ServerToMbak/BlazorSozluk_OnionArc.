using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQueryHandler : IRequestHandler<GetEntryCommentsQuery, PagedViewModel<GetEntryCommentsViewModel>>
{
    private readonly IEntryCommentRepository _entryCommentRepository;
    public GetEntryCommentsQueryHandler(IEntryCommentRepository entryCommentRepository)
    {
        _entryCommentRepository = entryCommentRepository;
    }
    public async Task<PagedViewModel<GetEntryCommentsViewModel>> Handle(GetEntryCommentsQuery request, CancellationToken cancellationToken)
    {
        var query = _entryCommentRepository.AsQueryable();

        query = query.Include(i => i.EntryCommentFavorites)
                     .Include(i => i.CreatedBy)
                     .Include(i => i.EntryCommentVotes)
                     .Where(i => i.EntryId == request.EntryId);


        var list = query.Select(i => new GetEntryCommentsViewModel
        {
            Id = i.Id,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryCommentFavorites.Any(j => j.CreatedById == request.UserId),
            FavoriteCount = i.EntryCommentFavorites.Count,
            CreatedDate = i.CreatedAt,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType = 
            request.UserId.HasValue && i.EntryCommentVotes.Any(j => j.CreatedById == request.UserId)
            ? i.EntryCommentVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType
            : VoteType.None

        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}
