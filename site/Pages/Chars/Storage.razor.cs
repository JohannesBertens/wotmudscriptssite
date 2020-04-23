using Microsoft.JSInterop;
using site.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace site.Pages.Chars
{
    public partial class Storage: IDisposable
    {
        private RentItem[] rentItems;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }


        [CascadingParameter]
        private Task<AuthenticationState> authenticationStateTask { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var user = (await authenticationStateTask).User;

            if (user.Identity.IsAuthenticated)
            {
                // Perform an action only available to authenticated (signed-in) users.
                rentItems = await RentItemsService.GetRentItemsAsync();
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
