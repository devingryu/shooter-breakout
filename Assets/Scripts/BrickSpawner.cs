using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class BrickSpawner : MonoBehaviour
    {
        private GameManager gm;
        [SerializeField]
        private GameObject item;
        [SerializeField]
        private GameObject brick;
        [SerializeField]
        private Transform brickParent;
        void Start()
        {
            gm = GameManager.Inst;
            gm.minimap.Init();
            SpawnLine();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void SpawnLine()
        {
            int round = gm.Round;
            int max = gm.grid.gridCount[0] * gm.grid.gridCount[1];
            int numberOfBricks = Random.Range(1, max);
            List<int> bricks = new List<int>();
            int currentBrick = Random.Range(0, max);
            for(int i =0; i<numberOfBricks;){
                if (bricks.Contains(currentBrick)){
                    currentBrick = Random.Range(0, max);
                }
                else{
                    bricks.Add(currentBrick);
                    i++;
                }
            }            
            foreach(var b in bricks){
                XYZ xyz = new XYZ(b % gm.grid.gridCount[0], b / gm.grid.gridCount[0], gm.grid.gridCount[2]-1);
                var newBrick = Instantiate(brick, brickParent);
                newBrick.GetComponent<Brick>().Init(round, xyz);;
                gm.minimap.OnBrickUpdate(xyz);
            }

            currentBrick = Random.Range(0, max);
            while (bricks.Contains(currentBrick))
                currentBrick = Random.Range(0, max);
            XYZ itemxyz = new XYZ(currentBrick % gm.grid.gridCount[0], currentBrick / gm.grid.gridCount[0], gm.grid.gridCount[2] - 1);
            var newItem = Instantiate(item, brickParent);
            newItem.GetComponent<Brick>().Init(round, itemxyz); ;
            gm.minimap.OnBrickUpdate(itemxyz);
        }
        
        public void MoveObjects()
        {
            List<Brick> gridBricks = new List<Brick>();
            foreach (var gb in gm.grid.bricks)
            {
                if (gb == null)
                    continue;
                if (gb.Coord.Z == 0)
                {
                    Debug.Log("Game Over");
                    return;
                }
                gridBricks.Add(gb);
            }
            foreach (var b in gridBricks)
            {
                gm.grid.bricks[b.Coord.X, b.Coord.Y, b.Coord.Z] = null;
                gm.minimap.OnBrickUpdate(b.Coord);
                b.Coord = b.Coord.move(0, 0, -1);
                gm.grid.bricks[b.Coord.X, b.Coord.Y, b.Coord.Z] = b;
                gm.minimap.OnBrickUpdate(b.Coord);
            }
        }

        public void NextRound()
        {
            MoveObjects();
            SpawnLine();
        }
        [ContextMenu("Line add")]
        private void AddLine()
        {
            NextRound();
        }
    }
}