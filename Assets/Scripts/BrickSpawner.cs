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
            gm.minimap.Init(gm.grid.gridCount[1]);
        }

        // Update is called once per frame
        void Update()
        {
        }
        public void SpawnLine()
        {
            int round = gm.Round;
            Grid grid = gm.grid;
            int max = grid.gridCount[0] * grid.gridCount[1];
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
                XYZ xyz = new XYZ(b % grid.gridCount[0], b / grid.gridCount[0], grid.gridCount[2]-1);
                ManualBrickAdd(xyz, round);
            }

            currentBrick = Random.Range(0, max);
            while (bricks.Contains(currentBrick))
                currentBrick = Random.Range(0, max);
            XYZ itemxyz = new XYZ(currentBrick % grid.gridCount[0], currentBrick / grid.gridCount[0], grid.gridCount[2] - 1);
            ManualItemAdd(itemxyz, round);
        }
        
        public bool MoveObjects()
        {
            List<Brick> gridBricks = new List<Brick>();
            foreach (var gb in gm.grid.bricks)
            {
                if (gb == null)
                    continue;
                if (gb.Coord.Z == 0)
                {
                    Debug.Log("Game Over");
                    Reset();
                    return false;
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
            return true;
        }

        public void NextRound()
        {
            if(MoveObjects())
                SpawnLine();
        }
        public void ManualBrickAdd(XYZ pos, int health)
        {
            var newBrick = Instantiate(brick, brickParent);
            newBrick.GetComponent<Brick>().Init(health, pos);
            gm.minimap.OnBrickUpdate(pos);
        }
        public void ManualItemAdd(XYZ pos, int health)
        {
            var newBrick = Instantiate(item, brickParent);
            newBrick.GetComponent<Brick>().Init(health, pos);
            gm.minimap.OnBrickUpdate(pos);
        }
        public void Reset()
        {
            foreach (var b in gm.grid.bricks)
            {
                if (b != null)
                    b.Health = 0;
            }
            gm.MaxBallCount = 1;
            gm.Round = 0;
            gm.Round++;
            gm.running = false;
            DataHandler.Inst.RemoveSaveFile();
        }
        [ContextMenu("Line add")]
        private void AddLine()
        {
            gm.Round++;
        }
    }
}