using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface AITransition
{
    public AICondition PositiveCondition { get; set; }
    public AIState NextTransition { get; set; }
}
