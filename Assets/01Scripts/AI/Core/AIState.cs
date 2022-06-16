using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AIState
{
    public Action OnStateAction { get; }
    public AIState NextState { get; }
    public AICondition PositiveCondition { get; }
    public AICondition NegativeCondition { get; }
}
