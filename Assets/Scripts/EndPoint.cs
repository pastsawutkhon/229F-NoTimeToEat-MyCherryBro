using UnityEngine;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เปลี่ยนจาก "Bullet" เป็น "Player"
        if (collision.CompareTag("Player"))
        {
            Debug.Log("You Win!");
        }
    }
}