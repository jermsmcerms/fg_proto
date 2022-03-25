using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField]private float _speed = 2.0f;

    private PlayerControls _playerControls;
    private Rigidbody2D _rigidbody;
    private Fighter _fighter;

    private float _velocity;
    private bool _beginCharge;

    public void OnAttack(InputAction.CallbackContext context) { 
        if(context.performed) {
            _beginCharge = true;
            if(!_fighter.PerformingAttack()) {
                if(Mathf.Approximately(_velocity, 0.0f)) {
                    _fighter.PerformLongPokeAttack();
                } else {
                    _fighter.PerformShortPokeAttack();
                }
            }
        } else if(context.canceled) {
            _beginCharge = false;
            if(_fighter.IsAttackCharged() && !_fighter.PerformingAttack()) {
                if(Mathf.Approximately(_velocity, 0.0f)) {
                    _fighter.PerformSpecialAttack();
                } else {
                    _fighter.PerformInvincibleAttack();
                }
            }
        }
    }

    // Start is called before the first frame update
    void Awake() {
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _fighter = GetComponent<Fighter>();
    }

    // Update is called once per frame
    void Update() {
        _velocity = _playerControls.MatchControlls.Movement.ReadValue<float>() * _speed;
    }

    private void FixedUpdate() {
        if (_beginCharge && !_fighter.PerformingAttack()) {
            _fighter.ChargeSpecialAttack();
        }

        _rigidbody.velocity = new Vector3(_velocity, 0, 0);
    }

    private void OnEnable() {
        _playerControls.MatchControlls.Enable();
    }


    private void OnDisable() {
        _playerControls.MatchControlls.Disable();
    }
}
