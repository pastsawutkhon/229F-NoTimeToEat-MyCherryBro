using UnityEngine;
using UnityEngine.SceneManagement; // สำคัญมาก ต้องมีบรรทัดนี้เพื่อใช้คำสั่งเปลี่ยนด่าน

public class MainMenuMenu : MonoBehaviour
{
    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่ม Play
    public void PlayGame()
    {
        // สั่งให้โหลดด่านถัดไป (ด่านที่ 1 ของเราจะอยู่ลำดับที่ 1 ใน Build Settings)
        SceneManager.LoadScene(1);
    }

    // ฟังก์ชันนี้จะถูกเรียกเมื่อกดปุ่ม Quit
    public void QuitGame()
    {
        // สั่งปิดเกม
        Application.Quit();

        // (บรรทัดนี้ใส่ไว้เพื่อให้รู้ว่าปุ่มทำงานตอนเราเทสต์ใน Unity Editor เพราะ Application.Quit จะเห็นผลแค่ตอนเซฟเป็นตัวเกมเต็มๆ เท่านั้น)
        Debug.Log("กดปุ่มออกจากเกมแล้ว!");
    }
}