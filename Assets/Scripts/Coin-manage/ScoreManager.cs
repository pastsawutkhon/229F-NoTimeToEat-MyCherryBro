using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    // สร้างตัวแทนของ ScoreManager ให้สคริปต์อื่นเรียกใช้ได้ง่ายๆ
    public static ScoreManager instance;

    [Header("คะแนนเหรียญปัจจุบัน")]
    public int coinScore = 0;

    void Awake()
    {
        // กำหนดค่าเริ่มต้นให้ instance
        if (instance == null)
        {
            instance = this;
        }
    }

    // ฟังก์ชันสำหรับรับคะแนนเพิ่ม
    public void AddCoin(int amount)
    {
        coinScore += amount;
        Debug.Log("เก็บเหรียญได้! ตอนนี้มีเหรียญ: " + coinScore); // แสดงผลใน Console
    }
}