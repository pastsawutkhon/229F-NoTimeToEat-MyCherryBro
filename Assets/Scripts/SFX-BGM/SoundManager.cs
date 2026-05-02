using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("ใส่ไฟล์เสียงลงในช่องเหล่านี้")]
    public AudioClip jumpSound;
    public AudioClip hitSound;
    public AudioClip dieSound;
    public AudioClip shootSound;
    public AudioClip coinSound;

    [Header("ตั้งค่าความดังเสียงเอฟเฟกต์ (0.0 ถึง 1.0)")]
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    private AudioSource _audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            _audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ฟังก์ชันสำหรับเรียกเล่นเสียง พร้อมคูณความดังเข้าไปด้วย
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _audioSource.PlayOneShot(clip, sfxVolume);
        }
    }

    // ฟังก์ชันนี้เตรียมไว้ให้ UI Slider เรียกใช้เวลาผู้เล่นเลื่อนปรับเสียง
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;
    }
}