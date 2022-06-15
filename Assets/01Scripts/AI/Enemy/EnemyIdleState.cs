using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : MonoBehaviour, AIState    
{
    public Action OnStateAction { get; set; } = null;
    
    [SerializeField]
    [RequireInterface(typeof(AITransition))]
    private UnityEngine.Object _complexExample;
    public AITransition NextTransition { get => _complexExample as AITransition; set { NextTransition = value; } }
    private AgentMove _move;

    private void Awake()
    {
        _move = transform.parent.GetComponent<AgentMove>();
        OnStateAction += () =>
        {
            _move.MoveAgent(Vector3.zero);
        };
    }
}
