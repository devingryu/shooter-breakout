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
            SpwanLine();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SpwanLine()
        {
            int round = gm.Round++;
            XYZ[] bricks = {new XYZ(0,0,8), new XYZ(0,0,9), new XYZ(1,0,9), new XYZ(3,0,6), new XYZ(4,0,5)};
            foreach(var b in bricks){
                var newBrick = Instantiate(brick, brickParent);
                newBrick.GetComponent<Brick>().Init(round++, b);
            }
        }
    }
}