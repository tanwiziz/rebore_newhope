using UnityEngine;
using UnityEngine.Events; // สำคัญสำหรับเหตุการณ์เมื่อค่า HP เปลี่ยน

public class HealthSystem : MonoBehaviour
{
    // กำหนดค่า HP สูงสุดและ HP ปัจจุบัน (ปรับใน Inspector ได้)
    [Header("Health Stats")]
    public float maxHealth = 100f;
    public float currentHealth;

    // เหตุการณ์ (Event) ที่จะถูกเรียกเมื่อตัวละครตาย
    public UnityEvent OnDeath; 

    void Start()
    {
        // กำหนดให้ HP เริ่มต้นเท่ากับ HP สูงสุด
        currentHealth = maxHealth;
    }

    // ฟังก์ชันสำหรับรับความเสียหาย
    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0) return; // ถ้าตายอยู่แล้ว ไม่ต้องรับความเสียหายอีก

        // ลด HP
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " took " + damageAmount + " damage. Current HP: " + currentHealth);

        // ตรวจสอบว่า HP เหลือ 0 หรือต่ำกว่าหรือไม่
        if (currentHealth <= 0)
        {
            Die();
        }

    }

    // ฟังก์ชันเมื่อตัวละครตาย
    private void Die()
    {
        currentHealth = 0;
        Debug.Log(gameObject.name + " has died!");

        // หยุดการเคลื่อนไหวและการหมุนของตัวละคร
        // ปิดการใช้งาน CharacterController
        PlayerController playerController = GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.enabled = false;
        }

        // เรียก Event OnDeath (ใช้สำหรับทำ UI หรืออนิเมชั่นตาย)
        OnDeath.Invoke();

        // ตัวอย่าง: ถ้าต้องการให้วัตถุหายไปเมื่อตาย
        // Destroy(gameObject, 3f); 
    }
    void Update() {

    // === 4. การทดสอบรับความเสียหาย (ใหม่) ===
    if (Input.GetKeyDown(KeyCode.K)) // กดปุ่ม K เพื่อรับความเสียหาย
    {
        HealthSystem health = GetComponent<HealthSystem>();
        if (health != null)
        {
            health.TakeDamage(20f); // รับความเสียหาย 20 หน่วย
        }
    }
    
    // ... โค้ดการหมุนกล้องและตัวผู้เล่น (ส่วนที่ 3) ...
}
}