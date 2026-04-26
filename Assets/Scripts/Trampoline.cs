using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Settings")]
    [Range(1f, 2.5f)]
    public float bounceMultiplier = 1.5f;
    public float minBounceForce = 10f;

    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                float impactVelocityY = Mathf.Abs(playerRb.linearVelocity.y);

                float calculatedForceY = Mathf.Max(impactVelocityY * bounceMultiplier, minBounceForce);

                playerRb.linearVelocity = Vector2.zero;
                
                playerRb.AddForce(new Vector2(0, calculatedForceY), ForceMode2D.Impulse);

                if (_animator != null) _animator.SetTrigger("Bounce");
            }
        }
    }
}