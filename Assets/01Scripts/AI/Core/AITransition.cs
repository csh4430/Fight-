using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public Dictionary<AIState, AIState> TransitionDict = new Dictionary<AIState, AIState>();

    public Dictionary<AIState, AICondition> ConditionDict = new Dictionary<AIState, AICondition>();
}
