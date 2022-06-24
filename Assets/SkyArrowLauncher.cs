using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class SkyArrowLauncher : MonoBehaviour
{
    public GameObject ArrowPrefab = null;
    public void Summon(float range)
    {
        for (int i = 0; i < 7; i++)
        {
            Vector2 rCenter = Random.insideUnitCircle * range;
            Vector3 rPos = transform.position + new Vector3(rCenter.x, 0, rCenter.y);
            ArrowPrefab.Reuse(rPos, Quaternion.identity);
        }
    }
}
