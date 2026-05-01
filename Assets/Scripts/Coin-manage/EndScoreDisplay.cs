using UnityEngine;
using TMPro;

public class EndScoreDisplay : MonoBehaviour
{
    [Header("ลากข้อความ Score Text มาใส่ช่องนี้")]
    public TextMeshProUGUI scoreText;

    void Start()
    {
        // เช็กว่ามี ScoreManager ข้ามด่านมาด้วยใช่ไหม
        if (ScoreManager.instance != null)
        {
            // ดึงตัวเลขเหรียญมาต่อท้ายข้อความ
            scoreText.text = "Total Coins: " + ScoreManager.instance.coinScore.ToString();
        }
        else
        {
            // อันนี้เผื่อคุณกด Play เทสต์หน้านี้ตรงๆ (ไม่ได้เล่นจากด่าน 1) มันจะได้ไม่ Error
            scoreText.text = "Congratulations!";
        }
    }
}