using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage = 1;
    public Vector2 knockbackForce = new Vector2(10f, 8f); // X = แรงถอยหลัง, Y = แรงเด้งขึ้น

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // ส่งข้อมูลตำแหน่งของ Object นี้ไปให้ Player คำนวณทิศทางกระเด็น
            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage, transform.position, knockbackForce);
            }
        }
    }
}