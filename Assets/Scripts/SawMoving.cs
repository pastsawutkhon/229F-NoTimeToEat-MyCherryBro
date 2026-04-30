using UnityEngine;

public class SawMoving : MonoBehaviour
{
    private Vector2 _startPos;
    public float moveDistance = 1f;
    public float moveSpeed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance);
        transform.position = _startPos + (Vector2)transform.up * offset;
    }
}
