using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAgent : Agent, IHittable, IHealable
{
    [field:SerializeField]
    public bool IsDead { get; private set; } = false;
    public List<ChaseState> TeamChase = new List<ChaseState>();
    private List<Transform> TeamChasePos = new List<Transform>();
                                                                    
    public void DieAgent()
    {
        IsDead = true;
        _controller.enabled = false;
        GameManager.Instance.GameFail();
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
        for (int i = 0; i < TeamChase.Count; i++)
        {
            TeamChasePos.Add(teampos.GetChild(i));
            TeamChase[i].TargetPos = TeamChasePos[i];
        }
    }

    private void Update()
    {
        for (int i = 0; i < TeamChase.Count; i++)
        {
            Vector3 target =  TeamChasePos[i].position;
            target.y = TeamChase[i]._basePos.position.y;
            TeamChasePos[i].position = target;
        }
    }

    public void HealAgent(float value, GameObject healer)
    {
        OnHealed?.Invoke(value, healer);
    }
    public void OnTriggerEnter(Collider collider)
    {
        foreach(Collider col in GameManager.Instance._mapCol)
        {
            if(collider == col)
            {
                if(GameManager.Instance._mapCol.IndexOf(col) == GameManager.Instance._mapCol.Count - 1)
                {
                    GameManager.Instance.GameClear();
                    return;
                }
                GameManager.Instance.SummonEnemy(GameManager.Instance._mapCol.IndexOf(col));
            }
        }
    }
}
