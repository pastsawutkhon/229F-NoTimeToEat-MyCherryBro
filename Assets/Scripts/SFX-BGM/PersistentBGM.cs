using UnityEngine;

public class PersistentBGM : MonoBehaviour
{
    public static PersistentBGM instance;

    void Awake()
    {
        // ตรวจสอบว่ามี Background Music ตัวนี้อยู่ในเกมหรือยัง
        if (instance == null)
        {
            // ถ้ายังไม่มี ให้ตัวมันเองกลายเป็นต้นแบบ
            instance = this;

            // สั่ง Unity ว่า "ห้ามทำลาย Object นี้เวลาโหลดซีนใหม่"
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // ถ้ามีต้นแบบอยู่แล้ว (เช่น กลับมาซีนแรกอีกครั้ง) ให้ทำลายตัวที่เกิดใหม่ทิ้ง
            // เพื่อป้องกันไม่ให้เสียงเพลงเล่นซ้อนกัน 2 เพลง
            Destroy(gameObject);
        }
    }
}