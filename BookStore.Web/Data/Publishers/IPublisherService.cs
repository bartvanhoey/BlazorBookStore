using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore.Web.Data.Publishers
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetPublishers();
        Task<Publisher> GetPublisher(int id);
        Task<bool> SavePublisher(Publisher Publisher);
        Task<bool> DeletePublisher(int id);
    }
}