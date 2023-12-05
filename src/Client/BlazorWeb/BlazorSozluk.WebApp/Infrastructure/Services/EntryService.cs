using BlazorSozluk.Common.Models.Page;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services;

public class EntryService : IEntryService
{
    private readonly HttpClient _client;

    public EntryService(HttpClient client)
    {
        _client = client;   
    }
    public async Task<Guid> CreateEntry(CreateEntryCommand command)
    {
        var result =await _client.PostAsJsonAsync("/api/entry/CreteEntry", command);

        if(!result.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await result.Content.ReadAsStringAsync();


        return new Guid(guidStr.Trim('"'));


    }

    public async Task<Guid> CreateEntryComment(CreateEntryCommentCommand command)
    {
        var res = await _client.PostAsJsonAsync("/api/entry/CreteEntryComment", command);

        if (!res.IsSuccessStatusCode)
            return Guid.Empty;

        var guidStr = await res.Content.ReadAsStringAsync();


        return new Guid(guidStr.Trim('"'));

    }

    public async Task<List<GetEntriesViewModel>> GetEntries()
    {
        var result = await _client.GetFromJsonAsync<List<GetEntriesViewModel>>("/api/entry?todaysEntries=false&count=30");
        
        return result;
    }

    public async Task<PagedViewModel<GetEntryCommentsViewModel>> GetEntryComments(Guid entryId, int page, int pageSize)
    {
        var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryCommentsViewModel>>($"/api/entry/comments/{entryId}?page&pageSize={pageSize}");

        return result;
    }

    public async Task<GetEntryDetailViewModel> GetEntryDetail(Guid entryId)
    { 
        var result = await _client.GetFromJsonAsync<GetEntryDetailViewModel>($"/api/entry/{entryId}");

        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetMainPageEntries(int page, int pageSize)
    {
        var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/mainpageentries?page={page}&pagesize={pageSize}");

        return result;
    }

    public async Task<PagedViewModel<GetEntryDetailViewModel>> GetProfilPageEntries(int page, int pageSize, string userName = null)
    {
        var result = await _client.GetFromJsonAsync<PagedViewModel<GetEntryDetailViewModel>>($"/api/entry/UserEntries?userName={userName}&page={page}&pageSize&{pageSize}");
        
        return result;
    }

    public async Task<List<SearchEntryViewModel>> SearchBySubject(string searchText)
        //https://localhost:5001/api/Entry/Search?SearchText=a
    {
        var result = await _client.GetFromJsonAsync<List<SearchEntryViewModel>>($"/api/Entry/Search?SearchText={searchText}");

        return result;
    }

}
