using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public override Action OnStateAction { get; set; }
    [field: SerializeField]
    public override List<AITransition> Transition { get; set; } = new List<AITransition>();

    Transform _basePos = null;
    private AgentMove _move = null;

    private void Awake()
    {
        _basePos = transform.parent;
        _move = _basePos.GetComponent<AgentMove>();
        foreach (AITransition transition in Transition)
        {
            transition.StartState = this;
        }
        OnStateAction += () =>
        {
            _move.StopMove();
        };
    }
}
