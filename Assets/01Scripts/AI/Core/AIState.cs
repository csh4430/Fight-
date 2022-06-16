using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIState
{
    public Action OnStateAction { get; }
    public AIState NextState { get; }
    public List<AICondition> PositiveCondition { get; }
    public List<AICondition> NegativeCondition { get; }
    public bool IsOr { get; }
}
