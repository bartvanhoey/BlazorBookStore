using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore.Web.Data.Publishers
{
    public class PublisherService : IPublisherService
    {

        public List<Model.Publisher> Publishers { get; set; } = new List<Model.Publisher>();
        public PublisherService()
        {
            // Publishers.Add(new Publisher("0736", "New Moon Books", "Boston", "MA", "USA"));
            // Publishers.Add(new Publisher("0877", "Binnet & Hardley", "Washington", "DC", "USA"));
            // Publishers.Add(new Publisher("1389", "Algodata Infosystems", "Berkeley", "CA", "USA"));
            // Publishers.Add(new Publisher("1622", "Five Lakes Publishing", "Chicago", "IL", "USA"));
            // Publishers.Add(new Publisher("1756", "Ramona Publishers", "Dallas", "TX", "USA"));

        }
        public Task<bool> DeletePublisher(int id)
        {
            Publishers.Remove(Publishers.FirstOrDefault(x => x.PubId == id));
            return Task.FromResult(true);
        }

        public async Task<Model.Publisher> GetPublisher(int id)
        {
            return await Task.FromResult(Publishers.FirstOrDefault(p => p.PubId == id));
        }

        public async Task<List<Model.Publisher>> GetPublishers()
        {
            return await Task.FromResult(Publishers);
        }

        public Task<bool> SavePublisher(Model.Publisher Publisher)
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