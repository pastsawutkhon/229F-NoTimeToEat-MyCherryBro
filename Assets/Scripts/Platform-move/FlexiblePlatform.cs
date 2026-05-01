using UnityEngine;

public class FlexiblePlatform : MonoBehaviour
{
    [Header("ตั้งค่าการเลื่อน")]
    [Tooltip("ทิศทางที่ต้องการให้เลื่อน เช่น X=1 Y=0 (ซ้ายขวา), X=0 Y=1 (ขึ้นลง)")]
    public Vector2 moveDirection = new Vector2(1, 0);

    [Tooltip("ระยะทางที่จะสวิงออกไปจากจุดเริ่มต้น")]
    public float moveDistance = 3f;

    [Tooltip("ความเร็วในการเลื่อน")]
    public float speed = 2f;

    private Vector2 _startPos;
    private Vector2 _normalizedDirection;

    void Start()
    {
        // จำจุดเริ่มต้นไว้
        _startPos = transform.position;

        // ทำให้แน่ใจว่าทิศทางที่กรอกมามีความยาวเป็น 1 เสมอ (เพื่อการคำนวณที่แม่นยำ)
        _normalizedDirection = moveDirection.normalized;
    }

    void Update()
    {
        // ใช้ Mathf.Sin คำนวณระยะการสวิง (-1 ถึง 1) แล้วคูณด้วยระยะทางที่ต้องการ
        float offset = Mathf.Sin(Time.time * speed) * moveDistance;

        // คำนวณตำแหน่งใหม่ โดยเอาทิศทางคูณกับระยะการสวิง แล้วบวกเข้ากับจุดเริ่มต้น
        transform.position = _startPos + (_normalizedDirection * offset);
    }

    // ---------------------------------------------------------
    // พาผู้เล่นไปด้วย
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