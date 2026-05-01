using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUI : MonoBehaviour
{
    private PlayerHealth playerHealth;

    [Header("ลากรูปหัวใจทั้ง 4 ดวงบนจอมาใส่เรียงตามลำดับ")]
    public Image[] hearts;

    private Canvas _canvas;

    void Start()
    {
        _canvas = GetComponent<Canvas>();
        FindPlayerAndRefresh(); // บังคับหาผู้เล่นและรีเฟรช 1 ครั้งตอนเริ่ม
    }

    // ใช้ LateUpdate เพื่อให้แน่ใจว่ามันทำงานหลังจาก PlayerHealth รีเซ็ตเลือดเสร็จแล้ว
    void LateUpdate()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // 1. ตรวจสอบการซ่อน UI ในหน้าเมนูหรือหน้าจบ
        if (currentScene == "MainMenu" || currentScene == "EndSence" || currentScene == "EndCredit")
        {
            if (_canvas != null && _canvas.enabled) _canvas.enabled = false;
            return;
        }
        else
        {
            if (_canvas != null && !_canvas.enabled) _canvas.enabled = true;
        }

        // 2. ระบบตามล่าหา Player ถ้าหาไม่เจอให้พยายามหาใหม่เรื่อยๆ
        if (playerHealth == null)
        {
            FindPlayerAndRefresh();
            if (playerHealth == null) return;
        }

        // 3. แสดงผลหัวใจตามจำนวนเลือดปัจจุบัน
        UpdateHeartIcons();
    }

    private void FindPlayerAndRefresh()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<PlayerHealth>();
            UpdateHeartIcons(); // เมื่อเจอตัวแล้ว ให้รีเฟรชรูปหัวใจทันที
        }
    }

    private void UpdateHeartIcons()
    {
        if (playerHealth == null) return;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < playerHealth.currentHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}