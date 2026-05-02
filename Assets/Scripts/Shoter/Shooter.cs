using UnityEngine;
using UnityEngine.InputSystem;

public class Shooter : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject target;
    public Rigidbody2D bulletPrefab;

    [Header("ตั้งค่าเป้าเล็ง (Layer ที่เมาส์ชี้ได้)")]
    public LayerMask aimLayerMask = Physics2D.DefaultRaycastLayers;

    void Update()
    {
        Vector2 screePos = Mouse.current.position.ReadValue();

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            // [เพิ่ม] เล่นเสียงยิงจาก SoundManager ของเรา
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound(SoundManager.instance.shootSound);
            }

            Ray ray = Camera.main.ScreenPointToRay(screePos);
            Debug.DrawRay(ray.origin, ray.direction * 5, Color.red, 5f);

            // [ปรับปรุง] ใส่ aimLayerMask เข้าไป เพื่อให้เส้นสมมติทะลุเหรียญไปโดนฉากหลังแทน
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, aimLayerMask);

            if (hit.collider != null)
            {
                target.transform.position = new Vector2(hit.point.x, hit.point.y);
                Debug.Log($"Hit {hit.collider.gameObject.name}");

                Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 0.5f);

                Rigidbody2D shootBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

                shootBullet.linearVelocity = projectileVelocity;
            }
        }
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 direction = target - origin;
        return new Vector2(direction.x / time, (direction.y / time) + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time);
    }
}