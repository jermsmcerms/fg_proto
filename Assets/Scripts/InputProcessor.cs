using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputProcessor : MonoBehaviour
{
    private Fighter _fighter;
    private float _direction;
    private float _previousDirection; // May consider refactoring to using a PDA if this idea grows more complex

    public void OnMove(InputAction.CallbackContext  context) {
        if(context.performed) {
            _direction = context.ReadValue<float>();
            if (_direction < 0) {
                _fighter.HandleInput(InputType.LEFT);
            } else if (_direction > 0) {
                _fighter.HandleInput(InputType.RIGHT);
            }
        }

        if(context.canceled) {
            _direction = 0.0f;
        }
    }

    public void OnCrouch(InputAction.CallbackContext context) {
        if (context.performed) {
            _fighter.UpdateState(Fighter.crouchingState);
            _fighter.HandleInput(InputType.DOWN);
            _previousDirection = _direction;
            _direction = 0.0f;
        }
        if (context.canceled) {
            _fighter.UpdateState(Fighter.standingState);
            _fighter.HandleInput(InputType.NETURAL);
            _direction = _previousDirection;
        }
    }

    private void Awake() {
        _fighter = GetComponent<Fighter>();
    }

    private void Update() {
        Debug.Log("direction " + _direction);
    }
}
