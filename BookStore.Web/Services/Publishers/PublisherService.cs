using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using BookStore.Model;
using Microsoft.AspNetCore.Components;
using Publisher = BookStore.Model.Publisher;

namespace BookStore.Web.Data.Publishers
{
    public class PublisherService : IPublisherService
    {
        private readonly HttpClient _httpClient;

        public PublisherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public List<Publisher> Publishers { get; set; } = new List<Publisher>();
      

        public Task<bool> DeletePublisher(int id)
        {
            Publishers.Remove(Publishers.FirstOrDefault(x => x.PubId == id));
            return Task.FromResult(true);
        }

        public async Task<Publisher> GetPublisher(int id)
        {
            return await Task.FromResult(Publishers.FirstOrDefault(p => p.PubId == id));
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await _httpClient.GetJsonAsync<List<Publisher>>("api/publishers");
        }

        public Task<bool> SavePublisher(Publisher Publisher)
        {
            Publisher.PubId = GeneratePublisherId();
            Publishers.Add(Publisher);
            return Task.FromResult(true);
        }

        private int GeneratePublisherId()
        {
            int id;
            Random random = new Random();
            id = random.Next(100, 10000);

            return id;
        }
    }
}