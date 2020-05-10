using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Web.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Web.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public User User { get; set; } = new User();
        public string LoginMessage { get; set; }
        public async Task ValidateUser()
        {
            await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsAuthenticatedAsync(User.EmailAddress);
            NavigationManager.NavigateTo("/");
        }
    }
}