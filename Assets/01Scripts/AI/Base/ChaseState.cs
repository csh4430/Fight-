using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : AIState
{
    public override Action OnStateAction { get; set; }
    [field: SerializeField]
    public override List<AITransition> Transition { get; set; } = new List<AITransition>();

    public Transform _basePos = null;
    private AgentMove _move = null;
    public Transform TargetPos = null;
    public AIBase _ai;

    private void Awake()
    {
        _move = _basePos.GetComponent<AgentMove>();
        _ai = _basePos.Find("AI").GetComponent<AIBase>();
        foreach (AITransition transition in Transition)
        {
            transition.StartState = this;
        }
        OnStateAction += () =>
        {
            if (TargetPos == null) return;
            _basePos.LookAt(TargetPos);
            Vector3 dir = TargetPos.position - _basePos.position;
            if (dir.sqrMagnitude < 4f)
                _move.OnWalkEvent?.Invoke(Vector3.forward);
            else
                _move.OnRunEvent?.Invoke(Vector3.forward);
        };
    }

    private void Update()
    {
        TargetPos = _ai.TargetPos;
    }
}
