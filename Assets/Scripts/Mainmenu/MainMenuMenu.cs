using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    // 1. ฟังก์ชันโหลดด่านด้วย "ตัวเลข" (ใช้กับปุ่ม Play หรือ Back)
    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    // 2. ฟังก์ชันโหลดด่านด้วย "ชื่อด่าน" (ใช้กับปุ่มเข้าหน้า Credit)
    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 3. ฟังก์ชันออกเกม (ใช้กับปุ่ม Quit)
    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ออกจากเกมแล้ว!");
    }
}