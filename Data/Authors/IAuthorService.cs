using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorBookStore.Data
{
    public interface IAuthorService
    {
        public DateTime GetCreationDate();
        public string GetVersion();
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthor(string id);
        Task<bool> SaveAuthor(Author author);
        Task<bool> DeleteAuthor(string id);
        Task<bool> CheckConnection();
    }
}