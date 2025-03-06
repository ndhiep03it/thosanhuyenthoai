using UnityEngine;

public abstract class BossAbility : Ability
{
    public float abilityStrength; // Sức mạnh của chiêu thức boss

    public abstract void PerformSpecialEffect();
}
