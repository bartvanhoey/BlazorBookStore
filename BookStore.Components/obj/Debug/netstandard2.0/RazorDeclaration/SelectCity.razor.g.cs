#pragma checksum "c:\_Data\Blazor\BlazorBookStore\BookStore.Components\SelectCity.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e15577a697532bb711808e4edea8409f83d718cf"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace BookStore.Components
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#line 1 "c:\_Data\Blazor\BlazorBookStore\BookStore.Components\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#line 2 "c:\_Data\Blazor\BlazorBookStore\BookStore.Components\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
    public partial class SelectCity : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
        }
        #pragma warning restore 1998
#line 14 "c:\_Data\Blazor\BlazorBookStore\BookStore.Components\SelectCity.razor"
      
    [Parameter]
    public string CurrentCity { get; set; }
    public string[] Cities { get; set; }
    [Parameter]
    public EventCallback<ChangeEventArgs> OnChangeEvent {get; set;}
    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && Cities == null)
        {
            Cities = await JSRuntime.InvokeAsync<string[]>("getCities");
            StateHasChanged();
        }
    }

#line default
#line hidden
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private IJSRuntime JSRuntime { get; set; }
    }
}
#pragma warning restore 1591
