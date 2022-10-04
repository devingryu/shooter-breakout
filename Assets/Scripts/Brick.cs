using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SBR
{
    public class Brick : MonoBehaviour
    {
        public TextMeshProUGUI[] texts;
        private int health;
        public int Health
        {
            get => health;
            set
            {
                health = value;
                if (health <= 0)
                    Destroy(gameObject);
                foreach (var t in texts)
                    t.text = health.ToString();

            }
        }
        private XYZ coord;
        public XYZ Coord
        {
            get => coord;
            set {
                coord = value;
                transform.position = GameManager.Inst.grid.getPosFromCoord(value);
            }
        }

        public void Init(int health, XYZ coord) 
        {
            Health = health;
            Coord = coord;
        }
        private void OnCollisionEnter(Collision other)
        {
            Health--;
        }
    }
}