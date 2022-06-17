using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AttackState : AIState
{
    public override Action OnStateAction { get; set; }
    [field: SerializeField]
    public override List<AITransition> Transition { get; set; } = new List<AITransition>();

    public Transform _basePos = null;
    private AgentAttack _attack = null;

    private void Awake()
    {
        _attack = _basePos.GetComponent<AgentAttack>();
        foreach (AITransition transition in Transition)
        {
            transition.StartState = this;
        }
        OnStateAction += () =>
        {
            _attack.OnAttackEvent?.Invoke(Random.Range(1, 3));
        };
    }
}
