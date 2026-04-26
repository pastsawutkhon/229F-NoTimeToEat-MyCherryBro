using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 3f;
    public int maxJumps = 2; 

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _moveInputValue;
    
    private bool _isGrounded;
    private int _jumpCount; 

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current == null) return;

        _moveInputValue = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);

        if (_moveInputValue < 0) { _spriteRenderer.flipX = true; }
        else if (_moveInputValue > 0) { _spriteRenderer.flipX = false; }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            if (_isGrounded || _jumpCount < maxJumps)
            {
                PerformJump();
            }
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        _rb.linearVelocity = new Vector2(_moveInputValue * moveSpeed, _rb.linearVelocity.y);
    }

    private void PerformJump()
    {
        _rb.linearVelocity = new Vector2(_rb.linearVelocity.x, 0f);
        _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        _animator.SetBool("Run", false);
        _animator.SetBool("Jump", true);

        _animator.ResetTrigger("DbJump");
        if (_jumpCount > 0)
        {
            _animator.SetTrigger("DbJump");
        }

        _jumpCount++;
        _isGrounded = false;
    }

    private void UpdateAnimations()
    {
        bool isRunning = Mathf.Abs(_moveInputValue) > 0 && _isGrounded;
        _animator.SetBool("Run", isRunning);

        bool isJumping = !_isGrounded && _rb.linearVelocity.y > 0.1f;
        _animator.SetBool("Jump", isJumping);

        bool isFalling = !_isGrounded && _rb.linearVelocity.y < -0.1f;
        _animator.SetBool("Fall", isFalling);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _jumpCount = 0; 
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = true;
            _jumpCount = 0; 
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _isGrounded = false;
            if (_jumpCount == 0) _jumpCount = 1;
        }
    }
}