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
        public void Init(XYZ pos)
        {
            var vec3 = GameManager.Inst.minimap.getPosFromCoord(pos);
            transform.localPosition = new(vec3.x,vec3.z,0);
        }
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}