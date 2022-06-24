using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Agent))]
[RequireComponent(typeof(AgentAnimation))]
public class AgentAttack : MonoBehaviour
{
    public bool IsAttacking = false;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private Transform centerPos;
    public LayerMask enemyLayer;
    private Agent _base;
    private AgentAnimation _anim;
    private AgentProjectileLauncher _launcher;

    public Action<int> OnAttackEvent;

    private void Awake()
    {
        _base = GetComponent<Agent>();
        _anim = GetComponent<AgentAnimation>();
        TryGetComponent<AgentProjectileLauncher>(out _launcher);
        OnAttackEvent += (type) =>
        {
            if (IsAttacking)
                return;
            IsAttacking = true;
            _anim.PlayAttackAnimation(type);
        };

    }
    public void AttackAgent()
    {
        Collider[] victims = Physics.OverlapSphere(centerPos.position + centerPos.forward * attackRange, attackRange, enemyLayer);
        if(_launcher != null)
        {
            Collider target = victims.OrderBy(x => Vector3.Distance(transform.position, x.transform.position)).FirstOrDefault();
            if (target != null)
            {
                _launcher.LaunchProjectile(centerPos, target.transform);
                IHittable hit = target.GetComponent<IHittable>();
                hit?.DamageAgent(_base.Attack, _base.gameObject);
                Debug.Log($"Victim : {target.name}");
            }
        }
        else
        {
            foreach (Collider victim in victims)
            {
                IHittable hit = victim.GetComponent<IHittable>();
                hit?.DamageAgent(_base.Attack, gameObject);
                Debug.Log($"Victim : {victim.name}");
            }
        }
    }

    public void StopAttack()
    {
        IsAttacking = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(centerPos.position + centerPos.forward * attackRange, attackRange);
        Gizmos.color = Color.white;
    }
}
