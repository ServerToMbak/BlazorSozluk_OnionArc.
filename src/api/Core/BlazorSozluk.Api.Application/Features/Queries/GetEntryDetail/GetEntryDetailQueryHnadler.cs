using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryDetail;

public class GetEntryDetailQueryHandler : IRequestHandler<GetEntryDetailQuery, GetEntryDetailViewModel>
{
    private IEntryRepository _entryRepository;
    public GetEntryDetailQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }
    public async Task<GetEntryDetailViewModel> Handle(GetEntryDetailQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query.Include(i => i.CreatedBy)
                     .Include(i => i.EntryFavorites)
                     .Include(i => i.EntryVotes)
                     .Where(i => i.CreatedById == request.UserId);

        var list = query.Select(i => new GetEntryDetailViewModel()
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(i => i.CreatedById == request.UserId),
            FavoriteCount = i.EntryFavorites.Count,
            CreatedDate = i.CreatedAt,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType =
                request.UserId.HasValue && i.EntryVotes.Any(i => i.CreatedById == request.UserId)
                ? i.EntryVotes.FirstOrDefault(i => i.CreatedById == request.UserId).VoteType
                : VoteType.None
        });

        return await list.FirstOrDefaultAsync(cancellationToken:cancellationToken);
    }
}
