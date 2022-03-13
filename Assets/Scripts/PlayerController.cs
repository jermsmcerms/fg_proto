using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour {
    [SerializeField] private GameObject _groundCheck;
    [SerializeField] private LayerMask _groundLayer;
    
    private Rigidbody2D _rigidbody2D;
    private PlayerControls _playerControls;


    private Vector2 _velocity;

    private float _speed;
    private float _movementSpeed;

    public void OnJump(InputAction.CallbackContext context) {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.transform.position, 0.05f, _groundLayer);
        if (colliders.Length > 0) {
            _velocity.y = 12.0f;
        }
    }

    // Awake is called when the script instance is being loaded
    private void Awake() {
        _playerControls = new PlayerControls();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _velocity = Vector2.zero;
        _speed = 10.0f;
        _movementSpeed = 10.0f;

    }

    // Start is called before the first frame update
    private void Start() { }

    // Update is called every frame
    private void Update() {
        float direction = _playerControls.MatchControlls.Movement.ReadValue<float>();
        _velocity.y += -9.81f * Time.deltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(_groundCheck.transform.position, 0.05f, _groundLayer);
        if (colliders.Length > 0 && _velocity.y < 0) {
            _velocity.y = 0.0f;
        }

        _velocity = new Vector2(direction * _movementSpeed, _velocity.y);
    }

    // FixedUpdate is called every fixed frame-rate frame
    private void FixedUpdate() {
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
