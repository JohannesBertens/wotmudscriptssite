using Fluxor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using site.Data;
using site.Store.SelectedCharacterState;
using System.Threading.Tasks;

namespace site.Pages.Chars
{
    public partial class Stats
    {
        public CharData[] CharacterData { get; set; }

        [Inject]
        private IState<SelectedCharacterState> SelectedCharacterState { get; set; }

        [Inject] public IHttpContextAccessor Context { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        [Inject] public UserManager<ApplicationUser> UserManager { get; set; }

        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask).User;
            if (user.Identity.IsAuthenticated)
            {
                // Perform an action only available to authenticated (signed-in) users.
                var applicationUser = await UserManager.GetUserAsync(Context.HttpContext.User);
                CharacterData = await CharDataService.GetCharDataAsync(applicationUser);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("toList", "#myList");
        }

    }
}
