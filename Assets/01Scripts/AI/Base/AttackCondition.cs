using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCondition : AICondition
{
    [SerializeField] private AgentAttack _atk;
    public override bool CheckCondition()
    {
        return _atk.IsAttacking;
    }
}
