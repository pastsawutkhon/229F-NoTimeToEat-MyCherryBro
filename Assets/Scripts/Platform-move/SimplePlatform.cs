using UnityEngine;

public class SimplePlatform : MonoBehaviour
{
    [Header("ตั้งค่าการเลื่อน")]
    public float moveDistance = 3f; // อยากให้เลื่อนออกไปไกลแค่ไหน (ซ้าย-ขวา)
    public float speed = 2f;        // ความเร็ว

    private Vector2 _startPos;

    void Start()
    {
        // จำจุดเริ่มต้นไว้
        _startPos = transform.position;
    }

    void Update()
    {
        // ใช้สมการคลื่น (Sin) เพื่อให้มันแกว่งซ้าย-ขวาจากจุดเริ่มต้นอย่างนุ่มนวล
        float offset = Mathf.Sin(Time.time * speed) * moveDistance;

        // ขยับเฉพาะแกน X (ซ้าย-ขวา) ส่วนแกน Y อยู่กับที่
        transform.position = _startPos + new Vector2(offset, 0);
    }

    // ---------------------------------------------------------
    // ส่วนนี้ตัดออกไม่ได้ครับ ต้องมีไว้เพื่อให้ตัวละครไม่ร่วงเวลาพื้นเลื่อน
    // ---------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}