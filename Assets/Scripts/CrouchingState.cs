using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrouchingState : FighterState
{
    public override void HandleInput(Fighter fighter, InputType inputType) {
        Debug.Log("Enter crouching state with " + inputType);
    }
}
