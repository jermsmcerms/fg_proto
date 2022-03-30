using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class InputData {
    public int[] inputs;
    public float timestamp;

    public InputData(int[] inputs, float timestamp) {
        this.inputs = inputs;
        this.timestamp = timestamp;
    }
}

public class PlayerController : MonoBehaviour {
    [SerializeField] private float _speed = 2.0f;

    private PlayerControls _playerControls;
    private Rigidbody2D _rigidbody;
    private Fighter _fighter;

    private float _velocity;
    private bool _beginCharge;

    private int _bufferSize = 60;
    private int _numSimultaniousInputs = 2;
    private int[][] _inputBuffer;
    private int _counter;
    private float _direction;
    private float _dashVelocity;

    static int[] leftDash = { 1, 1 };
    static int[] rightDash = { 2, 2 };

    public void OnAttack(InputAction.CallbackContext context) {
        if (context.performed) {
            _beginCharge = true;
        } else if (context.canceled) {
            _beginCharge = false;
        }
    }

    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed) {
            _direction = context.ReadValue<float>();
            if (_direction < 0) {
                _inputBuffer[mod(_counter, _bufferSize)][0] = 1;
            } else if (_direction > 0) {
                _inputBuffer[mod(_counter, _bufferSize)][0] = 2;
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

        _inputBuffer = new int[_bufferSize][];
        for (int i = 0; i < _inputBuffer.Length; i++) {
            _inputBuffer[i] = new int[_numSimultaniousInputs];
        }

        _dashVelocity = 20.0f;

    }

    // Update is called once per frame
    private void Update() {
        if(_fighter.IsAttackCharged()) {
           // _attackValue = 8;
        }

        _velocity = _direction * _speed;
    }

    private bool CheckSequence(int[] sequence, int duration) {
        int w = sequence.Length - 1;
        for (int i = 0; i < duration; i++) {
            int index = mod(mod(_counter, _bufferSize) - i, _bufferSize);
            int direction = _inputBuffer[index][0];

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
            _inputBuffer[i][0] = 0;
            _inputBuffer[i][1] = 0;
        }
    }

    private void FixedUpdate() {
        _counter++;
        _inputBuffer[mod(_counter, _bufferSize)][0] = 0; 
        _inputBuffer[mod(_counter, _bufferSize)][1] = 0; 

        if (_beginCharge && !_fighter.PerformingAttack()) {
            _fighter.ChargeSpecialAttack();
        }

        if (!_fighter.PerformingAttack()) {
            if (CheckSequence(leftDash, 9) ||CheckSequence(rightDash, 9)) {
                _velocity *= _dashVelocity;
                ClearInputBuffer();
            }
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
