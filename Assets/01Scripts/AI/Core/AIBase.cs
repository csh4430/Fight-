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
    protected IHittable _baseHit;
    private bool pos = false, neg = false;

    private void Awake()
    {
        currentState = _currentState as AIState;
        _transition = GetComponent<AITransition>();
        _baseHit = transform.parent.GetComponent<IHittable>();
    }

    private void Update()
    {
        if (_baseHit.IsDead)
            return;


        if (currentState.IsOr)
        {
            if (pos || neg)
                MoveNextState();
        }
        else
        {
            if (pos && neg)
                MoveNextState();
        }
        
        pos = neg = false;

        CurrentState.OnStateAction?.Invoke();        
        if (_transition.ConditionDict.ContainsKey(currentState))
        {
            foreach (var conditions in _transition.ConditionDict[CurrentState])
            {
                if (!conditions.CheckCondition())
                {
                    return;
                }
            }
            pos = true;
        }
        else
            pos = true;
        if (_transition.NegaConditionDict.ContainsKey(currentState))
        {
            foreach (var conditions in _transition.NegaConditionDict[CurrentState])
            {
                if (conditions.CheckCondition())
                {
                    return;
                }
            }
            neg = true;
        }
        else
            neg = true;
    }

    public void MoveNextState()
    {
        CurrentState = _transition.TransitionDict[CurrentState];
        Debug.Log(currentState);
    }
}
