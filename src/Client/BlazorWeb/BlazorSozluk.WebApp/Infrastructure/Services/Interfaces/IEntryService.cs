using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
namespace BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
public interface IEntryService
{
    Task<List<GetEntriesViewModel>> GetEntries();
    Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId);
    Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize);
    Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilPageEntries(int page, int pageSize,string userName=null);
    Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize);

    Task<Guid> CreateEntry(CreateEntryCommand command);
    Task<Guid> CreateEntryComment(CreateEntryCommentCommand command);
    Task<List<SearchEntryViewModel>> SearchBySubject(string searchText);

}
    

