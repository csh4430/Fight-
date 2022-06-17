using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetSetter : MonoBehaviour
{
    public static Transform SetTarget(Transform origin, float distance, LayerMask layerMask)
    {
        Collider[] hits = Physics.OverlapSphere(origin.position, distance, layerMask);
        var h = hits.OrderBy(x => Vector3.Distance(x.transform.position, origin.transform.position)).FirstOrDefault();

        
        if (h == null)
            return null;
        IHittable baseHit;
        h.TryGetComponent<IHittable>(out baseHit);

        if (baseHit != null)
            if (baseHit.IsDead)
                return null;
        return h.transform;

    }
}
