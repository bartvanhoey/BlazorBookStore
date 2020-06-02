using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BookStore.Components
{
    public class SelectCityBase : ComponentBase
    {
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Parameter] public string CurrentCity { get; set; }
        [Parameter]public EventCallback<ChangeEventArgs> OnChangeEvent { get; set; }
        public string[] Cities { get; set; }
        
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender && Cities == null)
            {
                Cities = await JSRuntime.InvokeAsync<string[]>("getCities");
                StateHasChanged();
            }
        }
    }
}