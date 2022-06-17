using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{
    public override Action OnStateAction { get; set; }
    [field: SerializeField]
    public override List<AITransition> Transition { get; set; } = new List<AITransition>();

    public Transform BasePos = null;
    private AgentMove _move = null;

    private void Awake()
    {
        _move = BasePos.GetComponent<AgentMove>();
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
