using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorageService;

        public CustomAuthenticationStateProvider(ISessionStorageService sessionStorageService)
        {
            _sessionStorageService = sessionStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var emailAddress = await _sessionStorageService.GetItemAsync<string>("emailAddress");
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

         public async Task MarkUserAsAuthenticatedAsync(string emailAddress)
        {
            var identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, emailAddress)}, "apiauth_type");

            var user = new ClaimsPrincipal(identity);

            await _sessionStorageService.SetItemAsync("emailAddress", emailAddress);

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _sessionStorageService.RemoveItemAsync("emailAddress");
            
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }










    }
}