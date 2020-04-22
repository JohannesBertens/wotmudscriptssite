using Microsoft.JSInterop;
using site.Data;
using System;
using System.Threading.Tasks;

namespace site.Pages.Chars
{
    public partial class Storage: IDisposable
    {
        private RentItem[] rentItems;

    protected override async Task OnInitializedAsync()
        {
            rentItems = await RentItemsService.GetRentItemsAsync();
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
