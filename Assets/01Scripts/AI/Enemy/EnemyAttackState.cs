using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAttackState : MonoBehaviour, AIState
{
    public Transform basePos;
    
    public Action OnStateAction { get; set; } = null;

    [SerializeField]
    [RequireInterface(typeof(AIState))]
    private UnityEngine.Object _nextState;
    private AIState nextState;
    public AIState NextState { get => nextState; set { nextState = value; } }

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
    private AgentAttack _attack;
    private AITransition _transition;

    private void Awake()
    {
        nextState = _nextState as AIState;
        
        basePos = transform.parent;
        _attack = basePos.GetComponent<AgentAttack>();
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
            _attack.OnAttackEvent?.Invoke(Random.Range(1, 3));
        };
    }
}