using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fluxor;

namespace site.Store.SelectedCharacterState
{
    public static class Reducers
    {
        [ReducerMethod]
        public static SelectedCharacterState ReduceChangeSelectedCharacterState(SelectedCharacterState state, ChangeSelectedCharacterAction action) =>
            new SelectedCharacterState(action.NewCharacter);
    }
}
