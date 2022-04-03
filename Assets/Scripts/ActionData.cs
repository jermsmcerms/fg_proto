using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType {
    Movement, Guard, Attack, Damage
}

[CreateAssetMenu(menuName = "Action Data")]
public class ActionData : ScriptableObject {
    public ActionType actionType;
}

public abstract class ActionBase : ScriptableObject {
    public int startFrame;
    public Hurtbox[] hurtboxes;
}

public class Hurtbox : ActionBase {
    public Vector2 size { get; set; }
    public Vector2 offset { get; set; }
}
