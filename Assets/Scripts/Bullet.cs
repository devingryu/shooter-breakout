using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 direction;
        public float SpeedMultiplier => 4f;
        public UIBall pair;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            direction = transform.forward;
            rb.velocity = direction * SpeedMultiplier;
            pair = GameManager.Inst.minimap.newBall();
            pair.Init(transform);
        }

        private void OnCollisionEnter(Collision other) 
        {
            direction = Vector3.Reflect(direction, other.contacts[0].normal).normalized;
            rb.velocity = direction * SpeedMultiplier;
        }
        private void OnDestroy() 
            => Destroy(pair.gameObject);
        
    }
}