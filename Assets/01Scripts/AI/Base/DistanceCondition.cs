using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCondition : MonoBehaviour, AICondition
{
    [SerializeField]
    private float _distance;
    [SerializeField]
    Transform _targetPos = null;
    private Transform basePos = null;
    public bool CheckCondition()
    {
        bool temp = false;
        Vector3 dir = _targetPos.position - basePos.position;
        if (dir.sqrMagnitude < _distance * _distance)
        {
            temp = true;
        }
            return temp;
    }

    private void Awake()
    {
        basePos = transform.parent;
    }

    private void Update()
    {
        if (_targetPos == null)
            _targetPos = TargetSetter.SetTarget(basePos, _distance, LayerMask.GetMask("Player"));
    }
}
