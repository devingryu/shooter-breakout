using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class BrickSpawner : MonoBehaviour
    {
        private GameManager gm;
        [SerializeField]
        private GameObject brick;
        [SerializeField]
        private Transform brickParent;
        void Start()
        {
            gm = GameManager.Inst;
            gm.minimap.Init();
            SpwanLine();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SpwanLine()
        {
            int round = gm.Round++;
            XYZ[] bricks = {new(1,0,8), new(3,0,8), new(0,0,0), new(4,0,0)};
            foreach(var b in bricks){
                var newBrick = Instantiate(brick, brickParent);
                newBrick.GetComponent<Brick>().Init(++round, b);;
                gm.minimap.OnBrickUpdate(b);
            }
        }
    }
}