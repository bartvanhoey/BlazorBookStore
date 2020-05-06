using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBookStore.Data.Publishers
{
    public class PublisherService : IPublisherService
    {

        public List<Publisher> Publishers { get; set; } = new List<Publisher>();
        public PublisherService()
        {
            Publishers.Add(new Publisher("0736", "New Moon Books", "Boston", "MA", "USA"));
            Publishers.Add(new Publisher("0877", "Binnet & Hardley", "Washington", "DC", "USA"));
            Publishers.Add(new Publisher("1389", "Algodata Infosystems", "Berkeley", "CA", "USA"));
            Publishers.Add(new Publisher("1622", "Five Lakes Publishing", "Chicago", "IL", "USA"));
            Publishers.Add(new Publisher("1756", "Ramona Publishers", "Dallas", "TX", "USA"));

        }
        public Task<bool> DeletePublisher(string id)
        {
            Publishers.Remove(Publishers.FirstOrDefault(x => x.Id == id));
            return Task.FromResult(true);
        }

        public async Task<Publisher> GetPublisher(string id)
        {
            return await Task.FromResult(Publishers.FirstOrDefault(p => p.Id == id));
        }

        public async Task<List<Publisher>> GetPublishers()
        {
            return await Task.FromResult(Publishers);
        }

        public Task<bool> SavePublisher(Publisher Publisher)
        {
            Publisher.Id = GeneratePublisherId();
            Publishers.Add(Publisher);
            return Task.FromResult(true);
        }

        private string GeneratePublisherId()
        {
            string id;
            Random random = new Random();
            id = random.Next(100, 10000).ToString().PadLeft(4, '0');

            return id;
        }
    }
}