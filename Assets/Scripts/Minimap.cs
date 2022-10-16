using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class Minimap : MonoBehaviour
    {
        [SerializeField]
        private float gridCenterX = 0f;
        [SerializeField]
        private float gridStartZ = 0f;
        // 기준 벽돌 좌표, 위치
        private XYZ StdBrickCoord;
        private Vector3 StdBrickPos;
        public int currentY = 0;
        public UIBrick[,] bricks;
        private int[] gridCount;
        [SerializeField]
        private GameObject uiBrickPrefab;
        [SerializeField]
        private GameObject uiBackgroundPrefab;
        [SerializeField]
        private GameObject uiBallPrefab;
        private Grid grid;
        public readonly Vector3 brickBound = new(0.2f,0f,0.125f);
        public Vector3[] gridBound;
        private void Start()
        {
            grid = GameManager.Inst.grid;
            gridCount = (int[]) grid.gridCount.Clone();
        }
        public void Init()
        {
            StdBrickCoord = new XYZ(((gridCount[0] % 2 == 0) ? (gridCount[0] - 2) : (gridCount[0] - 1)) / 2, 0, 0);
            StdBrickPos = new Vector3((gridCount[0] % 2 == 0) ? gridCenterX - (brickBound.x / 2) : gridCenterX, 0, gridStartZ);
            gridBound = new Vector3[2] { getPosFromCoord(new XYZ(0, 0, 0)), getPosFromCoord(new XYZ(gridCount[0] - 1, 0, gridCount[2] - 1)) };

            bricks = new UIBrick[gridCount[0],gridCount[2]];
            for(int i=0;i<gridCount[0];i++)
                for(int k=0;k<gridCount[2];k++){
                    var newUIBrick = Instantiate(uiBrickPrefab,Vector3.zero,Quaternion.Euler(0f,-90f,0f),transform);
                    newUIBrick.SetActive(false);
                    bricks[i,k] = newUIBrick.GetComponent<UIBrick>();
                    bricks[i,k].Init(new(i,0,k));
                }
            //Debug.Log($"{gridBound[0]} {gridBound[1]}");
            var grdbndsum = (gridBound[0] + gridBound[1])/2f;
            var grdbndmns = gridBound[1] - gridBound[0] + brickBound;
        
            var bg = Instantiate(uiBackgroundPrefab, Vector3.zero, Quaternion.Euler(0f,-90f,0f), transform);
            bg.transform.localPosition = new Vector3(gridCenterX+grdbndsum.x, gridStartZ+grdbndsum.z, 0f);
            bg.transform.localScale = new Vector3(grdbndmns.x*2f,grdbndmns.z*2f,1f);

            GameManager m = GameManager.Inst;
            Vector3 whtg = m.grid.gridBound[1]-m.grid.gridBound[0]+m.grid.brickBound;
            Vector3 whtm = m.minimap.gridBound[1]-m.minimap.gridBound[0]+m.minimap.brickBound;
            //Debug.Log("Init Complete");
        }
        public Vector3 getPosFromCoord(XYZ coord)
        {
            Vector3 pos = new Vector3();
            for (int i = 0; i < 3; i++)
                pos[i] = StdBrickPos[i] + ((coord[i] - StdBrickCoord[i]) * brickBound[i]);
            return pos;
        }
        public void OnBrickUpdate(XYZ pos)
        {
            //Debug.Log($"OnBrickUpdate: {pos}");
            if(currentY != pos.Y) return;
            var gridbrick = grid.bricks[pos.X,pos.Y,pos.Z];
            if(gridbrick != null) 
                bricks[pos.X,pos.Z].Health = gridbrick.Health;
        }
        public void updateBricks()
        {
            for(int i=0;i<gridCount[0];i++)
                for(int k=0;k<gridCount[2];k++) 
                    OnBrickUpdate(new(i,currentY,k));
        }
        public UIBall newBall()
            => Instantiate(uiBallPrefab, Vector3.zero, Quaternion.Euler(0f,-90f,0f), transform).GetComponent<UIBall>();
        
    }
}