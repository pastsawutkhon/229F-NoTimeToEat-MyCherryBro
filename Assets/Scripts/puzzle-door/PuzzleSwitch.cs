using UnityEngine;
using UnityEngine.Events;

public class PuzzleSwitch : MonoBehaviour
{
    [Header("สถานะเริ่มต้นของสวิตช์")]
    public bool isOn = false;

    [Header("คำสั่งเมื่อเปิดสวิตช์ (ยิงแล้ว On)")]
    public UnityEvent onSwitchOn;

    [Header("คำสั่งเมื่อปิดสวิตช์ (ยิงแล้ว Off)")]
    public UnityEvent onSwitchOff;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            // สลับสถานะ (ถ้าเป็น false จะกลายเป็น true, ถ้าเป็น true จะกลายเป็น false)
            isOn = !isOn;

            if (isOn)
            {
                onSwitchOn.Invoke(); // เรียกคำสั่งเปิด
                GetComponent<SpriteRenderer>().color = Color.green; // เปลี่ยนสีให้รู้ว่าเปิด
            }
            else
            {
                onSwitchOff.Invoke(); // เรียกคำสั่งปิด
                GetComponent<SpriteRenderer>().color = Color.red; // เปลี่ยนสีให้รู้ว่าปิด
            }

            // ทำลายกระสุนทิ้งทันที เพื่อป้องกันไม่ให้กระสุนค้างและทะลุไปกดสวิตช์รัวๆ ในเฟรมเดียว
            Destroy(collision.gameObject);
        }
    }
}