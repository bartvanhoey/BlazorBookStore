using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Web.Authentication;
using BookStore.Web.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Web.Pages.Login
{
    public class SignupBase : ComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public IUserService UserService { get; set; }


        public User User { get; set; } = new User();
        public string LoginMessage { get; set; }
        public async Task RegisterUser()
        {
            User.Source = "APPC";

            var user = await UserService.RegisterUserAsync(User);

            if (user.EmailAddress != null)
            {
                await ((CustomAuthenticationStateProvider)AuthenticationStateProvider)
                    .MarkUserAsAuthenticatedAsync(user);
                NavigationManager.NavigateTo("/index");
            } else {
                LoginMessage = "Invalid username or password";
            }
        }

    }
}