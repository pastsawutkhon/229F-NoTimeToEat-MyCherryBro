using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 3;
    
    private Rigidbody2D _rb;
    private Animator _animator;
    private float _originalDrag;
    private bool _isKnockedBack = false;
    private Player2DController _playerController;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _originalDrag = _rb.linearDamping;
        _playerController = GetComponent<Player2DController>();
    }

    public void TakeDamage(int amount, Vector3 hazardPosition, Vector2 force)
    {
        if (_isKnockedBack) return;

        health -= amount;
        if (_animator != null) _animator.SetTrigger("Hit");

        float diff = transform.position.x - hazardPosition.x;
        float knockbackDir = (diff >= 0) ? 1f : -1f;

        StartCoroutine(KnockbackRoutine(knockbackDir, force));
    }

    private IEnumerator KnockbackRoutine(float knockbackDir, Vector2 force)
    {
        _isKnockedBack = true;
        if (_playerController != null) 
        {
            _playerController.canMove = false;
            var anim = GetComponent<Animator>();
            anim.SetBool("Run", false);
            anim.SetBool("Jump", false);
            anim.SetBool("Fall", false);
        }

        _rb.linearDamping = 0;
        _rb.linearVelocity = Vector2.zero;

        Vector2 finalKnockback = new Vector2(knockbackDir * force.x, Mathf.Abs(force.y));
        _rb.AddForce(finalKnockback, ForceMode2D.Impulse);

        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => _playerController.IsGrounded);

        _rb.linearDamping = _originalDrag;
        _isKnockedBack = false;
        if (_playerController != null) _playerController.canMove = true;
    }
}