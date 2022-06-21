using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ToolBox.Pools;

public class AgentProjectileLauncher : MonoBehaviour
{
    [SerializeField]
    private GameObject projetilePrefab;
    GameObject projectile;

    public void LaunchProjectile(Transform basePos, Transform targetPos)
    {
        projectile = projetilePrefab.Reuse(basePos.position, Quaternion.identity);
        projectile.GetComponent<Projectile>().TargetPos = targetPos;
    }
}
