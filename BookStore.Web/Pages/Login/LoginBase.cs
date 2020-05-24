using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Web.Authentication;
using BookStore.Web.Services.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BookStore.Web.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IUserService UserService { get; set; }
        public User User { get; set; } = new User { EmailAddress = "philip.cramer@gmail.com", Password = "33a7ce1b2f70fcbe4f7c2730d9885aab" };
        public string LoginMessage { get; set; }
        public async Task ValidateUser()
        {
            var user = await UserService.LoginAsync(User);

            if (user != null)
            {
                await ((CustomAuthenticationStateProvider)AuthenticationStateProvider)
                    .MarkUserAsAuthenticatedAsync(user);
                NavigationManager.NavigateTo("/index");
            }
            else
            {
                LoginMessage = "Invalid Username or Password!";
            }

        }
    }
}