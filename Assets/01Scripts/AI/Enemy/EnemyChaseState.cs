using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : MonoBehaviour, AIState
{
    public Transform TargetPos = null;
    public Transform basePos;
    public Action OnStateAction { get; set; } = null;

    [SerializeField]
    [RequireInterface(typeof(AIState))]
    private UnityEngine.Object _nextState;
    private AIState nextState;
    public AIState NextState { get => nextState as AIState; set { nextState = value; } }

    [SerializeField]
    [RequireInterface(typeof(AICondition))]
    private UnityEngine.Object _posCondition;
    private AICondition posCondition;
    public AICondition PositiveCondition { get => posCondition; set { posCondition = value; } }

    [SerializeField]
    [RequireInterface(typeof(AICondition))]
    private UnityEngine.Object _negaCondition;
    private AICondition negaCondition;
    public AICondition NegativeCondition { get => negaCondition; set { negaCondition = value; } }

    private AgentMove _move;
    private AITransition _transition;

    private void Awake()
    {
        nextState = _nextState as AIState;
        posCondition = _posCondition as AICondition;
        negaCondition = _negaCondition as AICondition;
        basePos = transform.parent;
        _move = basePos.GetComponent<AgentMove>();
        _transition = GetComponent<AITransition>();
        _transition.TransitionDict.Add(this, NextState);
        _transition.ConditionDict.Add(this, PositiveCondition == null ? NegativeCondition : PositiveCondition);
        OnStateAction += () =>
        {
            _move.OnWalkEvent?.Invoke(Vector3.forward);
            basePos.LookAt(TargetPos);
        };
    }
}