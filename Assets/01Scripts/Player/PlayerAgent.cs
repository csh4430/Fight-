using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : Agent, IHittable
{
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
        Hp -= damage;
        OnDamaged?.Invoke(damage, attacker);
        if (Hp <= 0)
        {
            DieAgent();
        }
    }   
}
