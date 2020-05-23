using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Web.Data.Publishers;
using BookStore.Web.Services.BookStore;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStore.Web.Pages.Publishers
{
    public class PublishersBase : ComponentBase
    {
        [Inject]
        public IBookStoreService<Publisher> BookStoreService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public Publisher Publisher { get; set; } = new Publisher();
        public string[] Cities { get; set; }
        public List<Publisher> Publishers { get; set; } = new List<Publisher>();
        protected ElementReference publisherNameRef;
        private string _selectedCity;
        public bool IsVisible { get; set; } = false;
        public string RecordName { get; set; }
        public bool Result { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadPublishers();
        }

        protected async Task SavePublisherAsync()
        {
            Publisher.City = _selectedCity;
            Publisher savedPublisher;
            if (Publisher.PubId > 0)
                savedPublisher = await BookStoreService.UpdateAsync("publishers", Publisher.PubId, Publisher);
            else
                savedPublisher = await BookStoreService.SaveAsync("publishers", Publisher);

            Result = savedPublisher == null ? false : true;
            RecordName = Publisher.PublisherName;
            IsVisible = true;
            await LoadPublishers();
            var name = Publisher.PublisherName;
            Publisher = new Publisher();
            await JSRuntime.InvokeVoidAsync("setFocus", publisherNameRef);
        }

        public async Task DeletePublisher(int id)
        {
            await BookStoreService.DeleteAsync("publishers", id);
            await LoadPublishers();
        }

        public void EditPublisher(Publisher publisher)
        {
            Publisher = publisher;
            IsVisible = false;
        }

        private async Task LoadPublishers()
        {
            var publishers = (await BookStoreService.GetAllAsync("publishers"));
            Publishers = publishers != null ? publishers.OrderByDescending(p => p.PubId).ToList() : new List<Publisher>();
            StateHasChanged();
        }

        protected void OnSelectCityChange(ChangeEventArgs changeEventArgs)
        {
            _selectedCity = changeEventArgs.Value.ToString();
        }
    }
}