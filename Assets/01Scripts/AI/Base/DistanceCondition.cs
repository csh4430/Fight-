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
    public override bool CheckCondition()
    {
        bool temp = false;
        Vector3 dir = _targetPos.position - BasePos.position;
        if (dir.sqrMagnitude < _distance * _distance)
        {
            temp = true;
        }
            return temp;
    }

    private void Update()
    {
        if (_targetPos == null)
            _targetPos = TargetSetter.SetTarget(BasePos, _distance, LayerMask.GetMask("Player"));
    }
}
