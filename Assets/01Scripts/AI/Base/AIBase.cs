using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : MonoBehaviour
{
    [SerializeField, SerializeReference]
    protected AIState _currentState;

    private void Update()
    {
        _currentState?.OnStateAction?.Invoke();
    }

    public void MoveNextState()
    {
        
    }
}
