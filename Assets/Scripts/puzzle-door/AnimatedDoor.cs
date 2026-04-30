using UnityEngine;

public class AnimatedDoor : MonoBehaviour
{
    private Animator _animator;

    void Start()
    {
        // ดึงคอมโพเนนต์ Animator ในตัวประตูมาใช้งาน
        _animator = GetComponent<Animator>();
    }

    // ฟังก์ชันนี้จะถูกเรียกตอนสวิตช์เปิด
    public void OpenDoor()
    {
        if (_animator != null)
        {
            // สั่งตั้งค่าตัวแปรใน Animator ให้ IsOpen เป็น true
            _animator.SetBool("IsOpen", true);
        }
    }

    // ฟังก์ชันนี้จะถูกเรียกตอนสวิตช์ปิด
    public void CloseDoor()
    {
        if (_animator != null)
        {
            // สั่งตั้งค่าตัวแปรใน Animator ให้ IsOpen เป็น false
            _animator.SetBool("IsOpen", false);
        }
    }
}