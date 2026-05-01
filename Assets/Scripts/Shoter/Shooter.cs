using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject target;
    public Rigidbody2D bulletPrefab;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // 1. อ่านค่าตำแหน่งเมาส์บนหน้าจอ
            Vector2 screenPos = Mouse.current.position.ReadValue();

            // 2. แปลงตำแหน่งเมาส์ให้กลายเป็นพิกัดในเกม 2D โดยใช้ Camera
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos.z = 0f; // บังคับให้แกน Z เป็น 0 เสมอเพื่อไม่ให้เป้าจมไปในฉากหลัง

            // 3. อัปเดตตำแหน่งเป้า (ถ้ามี)
            if (target != null)
            {
                target.transform.position = worldPos;
            }

            // 4. คำนวณความเร็วและยิงกระสุนออกไป
            Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, worldPos, 0.75f);
            Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            shootBullet.linearVelocity = projectileVelocity;
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 direction = target - origin;
        return new Vector2(direction.x / time, (direction.y / time) + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time);
    }
}