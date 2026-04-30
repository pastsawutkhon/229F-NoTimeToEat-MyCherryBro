using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Hit Effect Settings")]
    [Tooltip("ลาก Prefab เอฟเฟกต์การชนมาใส่ในช่องนี้")]
    public GameObject hitEffectPrefab; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
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
            // สร้างเอฟเฟกต์ออกมาตรงตำแหน่ง (transform.position) และการหมุน (transform.rotation) ของกระสุน ณ ตอนนั้น
            Instantiate(hitEffectPrefab, transform.position, transform.rotation);
        }
    }
}