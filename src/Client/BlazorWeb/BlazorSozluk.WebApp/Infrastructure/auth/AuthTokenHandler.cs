using Blazored.LocalStorage;
using BlazorSozluk.WebApp.Infrastructure.Extensions;
using System.Net.Http.Headers;

namespace BlazorSozluk.WebApp.Infrastructure.auth
{
    public class AuthTokenHandler : DelegatingHandler
    {
        private readonly ISyncLocalStorageService syncLocalStorageService;

        public AuthTokenHandler(ISyncLocalStorageService syncLocalStorageService)
        {
            this.syncLocalStorageService = syncLocalStorageService;
        }
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = syncLocalStorageService.GetToken();

            if(!string.IsNullOrEmpty(token) && request.Headers.Authorization == null)
                request.Headers.Authorization = new AuthenticationHeaderValue("bearer",token); 
            return base.SendAsync(request,cancellationToken);
        }
    }
}
