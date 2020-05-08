using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Model;
using BookStore.Web.Data.Publishers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStore.Web.Pages.Publishers
{
    public class PublishersBase : ComponentBase
    {
        [Inject]
        public IPublisherService PublisherService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public Publisher Publisher { get; set; } = new Publisher();
        public string[] Cities { get; set; }
        public int MyProperty { get; set; }
        public List<Publisher> Publishers { get; set; }
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
            Result = await PublisherService.SavePublisher(Publisher);
            RecordName = Publisher.PublisherName;
            IsVisible = true;
            await LoadPublishers();
            var name = Publisher.PublisherName;
            Publisher = new Publisher();
            // await JSRuntime.InvokeVoidAsync("saveMessage", name);
            await JSRuntime.InvokeVoidAsync("setFocus", publisherNameRef);
        }

        private async Task LoadPublishers()
        {
            Publishers = await PublisherService.GetPublishers();
            StateHasChanged();
        }

        protected void OnSelectCityChange(ChangeEventArgs changeEventArgs) {
            _selectedCity = changeEventArgs.Value.ToString();
        }
    }
}