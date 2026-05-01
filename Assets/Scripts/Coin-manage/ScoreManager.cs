using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [Header("มูลค่าของเหรียญนี้")]
    public int coinValue = 1; // ตั้งค่าได้ว่าเหรียญนี้ได้กี่คะแนน (เช่น เหรียญทอง=1, เหรียญเพชร=5)

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เช็กว่าคนที่มาชนคือ "Player" หรือเปล่า
        if (collision.CompareTag("Player"))
        {
            // ส่งคะแนนไปเพิ่มใน ScoreManager
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddCoin(coinValue);
            }

            // ทำลายเหรียญนี้ทิ้งออกจากฉาก
            Destroy(gameObject);
        }
    }
}