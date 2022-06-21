using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarRot : MonoBehaviour
{
    public Transform _base;

    private void Awake()
    {
        _base = Camera.main.transform;
    }

    void Update()
    {
        transform.LookAt(_base);
    }
}
