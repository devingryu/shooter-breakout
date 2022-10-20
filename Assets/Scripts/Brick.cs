using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace SBR
{
    public class Brick : MonoBehaviour
    {
        private MinimapManager minimap;
        public TextMeshProUGUI[] texts;
        public bool isBrick;
        private int health;
        public int Health
        {
            get => health;
            set
            {
                health = value;
                if(coord != null) {
                    minimap.OnBrickUpdate(coord);
                    if (health <= 0){
                        GameManager.Inst.grid.bricks[coord.X,coord.Y,coord.Z] = null;
                        Destroy(gameObject);
                    }
                    if(isBrick) {
                        foreach (var t in texts)
                            t.text = health.ToString();
                    }
                }
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
            minimap = GameManager.Inst.minimap;
            GameManager.Inst.grid.bricks[coord.X,coord.Y,coord.Z] = this;
            Coord = coord;
            Health = health;
        }
        private void OnCollisionEnter(Collision other)
        {
            if(!isBrick && other.gameObject.tag != "Bullet") return;
            Health--;
        }
        private void OnTriggerEnter(Collider other) 
        {
            if(isBrick) return;
            GameManager.Inst.MaxBallCount++;
            Health = 0;
        }
    }
}