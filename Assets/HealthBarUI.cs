using UnityEngine;
using UnityEngine.UI; // ต้องใช้สำหรับคลาส Slider

public class HealthBarUI : MonoBehaviour
{
    // อ้างอิงถึง Component Slider บนแถบเลือด
    public Slider healthSlider;

    // อ้างอิงถึงสคริปต์ HealthSystem ของตัวละคร
    private HealthSystem healthSystem;

    void Start()
    {
        // 1. ค้นหาสคริปต์ HealthSystem บน Parent
        healthSystem = GetComponentInParent<HealthSystem>();

        if (healthSystem == null)
        {
            Debug.LogError("HealthSystem not found on Parent or self. Cannot link Health Bar.");
            return;
        }

        // 2. ตั้งค่า Max Value ของ Slider ให้เท่ากับ Max Health
        healthSlider.maxValue = healthSystem.maxHealth;
        
        // 3. เรียกใช้ฟังก์ชันอัปเดตครั้งแรก
        UpdateHealthBar();
    }

    // ฟังก์ชันนี้จะถูกเรียกซ้ำๆ เพื่อตรวจสอบและอัปเดตแถบเลือด
    void Update()
    {
        UpdateHealthBar();
    }

    public void UpdateHealthBar()
    {
        if (healthSystem != null && healthSlider != null)
        {
            // กำหนดค่า Value ของ Slider ให้เท่ากับ HP ปัจจุบัน
            healthSlider.value = healthSystem.currentHealth;
        }
    }
}