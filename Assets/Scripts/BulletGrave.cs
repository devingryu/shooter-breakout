using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletGrave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other) {
        if(other.tag != "Bullet") return;

        var rb = other.GetComponent<Rigidbody>();
        var bulletVelocity = rb.velocity.normalized;

        if(bulletVelocity.z <0) Destroy(other.gameObject); // 나가는 방향
        else return; // 들어오는 방향
    }   
}
