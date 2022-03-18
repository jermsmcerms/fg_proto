using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private Hurtboxes _hurtboxes;
    [SerializeField] private LayerMask _groundLayer;
    
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _pushBox;
    private SpriteRenderer _pushBoxSR;
    private PlayerControls _playerControls;


    private Vector2 _velocity;

    private float _speed;
    private float _movementSpeed;
    private float _direction;
    private bool _isGrounded;
    private bool _isCrouching;

    public void OnCrouch(InputAction.CallbackContext context) { 
        if(context.performed && !_hurtboxes.PerfomingAttack()) {
            _isCrouching = true;
        } else if(context.canceled) {
            _isCrouching = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context) {
        if (context.performed) {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.transform.position, 0.05f, _groundLayer);
            if (colliders.Length > 0) {
                _velocity.y = 12.0f;
                _pushBox.size = new Vector2(_pushBox.size.x, _pushBox.size.y * 0.75f);
                if (!Mathf.Approximately(_direction, 0.0f)) {
                    _velocity = new Vector2(_direction * _movementSpeed, _velocity.y);
                }
                _hurtboxes.ResizeForJump();
                _isGrounded = false;
                _pushBoxSR.sprite = Resources.Load<Sprite>("Sprites/pushbox_air");
            }
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context) { 
        if(context.performed) {
            if(!_hurtboxes.PerfomingAttack() && _isGrounded) {
                _hurtboxes.StartAttack(14);
            }
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake() {
        _playerControls = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _pushBox = GetComponent<BoxCollider2D>();
        _pushBoxSR = GetComponent<SpriteRenderer>();
        _pushBoxSR.sprite = Resources.Load<Sprite>("Sprites/pushbox_ground");
        _pushBoxSR.sortingLayerName = "Foreground";
        _velocity = Vector2.zero;
        _speed = 10.0f;
        _movementSpeed = 10.0f;

    }

    // Start is called before the first frame update
    private void Start() { }

    // Update is called every frame
    private void Update() {
        _direction = _playerControls.MatchControlls.Movement.ReadValue<float>();
        _velocity.y += -9.81f * Time.deltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.transform.position, 0.05f, _groundLayer);
        if (colliders.Length > 0 && _velocity.y < 0) {
            _velocity.y = 0.0f;
            // Don't really need to do this every frame. I can check if I'm grounded every frame and 
            // if I just touched the ground do this stuff then don't do it again until after I've jumped.
            if(!_isGrounded) {
                _pushBoxSR.sprite = Resources.Load<Sprite>("Sprites/pushbox_ground");
                _hurtboxes.ResetDefaultBoxSettings();
                _isGrounded = true;
            }
        }

        if (_hurtboxes.PerfomingAttack() || _isCrouching) {
            _direction = 0.0f;
        }

        if(_isGrounded) {
            if (_isCrouching) {
                _pushBox.offset = new Vector2(0.0f, -0.34f);
                _pushBox.size = new Vector2(1.3f, 1.9f);
            } else {
                _pushBox.offset = Vector2.zero;
                _pushBox.size = new Vector2(1.3f, 2.57f);
            }
        }

        if (Mathf.Approximately(_velocity.y, 0.0f)) {
            _velocity = new Vector2(_direction * _movementSpeed, _velocity.y);
        }
    }

    // FixedUpdate is called every fixed frame-rate frame
    private void FixedUpdate() {
        _hurtboxes.UpdateHurtboxes(_isGrounded, _isCrouching);
        _rigidbody2D.velocity = _velocity * _speed * Time.deltaTime;
    }

    private void OnEnable() {
        _playerControls.MatchControlls.Enable(); 
    }

    private void OnDisable() {
        _playerControls.MatchControlls.Disable();
    }

    private void OnDrawGizmos() {
        Gizmos.DrawWireSphere(_groundCheck.transform.position, 0.05f);
    }
}
