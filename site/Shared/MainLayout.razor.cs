using site.Store.SelectedCharacterState;
using Fluxor;
using Microsoft.AspNetCore.Components;

namespace site.Shared
{
    public partial class MainLayout
    {
        string[] AllCharacters = new[] { "Asandra", "Mantas" };

        [Inject]
        public IDispatcher Dispatcher { get; set; }

        private void ChangeSelectedCharacter(ChangeEventArgs e)
        {
            var action = new ChangeSelectedCharacterAction(e.Value.ToString());
            Dispatcher.Dispatch(action);
        }
    }
}
