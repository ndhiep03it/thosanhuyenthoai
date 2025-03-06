using UnityEngine;

public class HealAbility : BossAbility
{
    public int healAmount;
    public GameObject FxHoimau;

    void Start()
    {
        abilityName = "Heal";
        cooldownTime = 10f;  // Thời gian hồi chiêu
        healAmount = 50;    // Lượng máu hồi phục
    }

    public override void ActivateAbility()
    {
        if (CanActivate())
        {
            // Hồi máu cho Boss
            Debug.Log("Boss chữa lành cho " + healAmount + " HP.");
            Instantiate(FxHoimau, transform.position, Quaternion.identity);

            PerformSpecialEffect();  // Thực hiện hiệu ứng hồi máu

            OnAbilityUsed();
            BossAI.Singleton.health += healAmount;
           // BossAI.Singleton.moveSpeed = 0;
        }
        else
        {
            Debug.Log("Kĩ năng đang trong thời gian hồi chiêu.");
        }
    }

    public override void PerformSpecialEffect()
    {
        // Tạo hiệu ứng hồi máu, ví dụ: ánh sáng phát ra từ Boss
        Debug.Log("Một luồng hào quang rực sáng bao quanh Boss khi họ hồi phục.");
    }
}
