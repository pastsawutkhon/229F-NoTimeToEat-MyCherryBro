using UnityEngine;
using UnityEngine.SceneManagement;

public class Coin : MonoBehaviour
{
    [Header("มูลค่าของเหรียญนี้")]
    public int coinValue = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddCoin(coinValue);
            }

            // [เพิ่ม] เล่นเสียงเก็บเหรียญ
            if (SoundManager.instance != null)
            {
                SoundManager.instance.PlaySound(SoundManager.instance.coinSound);
            }

            Destroy(gameObject);
        }
    }
}