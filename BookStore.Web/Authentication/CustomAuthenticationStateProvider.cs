using System.Security.Claims;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Blazored.SessionStorage;
using BookStore.Model;
using BookStore.Web.Services.Users;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Web.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IUserService _userService;

        public CustomAuthenticationStateProvider(ILocalStorageService localStorageService, IUserService userService)
        {
            _userService = userService;
            _localStorageService = localStorageService;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var accessToken = await _localStorageService.GetItemAsync<string>("accessToken");
            ClaimsIdentity identity;
            if (!string.IsNullOrEmpty(accessToken))
            {
                var user = await _userService.GetUserByAccessTokenAsync(accessToken);
                identity = GetClaimsIdentity(user);
            }
            else
            {
                identity = new ClaimsIdentity();
            }
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return new AuthenticationState(claimsPrincipal);
        }

        public async Task MarkUserAsAuthenticatedAsync(User user)
        {
            var identity = GetClaimsIdentity(user);
            await _localStorageService.SetItemAsync("accessToken", ((UserWithToken)user).AccessToken);
            await _localStorageService.SetItemAsync("refreshToken", ((UserWithToken)user).RefreshToken);
            var claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorageService.RemoveItemAsync("accessToken");
            await _localStorageService.RemoveItemAsync("refreshToken");
            var identity = new ClaimsIdentity();
            var claimsPrincipal = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }

        private ClaimsIdentity GetClaimsIdentity(User user)
        {
            var claimsIdentity = new ClaimsIdentity();
            if (user != null && user.EmailAddress != null)
            {
                claimsIdentity = new ClaimsIdentity(new[]
                               {
                                    new Claim(ClaimTypes.Name, user.EmailAddress)
                               }, "apiauth_type");
            }
            return claimsIdentity;
        }
    }
}