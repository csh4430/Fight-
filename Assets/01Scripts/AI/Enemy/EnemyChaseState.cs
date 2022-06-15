using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : MonoBehaviour, AIState
{
    public Transform TargetPos = null;
    public Action OnStateAction { get; set; } = null;

    [SerializeField]
    [RequireInterface(typeof(AITransition))]
    private UnityEngine.Object _complexExample;
    public AITransition NextTransition { get => _complexExample as AITransition; set { NextTransition = value; } }
    private AgentMove _move;

    private void Awake()
    {
        Transform basePos = transform.parent;
        _move = basePos.GetComponent<AgentMove>();
        OnStateAction += () =>
        {
            _move.MoveAgent(TargetPos.position - basePos.position);
        };
    }
}