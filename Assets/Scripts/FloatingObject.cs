using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    [Header("การตั้งค่าการลอย")]
    public float floatSpeed = 2f;
    public float floatAmplitude = 0.5f;

    // ตัวแปรเก็บค่าหน่วงจังหวะแบบสุ่ม
    private float _timeOffset;
    private Vector3 _startPos;

    void Start()
    {
        _startPos = transform.position;

        // สุ่มค่าตั้งแต่ 0 ถึง 100 เพื่อให้แต่ละ Object มีจุดเริ่มต้นของคลื่นไม่เหมือนกัน
        _timeOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        // นำค่า _timeOffset มาบวกเพิ่มเข้าไปในสมการ
        float newY = Mathf.Sin((Time.time * floatSpeed) + _timeOffset) * floatAmplitude;

        transform.position = _startPos + new Vector3(0, newY, 0);
    }
}