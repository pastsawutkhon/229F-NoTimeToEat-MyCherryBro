using UnityEngine;

public class FallOut : MonoBehaviour
{
    public GameObject _spawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = _spawnPoint.transform.position;
        }
    }
}
