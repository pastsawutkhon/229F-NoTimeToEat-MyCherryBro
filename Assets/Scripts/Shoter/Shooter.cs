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
            // [เพิ่ม] เล่นเสียงยิง
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound(SoundManager.instance.shootSound);
            }

            Vector2 screenPos = Mouse.current.position.ReadValue();
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(screenPos);
            worldPos.z = 0f;

            if (target != null)
            {
                target.transform.position = worldPos;
            }

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