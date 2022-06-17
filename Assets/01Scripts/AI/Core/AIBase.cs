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

    private void Awake()
    {
        _basePos = transform.parent;
        _currentState = transform.Find("Idle").GetComponent<AIState>();
    }

    private void Update()
    {
        _currentState.OnStateAction?.Invoke();
        foreach(var transition in _currentState.Transition)
        {
            foreach (var condition in transition.PositiveConditions)
            {
                if (!condition.CheckCondition())
                {
                    return;
                }
            }

            foreach (var condition in transition.NegativeConditions)
            {
                if (condition.CheckCondition())
                {
                    return;
                }
            }
            _currentTransition = transition;
            MoveNextState();
        }
    }

    public void MoveNextState()
    {
        _currentState = _currentTransition.NextState;
    }
}
