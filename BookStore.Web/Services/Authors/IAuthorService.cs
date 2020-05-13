using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Model;

namespace BookStore.Web.Data
{
    public interface IAuthorService
    {
        public DateTime GetCreationDate();
        public string GetVersion();
        Task<List<Author>> GetAuthors();
        Task<Author> GetAuthor(int id);
        Task<bool> SaveAuthor(Author author);
        Task<bool> DeleteAuthor(int id);
        Task<bool> CheckConnection();
    }
}