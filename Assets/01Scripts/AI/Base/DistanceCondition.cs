using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCondition : AICondition
{
    [SerializeField]
    private float _distance;
    [SerializeField]
    Transform _targetPos = null;
    public Transform BasePos = null;
    private AIBase _ai;
    public override bool CheckCondition()
    {
        bool temp = false;
        if (_targetPos == null) return false;
        Vector3 dir = _targetPos.position - BasePos.position;
        if (dir.sqrMagnitude < _distance * _distance)
        {
            temp = true;
        }
        return temp;
    }

    private void Awake()
    {
        _ai = BasePos.Find("AI").GetComponent<AIBase>();   
    }

    private void Update()
    {
        _targetPos = _ai.TargetPos;
    }
}
