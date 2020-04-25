using Microsoft.JSInterop;
using site.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace site.Pages.Chars
{
    public partial class Storage: IDisposable
    {
        public RentItem[] RentItems { get; set; }

        [Inject] public IJSRuntime JSRuntime { get; set; }

        [Inject] public IHttpContextAccessor Context { get; set; }

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
                RentItems = await RentItemsService.GetRentItemsAsync(applicationUser);
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await JSRuntime.InvokeVoidAsync("toDataTable", "#storageTable");
        }

        public void Dispose()
        {
            JSRuntime.InvokeVoidAsync("removeDataTable", "#storageTable");
        }
    }
}
