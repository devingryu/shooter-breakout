using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class Grave : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other) {
            if(other.gameObject.tag != "Bullet") return;
            Destroy(other.gameObject);
        }
    }
}