using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ReflectingLaser : MonoBehaviour
{
    public int Reflections;
    public float MaxLength;

    private LineRenderer lineRenderer;
    private Ray ray;
    private RaycastHit hit;
    private Vector3 direction;
    private int layermask;

    private void Awake() {
        lineRenderer = GetComponent<LineRenderer>();
        layermask = ~(1 << LayerMask.NameToLayer("Bullet") | 1 << LayerMask.NameToLayer("BulletGrave") | 1 << LayerMask.NameToLayer("Hand") | 1 << LayerMask.NameToLayer("Item"));
    }
    // Update is called once per frame
    void Update()
    {
        ray = new Ray(transform.position, transform.forward);

        lineRenderer.positionCount = 1;
        lineRenderer.SetPosition(0, transform.position);
        float remainingLength = MaxLength;
        
        for (int i=0; i< Reflections; i++)
        {
            if(Physics.Raycast(ray.origin, ray.direction, out hit, remainingLength,layermask))
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, hit.point);
                remainingLength -= Vector3.Distance(ray.origin, hit.point);
                if(hit.transform.tag != "Wall" && hit.transform.tag != "Brick") break;
                ray = new Ray(hit.point, Vector3.Reflect(ray.direction, hit.normal));
            }
            else
            {
                lineRenderer.positionCount += 1;
                lineRenderer.SetPosition(lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength);
            }
        }
    }
}
