using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBookStore.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorBookStore.Pages
{
    public class AuthorsBase : ComponentBase
    {
        [Inject]
        public IAuthorService AuthorService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();

        public Author Author { get; set; } = new Author();
        protected ElementReference firstNameRef;
        public string[] Cities { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await LoadAuthors();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && Cities == null)
            {
                Cities = await JSRuntime.InvokeAsync<string[]>("getCities");
                StateHasChanged();
            }
        }

        public async Task SaveAuthorAsync()
        {
            await AuthorService.SaveAuthor(Author);
            await LoadAuthors();
            var firstName = Author.FirstName;
            var lastName = Author.LastName;
            Author = new Author();
            await JSRuntime.InvokeVoidAsync("saveMessage", firstName, lastName);
            await JSRuntime.InvokeVoidAsync("setFocus", firstNameRef);
        }

        private async Task LoadAuthors()
        {
            Authors = await AuthorService.GetAuthors();
            StateHasChanged();

        }
    }
}