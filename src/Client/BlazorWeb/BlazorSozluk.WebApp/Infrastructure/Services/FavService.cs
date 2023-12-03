using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class FavService : IFavService
    {
        private readonly HttpClient _client;
        public FavService(HttpClient client)
        {
            _client = client;
        }
        public async Task CreatedEntryFav(Guid entryId)
        {
            await _client.PostAsync($"/api/favorite/Entry/{entryId}", null);
        }

        public async Task CreatedEntryComemntFav(Guid entryCommentId)
        {
            await _client.PostAsync($"/api/favorite/EntryComment/{entryCommentId}", null);
        }

        public async Task DeleteEntryFav(Guid entryCommentId)
        {
            await _client.PostAsync($"/api/favorite/DeleteEntryfav/{entryCommentId}", null);
        }

        public async Task DeleteEntryCommentFav(Guid entryCommentId)
        {
            await _client.PostAsync($"/api/favorite/DeleteEntrCommentfav/{entryCommentId}", null);
        }

    }
}
