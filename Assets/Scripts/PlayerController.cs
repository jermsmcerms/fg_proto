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
            //_attackValue = 4;
            //if (!_fighter.PerformingAttack()) {
            //    if (Mathf.Approximately(_velocity, 0.0f)) {
            //        _fighter.PerformLongPokeAttack();
            //    } else {
            //        _fighter.PerformShortPokeAttack();
            //    }
            //}
        } else if (context.canceled) {
            _beginCharge = false;
            //_attackValue = 0;
            //if (_fighter.IsAttackCharged() && !_fighter.PerformingAttack()) {
            //    if (Mathf.Approximately(_velocity, 0.0f)) {
            //        _fighter.PerformSpecialAttack();
            //    } else {
            //        _fighter.PerformInvincibleAttack();
            //    }
            //}
        }
    }

    float _direction;
    public void OnMove(InputAction.CallbackContext context) {
        if (context.performed) {
            _direction = context.ReadValue<float>();
            if (_direction < 0) {
                _inputBuffer[(counter + _bufferSize) % _bufferSize][0] = 1;
            } else if (_direction > 0) {
                _inputBuffer[(counter + _bufferSize) % _bufferSize][0] = 2;
            }
        } else if (context.canceled) {
            _direction = 0.0f;
        }
    }

    // Start is called before the first frame update
    void Awake() {
        _playerControls = new PlayerControls();
        _rigidbody = GetComponent<Rigidbody2D>();
        _fighter = GetComponent<Fighter>();

        _inputBuffer = new int[_bufferSize][];
        for(int i = 0; i < _inputBuffer.Length; i++) { 
            _inputBuffer[i] = new int[_numSimultaniousInputs];
        }
    }

    // Update is called once per frame
    void Update() {
        if(_fighter.IsAttackCharged()) {
           // _attackValue = 8;
        }

        _velocity = _direction * _speed;
    }

    int _bufferSize = 60;
    int _numSimultaniousInputs = 2;
    int[][] _inputBuffer; // TODO: Change to list of class InputInfo that contains input and timestamp. If the input has been in the buffer too long, remove it based on its time stamp.
    int counter;

    static int[] leftDash = { 1, 1 };
    static int[] rightDash = { 2, 2 };
    static int[] shortPoke = { 1, 2, 4 };
    static int[] invincibleAttack = { 1, 2, 8 };
    private int input;
    private int _inputDirection;
    private int _attackValue;

    private bool CheckSequence(int[] sequence, int duration) {
        int w = sequence.Length - 1;     
        for (int i = 0; i < duration; i++) {
            int index = (counter - i + _bufferSize) % _bufferSize;
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
        for (int i = 0; i < _inputBuffer.Length; i++) {
            _inputBuffer[i][0] = 0;
            _inputBuffer[i][1] = 0;
        }
    }

    private void FixedUpdate() {
        _inputBuffer[(counter + _bufferSize) % _bufferSize][1] = _attackValue;
        counter++;

        if (_beginCharge && !_fighter.PerformingAttack()) {
            _fighter.ChargeSpecialAttack();
        }

        if (CheckSequence(leftDash, 9)) {
            Debug.Log("dash left");
            ClearInputBuffer();
        } else if (CheckSequence(rightDash, 9)) {
            Debug.Log("dash right");
            ClearInputBuffer();
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
