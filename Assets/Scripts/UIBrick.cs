using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SBR
{
    public class UIBrick : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;
        private int health;
        public int Health
        {
            get => health;
            set
            {
                health = value;
                text.text = value.ToString();

                gameObject.SetActive(value > 0);
            }
        }
        public void Init(Vector3 pos)
        {
            transform.localPosition = new(pos.x,pos.z,0);
        }
    }
}