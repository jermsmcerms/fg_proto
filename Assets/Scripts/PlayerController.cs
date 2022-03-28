using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _speed = 2.0f;

    private PlayerControls _playerControls;
    private Rigidbody2D _rigidbody;
    private Fighter _fighter;

    private float _velocity;
    private bool _beginCharge;

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.performed) {
            _beginCharge = true;
            if (!_fighter.PerformingAttack()) {
                if (Mathf.Approximately(_velocity, 0.0f)) {
                    _fighter.PerformLongPokeAttack();
                } else {
                    _fighter.PerformShortPokeAttack();
                }
            }
        } else if (context.canceled) {
            _beginCharge = false;
            if (_fighter.IsAttackCharged() && !_fighter.PerformingAttack()) {
                if (Mathf.Approximately(_velocity, 0.0f)) {
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

    int buffer_size = 20;
    int[] input_buffer;
    int counter;

    static int[] leftDash = { 1, 1 };
    static int[] rightDash = { 2, 2 };
    private int input;

    private bool CheckSequence(int[] sequence, int duration) {
        int w = sequence.Length - 1;
        for (int i = 0; i < duration; i++) {
            int direction = input_buffer[(counter - i + buffer_size) % buffer_size];
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
        for (int i = 0; i < input_buffer.Length; i++) {
            input_buffer[i] = 5;
        }
    }

    private void FixedUpdate() {
        input_buffer[(counter + buffer_size) % buffer_size] = input;
        counter++;
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
