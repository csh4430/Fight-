using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Agent))]
public class AgentAttack : MonoBehaviour
{
    public bool IsAttacking = false;
    [SerializeField]
    private float attackRange = 1f;
    [SerializeField]
    private Transform centerPos;
    public LayerMask enemyLayer;
    private Agent _base;
    private void Awake()
    {
        _base = GetComponent<Agent>();
    }
    public void AttackAgent()
    {
        Collider[] victims = Physics.OverlapSphere(centerPos.position + centerPos.forward * attackRange, attackRange, enemyLayer);
        foreach(Collider victim in victims)
        {
            IHittable hit = victim.GetComponent<IHittable>();
            hit?.DamageAgent(_base.Attack, gameObject);
            Debug.Log(victim.name);
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
