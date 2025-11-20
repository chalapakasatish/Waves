using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDetectionRadius : MonoBehaviour
{
    public float detectionRadius = 10f;
    
    public LayerMask layer;   // assign "Enemy" layer
    
    public void Update()
    {
        Transform target = GetNearestGameobject();
    }
    public Transform GetNearestGameobject()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, layer);

        if (hits.Length == 0)
        {
            return null;
        }
        Transform nearest = null;
        float minDist = Mathf.Infinity;
        foreach (Collider hit in hits)
        {
            float dist = Vector3.Distance(transform.position, hit.transform.position);
            if (dist < minDist)
            {
                minDist = dist;
                nearest = hit.transform;
            }
        }
        return nearest;
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
