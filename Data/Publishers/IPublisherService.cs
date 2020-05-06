using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBookStore.Data.Publishers
{
    public interface IPublisherService
    {
        Task<List<Publisher>> GetPublishers();
        Task<Publisher> GetPublisher(string id);
        Task<bool> SavePublisher(Publisher Publisher);
        Task<bool> DeletePublisher(string id);
    }
}