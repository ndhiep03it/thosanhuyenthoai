using UnityEngine;

public class HeavyPunchAbility : BossAbility
{
    public int punchDamage;
    public float punchRange;
    

    void Start()
    {
        abilityName = "Heavy Punch";
        cooldownTime = 5f;  // Thời gian hồi chiêu
        punchDamage = 50;  // Sức mạnh của đòn tấn công
        punchRange = 2f;    // Phạm vi tấn công
    }

    public override void ActivateAbility()
    {
        if (CanActivate())
        {
            
            if (GameManager.Singleton.hpao <= 0)
            {

            }
            else
            {

                // Thực hiện đòn tấn công mạnh
                BossAI.Singleton.Attack();
                Debug.Log("Boss sử dụng cú đấm nặng nề! Hư hại: " + punchDamage);
                PlayerController.Singleton.Takedame(punchDamage);
                SoundManager.Instance.PlaySound(SoundManager.SoundEffect.Hit);
                //BossAI.Singleton.moveSpeed = 0;
            }

            PerformSpecialEffect();  // Thực hiện hiệu ứng đặc biệt của chiêu thức

            OnAbilityUsed();
        }
        else
        {
            Debug.Log("Kĩ năng đang trong thời gian hồi chiêu.");
        }
    }

    public override void PerformSpecialEffect()
    {
        // Tạo hiệu ứng đặc biệt cho đòn tấn công mạnh, ví dụ: làm cho mặt đất rung chuyển
        Debug.Log("Mặt đất rung chuyển khi Boss đấm!");
    }
}
