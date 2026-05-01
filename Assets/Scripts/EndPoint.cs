using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // เช็กว่าสิ่งที่เดินเข้ามาชน มี Tag เป็น "Player" ใช่หรือไม่
        if (collision.CompareTag("Player"))
        {
            // หาว่าตอนนี้อยู่ด่านเลขอะไร แล้วบวก 1 เพื่อไปด่านถัดไป
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            // สั่งโหลดด่าน
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}