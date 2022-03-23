using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterState
{
    public virtual void HandleInput(Fighter fighter, InputType inputState) { }
    public virtual void Update(Fighter fighter) { }
}
