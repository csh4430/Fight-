using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamAgent : Agent, IHittable, IHealable
{
    [field:SerializeField]
    public bool IsDead { get; private set; } = false;

    public void DieAgent()
    {
        IsDead = true;
        _controller.enabled = false;
        OnDied?.Invoke();
    }

    public void DamageAgent(float damage, GameObject attacker = null)
    {
        if (IsDead) return;
        Hp -= damage - damage * Defence; ;
        OnDamaged?.Invoke(damage, attacker);
        if (Hp <= 0)
        {
            DieAgent();
        }
    }

    public void HealAgent(float value, GameObject healer)
    {
        OnHealed?.Invoke(value, healer);
    }
}
