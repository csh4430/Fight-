using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : Agent, IHittable
{
    [field:SerializeField]
    public bool IsDead { get; private set; } = false;
    public List<TeamAI> TeamAi = new List<TeamAI>();

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

    protected override void Awake()
    {
        base.Awake();
        Transform teampos = transform.Find("Positions");
        SetTeamPos(teampos);
    }

    private void SetTeamPos(Transform teampos)
    {
        for (int i = 0; i < TeamAi.Count; i++)
        {
            TeamAi[i].TargetPos = teampos.GetChild(i);
        }
    }
}
