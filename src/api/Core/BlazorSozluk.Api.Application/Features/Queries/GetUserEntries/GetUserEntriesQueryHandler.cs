using BlazorSozluk.Api.Application.Interfaces.Repositories;
using BlazorSozluk.Common.Infrastructure.Extensions;
using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BlazorSozluk.Api.Application.Features.Queries.GetUserEntries;

public class GetUserEntriesQueryHandler : IRequestHandler<GetUserEntriesQuery, PagedViewModel<GetUserEntriesDetailViewModel>>
{
    private readonly IEntryRepository _entryRepository;

    public GetUserEntriesQueryHandler(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository; 
    }
    public async Task<PagedViewModel<GetUserEntriesDetailViewModel>> Handle(GetUserEntriesQuery request, CancellationToken cancellationToken)
    {
        var query = _entryRepository.AsQueryable();

        if(request.UserId != null && request.UserId.HasValue && request.UserId != Guid.Empty)
        {
            query = query.Where(i => i.CreatedById == request.UserId);
        }
        else if(!string.IsNullOrEmpty(request.UserName))
        {
            query = query.Where(i => i.CreatedBy.UserName == request.UserName);
        }
        else 
            return null;

        query.Include(i => i.EntryFavorites)
             .Include(i => i.CreatedBy);

        var list = query.Select(i => new GetUserEntriesDetailViewModel
        {
            Id = i.Id,
            Content = i.Content,
            Subject = i.Subject,
            IsFavorited = false,
            CreatedDate = i.CreatedAt,
            FavoriteCount = i.EntryFavorites.Count,
            CreatedByUserName = i.CreatedBy.UserName

        });
        var entries = await list.GetPaged(request.Page, request.PageSize);
        return entries;


    }
}
