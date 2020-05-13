using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookStore.Model;
using Newtonsoft.Json;

namespace BookStore.Web.Services.Users
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<User> LoginAsync(User user)
        {
            var serializedUser = JsonConvert.SerializeObject(user);
            
            var message = new HttpRequestMessage(HttpMethod.Post, "api/users/login");
            message.Content = new StringContent(serializedUser);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(message);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var deserializedUser = JsonConvert.DeserializeObject<UserWithToken>(responseBody);
                return deserializedUser;
            }
            else
            {
                return null;
            }
        }

     
    }
}