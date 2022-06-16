using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    [SerializeField]
    [RequireInterface(typeof(AIState))]
    private UnityEngine.Object _currentState;
    private AIState currentState;
    protected AIState CurrentState { get => currentState; set { currentState = value; } }
    protected AITransition _transition;

    private void Awake()
    {
        currentState = _currentState as AIState;
        _transition = GetComponent<AITransition>();
    }

    private void Update()
    {
        if (_transition.ConditionDict[CurrentState].CheckCondition())
        {
            MoveNextState();
        }
        CurrentState.OnStateAction?.Invoke();
    }

    public void MoveNextState()
    {
        CurrentState = _transition.TransitionDict[CurrentState];
    }
}
