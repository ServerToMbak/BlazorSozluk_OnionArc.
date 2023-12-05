using Blazored.LocalStorage;
using BlazorSozluk.WebApp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlazorSozluk.WebApp.Infrastructure.auth;

public class AuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService localStorageService;
    private readonly AuthenticationState anonymous;

    public AuthStateProvider(ILocalStorageService localStorageService)
    {
        this.localStorageService = localStorageService;
        anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var apiToken = await localStorageService.GetToken();

        if (string.IsNullOrEmpty(apiToken))
            return anonymous;

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.ReadJwtToken(apiToken);

        var cp = new ClaimsPrincipal(new ClaimsIdentity(securityToken.Claims,"jwtAuthType"));

        return new AuthenticationState(cp);

    }

    public void NotifyUserLogin(string userName,Guid id)
    {
        var cp = new ClaimsPrincipal(new ClaimsIdentity(new[] 
        {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, id.ToString())
        }));

        var authState = Task.FromResult(new AuthenticationState(cp));

        NotifyAuthenticationStateChanged(authState);
    }

    public void NotifyUserLogout()
    {
        var authState = Task.FromResult(anonymous);

        NotifyAuthenticationStateChanged(authState);    
    }
}
