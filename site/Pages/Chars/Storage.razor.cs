using Microsoft.JSInterop;
using site.Data;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;


namespace site.Pages.Chars
{
    public partial class Storage: IDisposable
    {
        private RentItem[] rentItems;

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

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
