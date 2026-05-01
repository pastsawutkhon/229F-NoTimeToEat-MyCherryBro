using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinUIUpdater : MonoBehaviour
{
    private TextMeshProUGUI _coinText;
    private Canvas _parentCanvas;

    void Start()
    {
        _coinText = GetComponent<TextMeshProUGUI>();
        _parentCanvas = GetComponentInParent<Canvas>();

        // เชื่อมต่อกับ ScoreManager[cite: 8, 10]
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.coinText = _coinText;
            _coinText.text = ScoreManager.instance.coinScore.ToString();
        }
    }

    void Update()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // ซ่อน UI เหรียญในหน้าเมนู หน้าจบ และหน้าเครดิต
        if (currentScene == "MainMenu" || currentScene == "EndSence" || currentScene == "EndCredit")
        {
            if (_parentCanvas != null) _parentCanvas.enabled = false;
        }
        else
        {
            if (_parentCanvas != null) _parentCanvas.enabled = true;

            // อัปเดตตัวเลขเหรียญตลอดเวลา[cite: 8, 10]
            if (ScoreManager.instance != null && _coinText != null)
            {
                _coinText.text = ScoreManager.instance.coinScore.ToString();
            }
        }
    }
}