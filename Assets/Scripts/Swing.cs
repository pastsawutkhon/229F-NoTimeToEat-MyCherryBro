using UnityEngine;

public class Swing : MonoBehaviour
{
    [Header("Swing Settings")]
    public float maxAngle = 45f;    // องศาที่จะให้แกว่งไปไกลสุด (ซ้ายและขวา)
    public float swingSpeed = 2f;   // ความเร็วในการแกว่งรอบนั้นๆ

    private HingeJoint2D _joint;
    private JointMotor2D _motor;

    void Start()
    {
        _joint = GetComponent<HingeJoint2D>();
        
        // เปิดใช้ Motor
        _joint.useMotor = true;
        _motor = _joint.motor;
        
        // ใส่แรงบิดมอเตอร์ให้สูงๆ ไว้ก่อน (เช่น 1000) เพื่อให้มันมีแรงดึงกลับตามสูตรเสมอ
        _motor.maxMotorTorque = 1000f; 
    }

    void FixedUpdate()
    {
        // ใช้สมการคลื่น Cosine ในการคำนวณความเร็วมอเตอร์
        // คลื่นจะทำหน้าที่ "ชะลอความเร็ว" จนเป็น 0 ตอนที่ลูกตุ้มถึงจุด maxAngle พอดี
        // และค่อยๆ เพิ่มแรงดึงกลับ (ติดลบ) อย่างนุ่มนวล
        float smoothSpeed = maxAngle * swingSpeed * Mathf.Cos(Time.time * swingSpeed);

        // จ่ายความเร็วที่คำนวณได้เข้าสู่ Motor
        _motor.motorSpeed = smoothSpeed;
        _joint.motor = _motor;
    }
}