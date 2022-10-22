using UnityEngine;

namespace SBR
{
    public class FaceCamera : MonoBehaviour
    {

        private void Update()
        {
            transform.rotation = Quaternion.LookRotation(transform.position-Camera.main.transform.position);
        }
    }
}