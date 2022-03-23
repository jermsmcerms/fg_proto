using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : FighterState
{
    public override void HandleInput(Fighter fighter, InputType inputType) {
        Debug.Log("Enter jump state with " + inputType);
    }
}
