using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 direction;
        public float SpeedMultiplier => 10f;
        private int defaultMask;
        private int noBltGrvMsk;
        private float colliderRadius = 0.5f * 0.04f;
        private bool hasCollision = false;
        private RaycastHit hit;
        private float maxDistance = 100f;
        private float movedDistance = 0f;
        private bool en = false;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            direction = transform.forward;
            //rb.velocity = direction * SpeedMultiplier;
            GameManager.Inst.minimap.NewBall(transform);

            defaultMask = (1 << LayerMask.NameToLayer("BulletGrave") | 1 << LayerMask.NameToLayer("Wall") | 1 << LayerMask.NameToLayer("Brick") | 1 << LayerMask.NameToLayer("Item") | 1 << LayerMask.NameToLayer("Outerior"));
            noBltGrvMsk = defaultMask & ~(1 << LayerMask.NameToLayer("BulletGrave"));
            CheckNextCollision();
        }
        private void Update() 
        {
            if(!en) return;
            float targetDisp = SpeedMultiplier * Time.deltaTime;
            if(!hasCollision || hit.distance > (movedDistance + targetDisp))
            {
                transform.position += direction * targetDisp;
                movedDistance += targetDisp;
                if(!hasCollision && movedDistance > 30f)
                    Destroy(gameObject);
            } 
            else
            {
                transform.position = hit.point;
                if(hit.collider != null)
                {
                    switch(hit.collider.tag)
                    {
                        case "Brick":
                            direction = Vector3.Reflect(direction, hit.normal).normalized;
                            hit.collider.GetComponent<Brick>().OnBulletHit();
                            break;
                        case "Wall":
                            direction = Vector3.Reflect(direction, hit.normal).normalized;
                            break;
                        case "Item":
                            hit.collider.GetComponent<Brick>().OnBulletHit();
                            break;
                        default:
                            Destroy(gameObject);
                            return;
                    }
                }
                CheckNextCollision();
            }
        }
        private void CheckNextCollision()
        {
            en=false;
            movedDistance = 0f;
            Ray ray = new Ray(transform.position, direction);
            hasCollision = Physics.SphereCast(ray.origin, colliderRadius, ray.direction, out hit, maxDistance, defaultMask);
            if(hasCollision && hit.collider.tag == "BulletGrave" && direction.z > 0)
                hasCollision = Physics.SphereCast(ray.origin, colliderRadius, ray.direction, out hit, maxDistance, noBltGrvMsk);
            if(hasCollision)
                Debug.Log($"{hit.collider.tag}");
            en=true;
        }

        private void OnCollisionEnter(Collision other) 
        {
            direction = Vector3.Reflect(direction, other.contacts[0].normal).normalized;
            rb.velocity = direction * SpeedMultiplier;
        }
        private void OnDestroy() {
            GameManager.Inst.ReturnedBallCount--;
        }
        
    }
}