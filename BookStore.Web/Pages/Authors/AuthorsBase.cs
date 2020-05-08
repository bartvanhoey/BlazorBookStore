using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Web.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStore.Web.Pages
{
    public class AuthorsBase : ComponentBase
    {
        [Inject]
        public IAuthorService AuthorService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public List<Author> Authors { get; set; } = new List<Author>();

        public bool IsVisible { get; set; } = false;
        public Author Author { get; set; } = new Author();
        protected ElementReference firstNameRef;
        public string[] Cities { get; set; }
        public string RecordName { get; set; }
        public bool Result { get; set; }
        private string _selectedCity;

        private async Task LoadAuthors()
        {
            Authors = (await AuthorService.GetAuthors()).OrderByDescending(a => a.AuthorId).ToList();
            StateHasChanged();
        }
        protected void OnSelectCityChange(ChangeEventArgs changeEventArgs)
        {
            _selectedCity = (string)changeEventArgs.Value;
        }
        public async Task SaveAuthorAsync()
        {
            Author.City = _selectedCity;
            // Result = false; // await AuthorService.SaveAuthor(Author);
            Result = await AuthorService.SaveAuthor(Author);
            IsVisible = true;
            RecordName = Author.FirstName + " " + Author.LastName;
            await LoadAuthors();
            Author = new Author();
            // await JSRuntime.InvokeVoidAsync("saveMessage", firstName, lastName);
            await JSRuntime.InvokeVoidAsync("setFocus", firstNameRef);
        }

        public async Task DeleteAuthor(int id)
        {
            await AuthorService.DeleteAuthor(id);
            await LoadAuthors();
        }

        public void EditAuthor(Author author)
        {
            Author = author;
        }

        protected override void OnInitialized()
        {
            // Console.WriteLine("Authors: OnInitialized");
        }
        protected override async Task OnInitializedAsync()
        {
            // Console.WriteLine("Authors: OnInitializedAsync");
            await LoadAuthors();
        }
        protected override void OnParametersSet()
        {
            // Console.WriteLine("Authors: OnParametersSet");
        }
        protected override async Task OnParametersSetAsync()
        {
            // Console.WriteLine("Authors: OnParametersSetAsync");
            Result = await AuthorService.CheckConnection();
            IsVisible = true;
        }

        protected override void OnAfterRender(bool firstRender)
        {
            // Console.WriteLine($"Authors: OnAfterRender - firstRender = {firstRender}");
        }
        // protected override async Task OnAfterRenderAsync(bool firstRender)
        // {
        //     // Console.WriteLine($"Authors: OnAfterRenderAsync - firstRender = {firstRender}");
        //     await base.OnAfterRenderAsync(firstRender);
        // }

        // protected override bool ShouldRender(){
        //     Console.WriteLine("Authors: ShouldRender");
        //     return false;
        // }

        // public void Dispose(){
        //     Console.WriteLine("Authors: Dispose");
        // }
    }
}