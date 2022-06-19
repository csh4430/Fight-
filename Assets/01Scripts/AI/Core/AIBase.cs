using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    [SerializeField]
    private AIState _currentState;
    [SerializeField]
    private AITransition _currentTransition;
    Transform _basePos = null;

    private bool pos = false, neg = false;
    public Transform TargetPos;
    public LayerMask layerMask;
    [SerializeField]
    private float _findDistance;

    private void Awake()
    {
        _basePos = transform.parent;
        _currentState = transform.Find("State").Find("Idle").GetComponent<AIState>();
    }
    private void Update()
    {
        Transform target = TargetSetter.SetTarget(_basePos, _findDistance, layerMask);
        if (target != null)
            TargetPos = target;

        _currentState.OnStateAction?.Invoke();

        foreach(var transition in _currentState.Transition)
        {
            pos = neg = true;
            if(transition.PositiveConditions.Count == 0)
                pos = false;
            foreach (var condition in transition.PositiveConditions)
            {
                if (!condition.CheckCondition())
                {
                    pos = false;
                }
            }

            if (transition.NegativeConditions.Count == 0)
                neg = false;
            foreach (var condition in transition.NegativeConditions)
            {
                if (condition.CheckCondition())
                {
                    neg = false;
                }
            }
            _currentTransition = transition;
            if (transition.IsOr)
            {
                if(neg || pos)
                    MoveNextState();
            }
            else
            {
                if (neg && pos)
                    MoveNextState();
            }
        }
    }

    public void MoveNextState()
    {
        AIState originState = _currentState;
        _currentState = _currentTransition.NextState;
        Debug.Log($"{_basePos.name} Changed State To {_currentState.ToString()} From {originState.ToString()}");
    }
}
