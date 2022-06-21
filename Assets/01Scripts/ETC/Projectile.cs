using System;
using System.Collections;
using System.Collections.Generic;
using ToolBox.Pools;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Action<Transform> OnSummoned = null;
    private Transform _targetPos = null;
    public Transform TargetPos { get => _targetPos; set => _targetPos = value; }

    private void Awake()
    {
        OnSummoned += (target) =>
        {
            transform.Translate(Vector3.forward * Time.deltaTime * 100);
            transform.LookAt(target);
        };
    }
    void Update()
    {
        OnSummoned?.Invoke(TargetPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        gameObject.Release();
    }
}
