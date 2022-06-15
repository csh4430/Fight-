using UnityEngine;

public interface IHittable
{
    public bool IsDead { get; }
    public void DieAgent();
    public void DamageAgent(float damage, GameObject attacker);

}
