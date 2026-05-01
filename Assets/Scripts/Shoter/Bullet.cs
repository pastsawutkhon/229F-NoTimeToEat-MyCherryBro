using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Hit Effect Settings")]
    public GameObject hitEffectPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. ถ้าชนวัตถุที่ตั้ง Tag ว่า Breakable ให้ทำลายมันทิ้ง
        if (collision.CompareTag("Breakable"))
        {
            CreateHitEffect();
            Destroy(collision.gameObject);
            Destroy(gameObject);
            return; // ออกจากฟังก์ชันทันทีเพื่อไม่ให้ไปรันเงื่อนไขข้างล่างซ้ำ
        }

        // 2. เงื่อนไขเดิมของคุณ: ถ้าไม่ใช่ Player และไม่ใช่ Bg ให้ระเบิดตัวเอง
        if (!collision.CompareTag("Player") && !collision.CompareTag("Bg"))
        {
            CreateHitEffect();
            Destroy(gameObject);
        }
    }

    private void CreateHitEffect()
    {
        if (hitEffectPrefab != null)
        {
            // สร้างเอฟเฟกต์ตามตำแหน่งและการหมุนของกระสุน
            Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        }
    }
}