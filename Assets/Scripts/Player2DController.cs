using UnityEngine;
using UnityEngine.InputSystem;

public class Player2DController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float jumpForce = 3f;
    public int maxJumps = 2;
    public bool canMove = true;
    public bool IsGrounded { get; private set; }

    private Rigidbody2D _rb;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private float _moveInputValue;
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

        if (canMove)
        {
            _moveInputValue = (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0);

            if (_moveInputValue < 0) { _spriteRenderer.flipX = true; }
            else if (_moveInputValue > 0) { _spriteRenderer.flipX = false; }

            if (Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                if (IsGrounded || _jumpCount < maxJumps)
                {
                    PerformJump();
                }
            }
        }
        else
        {
            _moveInputValue = 0;
        }

        UpdateAnimations();
    }

    void FixedUpdate()
    {
        if (canMove)
        {
            _rb.linearVelocity = new Vector2(_moveInputValue * moveSpeed, _rb.linearVelocity.y);
        }
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
        IsGrounded = false;
    }

    private void UpdateAnimations()
    {
        if(!canMove) return;
        bool isRunning = Mathf.Abs(_moveInputValue) > 0 && IsGrounded;
        _animator.SetBool("Run", isRunning);

        bool isJumping = !IsGrounded && _rb.linearVelocity.y > 0.1f;
        _animator.SetBool("Jump", isJumping);

        bool isFalling = !IsGrounded && _rb.linearVelocity.y < -0.1f;
        _animator.SetBool("Fall", isFalling);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            _jumpCount = 0;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = true;
            _jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            IsGrounded = false;
            if (_jumpCount == 0) _jumpCount = 1;
        }
    }
}