using System.Threading.Tasks;
using BookStore.Web.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Web.Shared
{
    public class MainLayoutBase : LayoutComponentBase
    {
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public async Task Logout()
        {
            await ((CustomAuthenticationStateProvider)AuthenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}