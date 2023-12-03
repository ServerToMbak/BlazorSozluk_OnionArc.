using Blazored.LocalStorage;
using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using BlazorSozluk.Common.Models.Queries;
using BlazorSozluk.Common.Models.RequestModels;
using BlazorSozluk.WebApp.Infrastructure.Extensions;
using BlazorSozluk.WebApp.Infrastructure.Services.Interfaces;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace BlazorSozluk.WebApp.Infrastructure.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _client;
        private readonly ISyncLocalStorageService _syncLocalStorageService;

        public IdentityService(HttpClient client, ISyncLocalStorageService syncLocalStorage)
        {
            _client = client;
            _syncLocalStorageService = syncLocalStorage;
        }


        public bool IsLoggedIn => !string.IsNullOrEmpty(GetUserToken());


        private string GetUserToken()
        {
            return _syncLocalStorageService.GetToken();
        }

        private string GetUserName()
        {
            return _syncLocalStorageService.GetToken();
        }

        private Guid GetUserId()
        {
            return _syncLocalStorageService.GetUserId();
        }
        public async Task<bool> Login(LoginUserCommand command)
        {
            string responseStr;
            var httpResponse = await _client.PostAsJsonAsync("/api/User/Login", command);

            if (httpResponse != null && !httpResponse.IsSuccessStatusCode)
            {
                if (httpResponse.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseStr = await httpResponse.Content.ReadAsStringAsync();
                    var validation = JsonSerializer.Deserialize<ValidationResponseModel>(responseStr);
                    responseStr = validation.FlattenErrors;
                    throw new DatabaseValidationException(responseStr);
                }
                return false;
            }


            responseStr = await httpResponse.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<LoginUserViewModel>(responseStr);

            if (!string.IsNullOrEmpty(response.Token))
            {
                _syncLocalStorageService.SetToken(response.Token);
                _syncLocalStorageService.SetUserId(response.Id);
                _syncLocalStorageService.SetUserName(response.UserName);


                //TODO Check after auth
                //((authStateProvider)autStateProvider).NotifyUserLogin(response.UserName, response.Id);

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", response.UserName);

                return true;
            }
            return false;
        }

        public void LogOut()
        {
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.TokenName);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserName);
            _syncLocalStorageService.RemoveItem(LocalStorageExtension.UserId);

            //TODO Check after with auth
            //((authStateProvider)authStateProvider).NotifyUserLogout();

            _client.DefaultRequestHeaders.Authorization = null;

        }

    }
}
