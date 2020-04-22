using Fluxor;

namespace site.Store.SelectedCharacterState
{
    public class Feature : Feature<SelectedCharacterState>
    {
        public override string GetName() => "SelectedCharacter";
        protected override SelectedCharacterState GetInitialState() =>
            new SelectedCharacterState(selectedCharacter: "Asandra");
    }
}
