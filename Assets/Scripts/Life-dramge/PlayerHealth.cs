using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("ตั้งค่าพลังชีวิต")]
    public int maxHealth = 4; // ตั้งค่าเลือดสูงสุดเป็น 4
    public int currentHealth;

    [Header("ตั้งค่าจุดเกิด")]
    public Transform spawnPoint; // ลากจุดเกิดมาใส่ช่องนี้

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

        // เริ่มเกมมาให้เลือดเต็ม
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount, Vector3 hazardPosition, Vector2 force)
    {
        if (_isKnockedBack) return;

        currentHealth -= amount; // ลดเลือดตามค่า damage ของ Hazard

        // เช็กว่าเลือดหมดหรือยัง
        if (currentHealth <= 0)
        {
            Respawn(); // เลือดหมด สั่งให้ไปเกิดใหม่
        }
        else
        {
            // เลือดยังไม่หมด เล่นแอนิเมชัน Hit และกระเด็นตามปกติ
            if (_animator != null) _animator.SetTrigger("Hit");

            float diff = transform.position.x - hazardPosition.x;
            float knockbackDir = (diff >= 0) ? 1f : -1f;

            StartCoroutine(KnockbackRoutine(knockbackDir, force));
        }
    }

    private void Respawn()
    {
        // 1. ย้ายตัวผู้เล่นกลับไปที่จุดเกิด
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        // 2. รีเซ็ตเลือดให้กลับมาเต็ม 4 ใหม่
        currentHealth = maxHealth;

        // 3. รีเซ็ตแรงฟิสิกส์ต่างๆ ป้องกันบั๊กตัวละครปลิวค้าง
        _rb.linearVelocity = Vector2.zero;
        _rb.linearDamping = _originalDrag;
        _isKnockedBack = false;

        if (_playerController != null)
        {
            _playerController.canMove = true;
        }

        Debug.Log("เลือดหมด! วาร์ปกลับจุดเกิดเรียบร้อย");
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