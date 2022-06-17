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
    public LayerMask layerMask;
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
        Transform target = TargetSetter.SetTarget(BasePos, _distance, layerMask);
        if (target != null)
            _targetPos = target;
    }
}
