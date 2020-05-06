using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBookStore.Data.Publishers;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorBookStore.Pages.Publishers
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
        public bool IsVisible { get; set; } = false;
        public string RecordName { get; set; }
        public bool IsSavedSuccessfully { get; set; }


        protected override async Task OnInitializedAsync()
        {
            await LoadPublishers();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                Cities = await JSRuntime.InvokeAsync<string[]>("getCities");
                StateHasChanged();
            }
        }

        protected async Task SavePublisherAsync()
        {
            IsSavedSuccessfully = await PublisherService.SavePublisher(Publisher);
            RecordName = Publisher.Name;
            IsVisible = true;
            await LoadPublishers();
            var name = Publisher.Name;
            Publisher = new Publisher();
            // await JSRuntime.InvokeVoidAsync("saveMessage", name);
            await JSRuntime.InvokeVoidAsync("setFocus", publisherNameRef);
        }

        private async Task LoadPublishers()
        {
            Publishers = await PublisherService.GetPublishers();
            StateHasChanged();
        }

    }
}