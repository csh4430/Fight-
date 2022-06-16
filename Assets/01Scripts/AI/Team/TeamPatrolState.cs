using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamPatrolState : MonoBehaviour, AIState
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
    private List<UnityEngine.Object> _posCondition = new List<UnityEngine.Object>();
    private List<AICondition> posCondition = new List<AICondition>();
    public List<AICondition> PositiveCondition { get => posCondition; set { posCondition = value; } }

    [SerializeField]
    [RequireInterface(typeof(AICondition))]
    private List<UnityEngine.Object> _negaCondition = new List<UnityEngine.Object>();
    private List<AICondition> negaCondition = new List<AICondition>();
    public List<AICondition> NegativeCondition { get => negaCondition; set { negaCondition = value; } }

    [field: SerializeField]
    public bool IsOr { get; set; } = false;

    private AgentMove _move;
    private AITransition _transition;

    private void Awake()
    {
        nextState = _nextState as AIState;

        basePos = transform.parent;
        _move = basePos.GetComponent<AgentMove>();
        _transition = GetComponent<AITransition>();
        _transition.TransitionDict.Add(this, NextState);
        foreach (var item in _posCondition)
        {
            posCondition.Add(item as AICondition);
            _transition.AddCondition(this, item as AICondition);
        }
        foreach (var item in _negaCondition)
        {
            negaCondition.Add(item as AICondition);
            _transition.AddNegaCondition(this, item as AICondition);
        }
        OnStateAction += () =>
        {
            _move.OnWalkEvent?.Invoke(Vector3.forward);
            basePos.LookAt(TargetPos);
        };
    }
}
