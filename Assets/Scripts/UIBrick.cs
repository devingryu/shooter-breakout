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
        [SerializeField]
        private SpriteRenderer spr;
        [SerializeField]
        private Sprite[] sprites;
        private bool isBrick;
        public bool IsBrick {
            get => isBrick;
            set {
                isBrick = value;
                
                text.enabled = value;
                spr.sprite = sprites[value?1:0];
                spr.color = value?new Color32(255,138,138,255):new Color32(32,166,0,255);
                spr.transform.localScale = value?new(1f,1f,1f):new(0.3125f,0.5f,0.5f);
            }
        }
        private int health;
        public int Health
        {
            get => health;
            set
            {
                health = value;
                gameObject.SetActive(value > 0);
                if(isBrick) {
                    text.text = value.ToString();
                }
            }
        }
        public void Init(Vector3 pos)
        {
            transform.localPosition = new(pos.x,pos.z,0);
        }
    }
}