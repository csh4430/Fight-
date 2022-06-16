using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCondition : MonoBehaviour, AICondition
{
    [SerializeField]
    private float _distance;
    [SerializeField]
    private bool _isNegative = false;
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
            return _isNegative ? !temp : temp;
    }

    private void Awake()
    {
        basePos = transform.parent;
    }
}
