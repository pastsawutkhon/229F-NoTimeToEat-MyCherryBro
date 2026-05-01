using UnityEngine;

public class Swing : MonoBehaviour
{
    [Header("Swing Settings")]
    public float maxAngle = 45f;    // องศาที่จะให้แกว่งไปไกลสุด
    public float swingSpeed = 2f;   // ความเร็วในการแกว่ง

    private HingeJoint2D _joint;
    private JointMotor2D _motor;

    // สร้างตัวแปรมาจับเวลาของด่านนี้โดยเฉพาะ
    private float _timer = 0f;

    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();

        _joint.useMotor = true;
        _motor = _joint.motor;

        // ใส่แรงบิดมอเตอร์ให้สูงมากๆ (เช่น 10000) เพราะ FixedJoints ทำให้โซ่หนักขึ้นมาก
        _motor.maxMotorTorque = 10000f;
    }

    void FixedUpdate()
    {
        // ให้นับเวลาบวกเพิ่มไปเรื่อยๆ (เริ่มจาก 0 เสมอเมื่อเปิดด่านใหม่)
        _timer += Time.fixedDeltaTime;

        // ใช้ _timer แทน Time.time จังหวะจะได้เป๊ะทุกด่าน
        float smoothSpeed = maxAngle * swingSpeed * Mathf.Cos(_timer * swingSpeed);

        _motor.motorSpeed = smoothSpeed;
        _joint.motor = _motor;
    }
}