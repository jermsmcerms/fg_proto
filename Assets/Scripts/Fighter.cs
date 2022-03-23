using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public static CrouchingState crouchingState;
    public static StandingState standingState;
    public static JumpingState jumpingState;

    public void UpdateState(FighterState newState) {
        if(newState != null) {
            _fighterState = newState;
        }
    }

    public string GetCurrentState() {
        string retval = "";
        if(_fighterState.GetType() == typeof(CrouchingState)) {
            retval = "crouching";
        } else if(_fighterState.GetType() == typeof(StandingState)) {
            retval = "standing";
        } else if(_fighterState.GetType() == typeof(JumpingState)) {
            retval = "jumping";
        }
        return retval;
    }

    public virtual void HandleInput(InputType inputType) {
        _fighterState.HandleInput(this, inputType);
    }

    public virtual void UpdateFighter() {
        _fighterState.Update(this);
    }


    private FighterState _fighterState;

    private void Awake() {
        crouchingState = new CrouchingState();
        standingState = new StandingState();
        jumpingState = new JumpingState();
        _fighterState = new StandingState(); // Set initial state to standing
    }
}
