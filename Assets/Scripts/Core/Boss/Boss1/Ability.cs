using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public string abilityName;
    public float cooldownTime;
    private float lastUsedTime;

    // Abstract method to define the effect of the ability
    public abstract void ActivateAbility();

    public bool CanActivate()
    {
        return Time.time - lastUsedTime >= cooldownTime;
    }

    protected void OnAbilityUsed()
    {
        lastUsedTime = Time.time;
        
        //BossAI.Singleton.moveSpeed = 2 ;
    }
    
}
