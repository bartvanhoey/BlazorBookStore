using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BookStore.Model;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        // private readonly ISessionStorageService _sessionStorageService;
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
            // _sessionStorageService = sessionStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var emailAddress = await _localStorageService.GetItemAsync<string>("emailAddress");
            ClaimsIdentity identity;

            if (emailAddress == null)
            {
                identity = new ClaimsIdentity();
            }
            else
            {
                identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, emailAddress)}, "apiauth_type");
            }
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticatedAsync(User user)
        {
            await MarkUserAsAuthenticatedAsync(user.EmailAddress);
        }
        public async Task MarkUserAsAuthenticatedAsync(string emailAddress, string token = null)
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, emailAddress)}, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            // await _sessionStorageService.SetItemAsync("emailAddress", emailAddress);
            // await _sessionStorageService.SetItemAsync("token", token);

            await _localStorageService.SetItemAsync("emailAddress", emailAddress);
            await _localStorageService.SetItemAsync("accessToken", token);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("emailAddress");
            await _localStorageService.RemoveItemAsync("emailAddress");

            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }










    }
}