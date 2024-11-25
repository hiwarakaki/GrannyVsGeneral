using UnityEngine;

public class UserProfile : MonoBehaviour
{
    public static UserProfile Instance; // Singleton Instance

    public string userName; // ชื่อผู้ใช้
    public Sprite profilePicture; // รูปโปรไฟล์

    void Awake()
    {
        // สร้าง Singleton Instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // ไม่ลบเมื่อโหลดซีนใหม่
            Debug.Log("UserProfile Instance created.");
        }
        else
        {
            Destroy(gameObject); // ถ้ามี Instance แล้ว ให้ลบตัวปัจจุบัน
        }
    }

    // ฟังก์ชันสำหรับตั้งค่าโปรไฟล์
    public void SetUserProfile(string name, Sprite picture)
    {
        userName = name;
        profilePicture = picture;
    }
}