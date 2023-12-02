using AutoMapper;
using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.ViewModels;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetMainPageEntries;

public class GetMainPageEntriesQueryHandler : IRequestHandler<GetMainPageEntriesQuery, PagedViewModel<GetEntryDetailViewModel>>
{
    public readonly IEntryRepository _entryRepository;
    private readonly IMapper _mapper;

    public GetMainPageEntriesQueryHandler(IEntryRepository entryRepository, IMapper mapper)
    {
        _entryRepository = entryRepository;
        _mapper = mapper;
    }
    public async Task<PagedViewModel<GetEntryDetailViewModel>> Handle(GetMainPageEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        query = query.Include(i => i.EntryFavorites)
                     .Include(i => i.CreatedBy)
                     .Include(i => i.EntryVotes);

        var list = query.Select(i => new GetEntryDetailViewModel
        {
            Id = i.Id,
            Subject = i.Subject,
            Content = i.Content,
            IsFavorited = request.UserId.HasValue && i.EntryFavorites.Any(j => j.CreatedById == request.UserId),
            FavoriteCount = i.EntryFavorites.Count(),
            CreatedDate = i.CreatedAt,
            CreatedByUserName = i.CreatedBy.UserName,
            VoteType =
                request.UserId.HasValue && i.EntryVotes.Any(j => j.CreatedById == request.UserId)
                        ?   i.EntryVotes.FirstOrDefault(j => j.CreatedById == request.UserId).VoteType 
                        :   VoteType.None

        });

        var entries = await list.GetPaged(request.Page, request.PageSize);

        return entries;
    }
}