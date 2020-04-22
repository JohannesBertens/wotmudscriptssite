using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace site.Store.SelectedCharacterState
{
    public class ChangeSelectedCharacterAction
    {
        public string NewCharacter { get; }
        public ChangeSelectedCharacterAction(string character)
        {
            NewCharacter = character;
        }
    }
}
