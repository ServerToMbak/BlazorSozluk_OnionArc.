using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using MediatR;

namespace BlazorSozluk.Api.Application.Features.Queries.GetEntryComments;

public class GetEntryCommentsQuery : BasePagedQuery, IRequest<PagedViewModel<GetEntryCommentsViewModel>>
{
    public GetEntryCommentsQuery(Guid entryId, Guid? userId, int page, int pageSize ) : base(page, pageSize)
    {
        EntryId = entryId;
        UserId = userId;
    }
    public Guid EntryId { get; set; }
    public Guid? UserId { get; set; }
}
