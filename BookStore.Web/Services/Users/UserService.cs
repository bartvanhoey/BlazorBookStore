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
            // var serializedObject = JsonConvert.SerializeObject(user);

            // var message = new HttpRequestMessage(HttpMethod.Post, "users/login");
            // message.Content = new StringContent(serializedObject);
            // message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            // var response = await _httpClient.SendAsync(message);

            // if (response.StatusCode == HttpStatusCode.OK)
            // {
            //     var responseBody = await response.Content.ReadAsStringAsync();
            //     var userWithToken = JsonConvert.DeserializeObject<UserWithToken>(responseBody);
            //     return userWithToken;
            // }
            // else
            // {
            //     return null;
            // }

            return await GetGenericAsync<UserWithToken>("users/login", "application/json", HttpMethod.Post, user);
        }



        public async Task<T> GetGenericAsync<T>(string apiUrl, string MediaTypeHeaderValue, HttpMethod httpMethod, User user)
        {
            var serializedObject = JsonConvert.SerializeObject(user);

            var message = new HttpRequestMessage(HttpMethod.Post, apiUrl);
            message.Content = new StringContent(serializedObject);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeHeaderValue);

            var response = await _httpClient.SendAsync(message);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var userWithToken = JsonConvert.DeserializeObject<T>(responseBody);
                return userWithToken;
            }
            else
            {
                return default(T);
            }
        }



        public async Task<User> RefreshAccessTokenAsync(RefreshRequest refreshRequest)
        {
            var serializedRefreshRequest = JsonConvert.SerializeObject(refreshRequest);

            var message = new HttpRequestMessage(HttpMethod.Post, "users/refreshaccesstoken");
            message.Content = new StringContent(serializedRefreshRequest);
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(message);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                var userWithToken = JsonConvert.DeserializeObject<UserWithToken>(responseBody);
                return userWithToken;
            }
            else
            {
                return null;
            }
        }


        public async Task<User> RegisterUserAsync(User user)
        {
            user.Password = Utility.Encrypt(user.Password);
            string serializedUser = JsonConvert.SerializeObject(user);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "users/registeruser");
            requestMessage.Content = new StringContent(serializedUser);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }

        public async Task<User> GetUserByAccessTokenAsync(string accessToken)
        {
            string serializedAccessToken = JsonConvert.SerializeObject(accessToken);

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "Users/GetUserByAccessToken");
            requestMessage.Content = new StringContent(serializedAccessToken);

            requestMessage.Content.Headers.ContentType
                = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;
            var responseBody = await response.Content.ReadAsStringAsync();

            var returnedUser = JsonConvert.DeserializeObject<User>(responseBody);

            return await Task.FromResult(returnedUser);
        }


    }
}