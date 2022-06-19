using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarRot : MonoBehaviour
{
    public Transform _base;

    void Update()
    {
        transform.LookAt(_base);
    }
}
