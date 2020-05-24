using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace BookStore.Components
{
    public partial class TableTemplateBase<TItem> : ComponentBase
    {
        [Parameter]
        public RenderFragment TableHeader { get; set; }
        
        [Parameter]
        public RenderFragment<TItem> TableRows { get; set; }

        [Parameter]
        public IReadOnlyList<TItem> Items { get; set; }

    }
}