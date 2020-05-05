using System.Threading.Tasks;
using BlazorBookStore.Data;
using Microsoft.AspNetCore.Components;

namespace BlazorBookStore.Pages
{
    public class AuthorDetailsBase : ComponentBase
    {
        [Parameter]
        public string Id { get; set; }

        public Author Author { get; set; } = new Author();
        [Inject]
        public IAuthorService AuthorService { get; set; }


        protected override async Task OnInitializedAsync()
        {
            Author = await AuthorService.GetAuthor(Id);
        }
    }
}