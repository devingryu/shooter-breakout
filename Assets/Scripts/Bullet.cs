using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class Bullet : MonoBehaviour
    {
        private Rigidbody rb;
        private Vector3 direction;
        public float SpeedMultiplier => 6f;
        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            direction = transform.forward;
            rb.velocity = direction * SpeedMultiplier;
            GameManager.Inst.minimap.NewBall(transform);
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