using UnityEngine;
using TMPro; // 1. ต้องเพิ่มบรรทัดนี้ เพื่อให้ใช้คำสั่ง TextMeshPro ได้

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [Header("คะแนนเหรียญปัจจุบัน")]
    public int coinScore = 0;

    [Header("UI แสดงตัวเลขเหรียญ")]
    public TextMeshProUGUI coinText; // 2. สร้างตัวแปรมารับค่า Text UI

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI(); // 3. อัปเดตข้อความบนจอตั้งแต่เริ่มเกม
    }

    public void AddCoin(int amount)
    {
        coinScore += amount;
        UpdateScoreUI(); // 4. อัปเดตข้อความบนจอทุกครั้งที่เก็บเหรียญ
    }

    // ฟังก์ชันสำหรับเปลี่ยนข้อความบนจอ
    private void UpdateScoreUI()
    {
        if (coinText != null)
        {
            // เปลี่ยนจาก "Coins: " + coinScore ให้เหลือแค่ตัวเลข
            coinText.text = coinScore.ToString();
        }
    }
}