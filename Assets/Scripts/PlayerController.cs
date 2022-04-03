using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private BoxCollider2D _pushBox;

    private PlayerControls _playerControls;
    private Rigidbody2D _rigidbody;
    private Fighter _fighter;

    private float _velocity;
    private bool _beginCharge;

    private int _bufferSize = 60;
    private int[] _inputBuffer;
    private int _counter;
    private float _direction;

    // Could be moved to data?
    static int[] leftDash = { 1, 1 };
    static int[] rightDash = { 2, 2 };

    public void OnAttack(InputAction.CallbackContext context) {
        if (!_fighter.PerformingAction()) {
            if (context.performed) {
                _beginCharge = true;
                if (_direction == 0) {
                    _fighter.PerformLongPokeAttack();
                } else {
                    _fighter.PerformShortPokeAttack();
                }
            } else if (context.canceled) {
                _beginCharge = false;
                if (_fighter.IsAttackCharged()) {
                    if (_direction == 0) {
                        _fighter.PerformSpecialAttack();
                    } else {
                        _fighter.PerformInvincibleAttack();
                    }
                }
            }
        } else if (_fighter.CanCancel()) {
            _fighter.PerformSpecialAttack();
        }
    }
    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed) {
            _direction = context.ReadValue<float>();
            if (_direction < 0) {
                _inputBuffer[mod(_counter, _bufferSize)] = 1;
            } else if (_direction > 0) {
                _inputBuffer[mod(_counter, _bufferSize)] = 2;
            }
        } else if (context.canceled) {
            _direction = 0.0f;
        }

    }

    // Start is called before the first frame update
    private void Awake() {
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _fighter = GetComponent<Fighter>();

        _inputBuffer = new int[_bufferSize];

    }

    // Update is called once per frame
    private void Update() {
        _velocity = _direction * _speed;
    }

    private bool CheckSequence(int[] sequence, int duration) {
        int w = sequence.Length - 1;
        for (int i = 0; i < duration; i++) {
            int index = mod(mod(_counter, _bufferSize) - i, _bufferSize);
            int direction = _inputBuffer[index];

            if (direction == sequence[w]) {
                --w;
            }

            if (w == -1) {
                return true;
            }
        }

        return false;
    }

    private void ClearInputBuffer() {
        for(int i = 0; i < _inputBuffer.Length; i++) {
            _inputBuffer[i] = 0;
        }
    }

    private void FixedUpdate() {
        _counter++;
        _inputBuffer[mod(_counter, _bufferSize)] = 0;

        if (!_fighter.PerformingAction()) {
            if (_fighter.IsFacingRight()) {
                if(_direction < 0) {
                    _fighter.SetBlocking(true);
                } else {
                    _fighter.SetBlocking(false);
                }
            } else {
                if (_direction > 0) {
                    _fighter.SetBlocking(true);
                } else {
                    _fighter.SetBlocking(false);
                }
        }

            if(_beginCharge) {
                _fighter.ChargeSpecialAttack();
            }

            if (CheckSequence(leftDash, 9)) {
                _fighter.PerformLeftDash();
                ClearInputBuffer();
            } else if (CheckSequence(rightDash, 9)) {
                _fighter.PerformRightDash();
                ClearInputBuffer();
            }     
        } else {
            _velocity = 0;
        }

        _rigidbody.velocity = new Vector3(_velocity, 0, 0);
    }

    private void OnEnable() {
            _playerControls.MatchControlls.Enable();
    }


    private void OnDisable() {
        _playerControls.MatchControlls.Disable();
    }

    /*
     *  A little helper function to get mathamatically correct modulus division.
     *  TODO: move into a separate file
     */
    private static int mod(int lhs, int rhs) {
        int modValue = lhs % rhs;
        return modValue < 0 ? modValue + rhs : modValue;
    }
}
