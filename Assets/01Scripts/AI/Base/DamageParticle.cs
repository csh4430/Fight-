using ToolBox.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

public class DamageParticle : MonoBehaviour
{
    public GameObject Particle;
    Sequence seq;
    private void Awake()
    {
        seq = DOTween.Sequence();
    }

    public void RenderParticle(Transform pos)
    {
        StartCoroutine(DamageCoroutine(pos));
    }

    private IEnumerator DamageCoroutine(Transform pos)
    {
        GameObject temp = null;
        temp = Particle.Reuse(pos.position, Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        temp.Release();
    }

    private void DestroyParticle(GameObject particle)
    {
        particle.Release();
    }
}
