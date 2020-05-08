using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.Model;
using Microsoft.AspNetCore.Components;

namespace BookStore.Web.Data
{
    public class AuthorService : IAuthorService
    {
        private readonly HttpClient _httpClient;

        public List<Author> Authors { get; set; } = new List<Author>();
        public DateTime CreationDate { get; set; } = DateTime.Now;

        public AuthorService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Author> GetAuthor(int id)
        {
            return await _httpClient.GetJsonAsync<Author>($"api/authors/{id}");
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _httpClient.GetJsonAsync<List<Author>>("api/authors");
        }

        public string GetVersion()
        {
            return "v1";
        }

        public DateTime GetCreationDate()
        {
            return CreationDate;
        }

        public async Task<bool> SaveAuthor(Author author)
        {
            if (author.AuthorId == 0)
            {
                await _httpClient.PostJsonAsync("api/authors", author);
            }
            else
            {
                await _httpClient.PutJsonAsync($"api/authors/{author.AuthorId}", author);
            }

            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteAuthor(int id)
        {
            var responseMessage = await _httpClient.DeleteAsync($"api/authors/{id}");
            return await Task.FromResult(responseMessage.IsSuccessStatusCode);
        }

        private int GenerateAuthorId()
        {
            int id;
            Random random = new Random();
            id = random.Next(100, 100000);
            return id;
        }

        public Task<bool> CheckConnection()
        {
            return Task.FromResult(false);
        }
    }
}