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
                if(coord != null)
                    minimap.OnBrickUpdate(coord);
                if (health <= 0){
                    GameManager.Inst.grid.bricks[Coord.X,Coord.Y,Coord.Z] = null;
                    Destroy(gameObject);
                }
                if(isBrick) {
                    foreach (var t in texts)
                        t.text = health.ToString();
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
        public void Start()
        {
            minimap = GameManager.Inst.minimap;
        }

        public void Init(int health, XYZ coord) 
        {
            GameManager.Inst.grid.bricks[coord.X,coord.Y,coord.Z] = this;
            Health = health;
            Coord = coord;
        }
        private void OnCollisionEnter(Collision other)
        {
            if(!isBrick) return;
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