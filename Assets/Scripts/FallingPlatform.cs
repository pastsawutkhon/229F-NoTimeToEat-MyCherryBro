using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public float fallSpeed = 2f;
    public float returnSpeed = 1f;

    private Vector2 _startPos;
    private bool _isPlayerOn;
    private Animator _animator;

    void Start()
    {
        _startPos = transform.position;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (_isPlayerOn)
        {
            transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, _startPos, returnSpeed * Time.deltaTime);
        }

        if (_animator != null)
        {
            _animator.SetBool("Off", _isPlayerOn);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // เช็คว่าผู้เล่นเหยียบอยู่ด้านบนจริงๆ
            if (collision.transform.position.y > transform.position.y)
            {
                _isPlayerOn = true;
                // [แก้บั๊ก] จับผู้เล่นมาเป็น Child ของ Platform 
                collision.transform.SetParent(transform);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOn = false;
            // [แก้บั๊ก] ปลดผู้เล่นออกจากการเป็น Child เมื่อกระโดดออกไป
            collision.transform.SetParent(null);
        }
    }
}