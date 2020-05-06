using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorBookStore.Data
{
    public class AuthorService : IAuthorService
    {
        public List<Author> Authors { get; set; } = new List<Author>();
        public DateTime CreationDate { get; set; }

        public AuthorService()
        {

            CreationDate = DateTime.Now;
            Authors = new List<Author>();

            Authors.Add(new Author("172-32-1176", "Johnson", "White", "406 496 7223", "johnsonwhite@gmail.com", 11000, "Menlo Park"));
            Authors.Add(new Author("213-46-8915", "Marjorie", "Green", "415 986 7020", "marjoriegreen@gmail.com", 32000, "Oakland"));
            Authors.Add(new Author("238-95-7766", "Cheryl", "Carson", "415-548-7723", "cherylcarson@gmail.com", 39000, "Menlo Park"));
            Authors.Add(new Author("267-41-2394", "Michael", "O'Leary", "406 286 2428", "michaeloleary@gmail.com", 31000, "San José"));
            Authors.Add(new Author("274-80-9391", "Dean", "Straight", "415 834 2919", "deanstraight@gmail.com", 29000, "Oakland"));

        }
        public Task<Author> GetAuthor(string id)
        {
            return Task.FromResult(Authors.FirstOrDefault(x => x.Id == id));
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await Task.FromResult(Authors);
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
            author.Id = GenerateAuthorId();
            Authors.Add(author);
            return await Task.FromResult(true);
        }
        public async Task<bool> DeleteAuthor(string id)
        {
            return await Task.FromResult(true);
        }

        private string GenerateAuthorId()
        {
            string id;
            Random random = new Random();
            id = random.Next(100, 1000).ToString() + "-";
            id += random.Next(10, 100).ToString() + "-";
            id += random.Next(1000, 10000).ToString();
            return id;
        }

        public Task<bool> CheckConnection()
        {
            return Task.FromResult(false);
        }
    }
}