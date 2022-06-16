using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITransition : MonoBehaviour
{
    public Dictionary<AIState, AIState> TransitionDict = new Dictionary<AIState, AIState>();

    public Dictionary<AIState, List<AICondition>> ConditionDict = new Dictionary<AIState, List<AICondition>>();
    public Dictionary<AIState, List<AICondition>> NegaConditionDict = new Dictionary<AIState, List<AICondition>>();

    public void AddCondition(AIState aiState, AICondition aICondition)
    {
        List<AICondition> temp = new List<AICondition>();
        if (ConditionDict.ContainsKey(aiState))
        {
            temp = ConditionDict[aiState];
        }
        temp.Add(aICondition);
        ConditionDict.Add(aiState, temp);
    }

    public void AddNegaCondition(AIState aiState, AICondition aICondition)
    {
        List<AICondition> temp = new List<AICondition>();
        if (NegaConditionDict.ContainsKey(aiState))
        {
            temp = NegaConditionDict[aiState];
        }
        temp.Add(aICondition);
        NegaConditionDict.Add(aiState, temp);
    }
}
