using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BookStore.Web.Pages
{
    public class IndexBase : ComponentBase
    {
        [Inject]
        public IAuthorizationService AuthorizationService { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState { get; set; }
        protected ClaimsPrincipal user;

        protected bool IsAuthenticated;
        protected bool IsPublisher;

        protected bool IsSeniorEmployee;
        protected override async Task OnInitializedAsync()
        {
            user = (await AuthenticationState).User;

            if (user.Identity.IsAuthenticated) IsAuthenticated = true;

            if (user.IsInRole("Publisher")) IsPublisher = true;

            if ((await AuthorizationService.AuthorizeAsync(user, "SeniorEmployee")).Succeeded)
            {
                IsSeniorEmployee = true;
            }
        }
    }
}