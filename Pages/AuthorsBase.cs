using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBookStore.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorBookStore.Pages
{
    public class AuthorsBase : ComponentBase
    {
         [Inject]
        public IAuthorService AuthorService { get; set; }    
         public List<Author> Authors { get; set; } = new List<Author>();

        public Author Author { get; set; } = new Author();
       
        protected override async Task OnInitializedAsync()  {
                await LoadAuthors();
        }

        public async Task SaveAUthorAsync(){
           await AuthorService.SaveAuthor(Author);
           await LoadAuthors();
            Author = new Author();
        }

        private async Task LoadAuthors(){
            Authors = await AuthorService.GetAuthors();
            StateHasChanged();

        }
    }
}