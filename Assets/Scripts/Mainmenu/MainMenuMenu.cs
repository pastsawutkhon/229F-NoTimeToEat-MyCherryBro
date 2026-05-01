using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // โหลดด่านด้วยตัวเลข (อัปเดตใหม่ ให้ล้างคะแนนตอนกลับเมนู)
    public void LoadSceneByIndex(int sceneIndex)
    {
        // ถ้าเป็นการโหลดหน้า MainMenu (ด่าน 0) ให้รีเซ็ตเหรียญเป็น 0
        if (sceneIndex == 0 && ScoreManager.instance != null)
        {
            ScoreManager.instance.coinScore = 0;
        }

        SceneManager.LoadScene(sceneIndex);
    }

    // โหลดด่านด้วยชื่อ
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ออกจากเกม
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ออกจากเกมแล้ว!");
    }
}