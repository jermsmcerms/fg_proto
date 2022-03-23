using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandingState : FighterState
{
    public override void HandleInput(Fighter fighter, InputType inputType) {
        Debug.Log("Enter standing state with " + inputType);
    }
}
