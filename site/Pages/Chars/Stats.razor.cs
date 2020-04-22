using Fluxor;
using Microsoft.AspNetCore.Components;
using site.Store.SelectedCharacterState;

namespace site.Pages.Chars
{
    public partial class Stats
    {
        [Inject]
        private IState<SelectedCharacterState> SelectedCharacterState { get; set; }
    }
}
