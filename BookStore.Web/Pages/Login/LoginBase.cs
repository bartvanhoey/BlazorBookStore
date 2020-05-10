using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Web.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;

namespace BookStore.Web.Pages.Login
{
    public class LoginBase : ComponentBase
    {
        [Inject]
        public HttpClient HttpClient { get; set; }

        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public User User { get; set; } = new User();
        public string LoginMessage { get; set; }
        public async Task ValidateUser()
        {

            var serializedUser = JsonConvert.SerializeObject(User);
            HttpRequestMessage message = new HttpRequestMessage();
            message.Method = new HttpMethod("POST");
            message.RequestUri = new Uri("https://localhost:52317/api/users/login");
            message.Content = new StringContent(serializedUser);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await HttpClient.SendAsync(message);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var user = JsonConvert.DeserializeObject<UserWithToken>(responseBody);

                await ((CustomAuthenticationStateProvider)AuthenticationStateProvider)
                    .MarkUserAsAuthenticatedAsync(user.EmailAddress, user.AccessToken);
                NavigationManager.NavigateTo("/index");
            }
            else
            {
                LoginMessage = "Invalid Username or Password!";
            }

        }
    }
}