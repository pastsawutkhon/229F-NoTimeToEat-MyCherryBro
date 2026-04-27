using UnityEngine;

public class SlopePlatform : MonoBehaviour
{
    [Header("Tilt Settings")]
    public float maxTiltAngle = 45f;   // องศาที่เอียงได้มากที่สุด
    public float tiltSpeed = 5f;       // ความเร็วในการเอียง (ยิ่งเยอะยิ่งเอียงตามเร็ว)
    public float tiltSensitivity = 15f; // ความไวต่อระยะห่าง (ยิ่งเยอะแค่เดินนิดเดียวก็เอียงสุดแล้ว)

    private bool _isPlayerOn;
    private Transform _playerTransform;
    private Rigidbody2D _rb;

    void Start()
    {
        // ดึง Rigidbody2D มาใช้เพื่อหมุนแบบฟิสิกส์ (ผู้เล่นจะได้ไม่สั่นกระตุก)
        _rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float targetAngle = 0f;

        if (_isPlayerOn && _playerTransform != null)
        {
            // 1. หาระยะห่างแกน X ระหว่างผู้เล่นกับจุดศูนย์กลางของแพลตฟอร์ม
            float offset = _playerTransform.position.x - transform.position.x;

            // 2. คำนวณมุมเป้าหมาย
            // (ถ้าอยู่ขวา offset เป็นบวก มุมต้องติดลบเพื่อให้ฝั่งขวากดลงไป)
            targetAngle = -offset * tiltSensitivity;

            // 3. จำกัดองศาไม่ให้เกิน -45 และ 45
            targetAngle = Mathf.Clamp(targetAngle, -maxTiltAngle, maxTiltAngle);
        }
        else
        {
            // ถ้าไม่มีผู้เล่น มุมเป้าหมายคือ 0 (กลับมาตรงปกติ)
            targetAngle = 0f;
        }

        // 4. ดึงองศาปัจจุบันออกมา
        float currentAngle = _rb != null ? _rb.rotation : transform.eulerAngles.z;

        // 5. ค่อยๆ เปลี่ยนมุมปัจจุบันไปหามุมเป้าหมายอย่างนุ่มนวล
        float smoothAngle = Mathf.LerpAngle(currentAngle, targetAngle, Time.fixedDeltaTime * tiltSpeed);

        // 6. สั่งหมุนแพลตฟอร์ม
        if (_rb != null)
        {
            _rb.MoveRotation(smoothAngle);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, smoothAngle);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOn = true;
            _playerTransform = collision.transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isPlayerOn = false;
            _playerTransform = null;
        }
    }
}