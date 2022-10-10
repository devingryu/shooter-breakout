using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class Grid : MonoBehaviour
    {
        [SerializeField]
        private GameObject wall;
        [SerializeField]
        private Transform boundaries;
        // X, Y, Z
        [SerializeField]
        private int[] gridCount = new int[3] { 5, 1, 10 };// x: 왼쪽에서 오른쪽, y: 아래에서 위, z: 앞에서 뒤
        [SerializeField]
        private float gridCenterX = 0f;
        [SerializeField]
        private float gridStartZ = 0f;

        [SerializeField]
        private Vector3 brickBound = new Vector3(1.6f, 1f, 1f);

        // 기준 벽돌 좌표, 위치
        private XYZ StdBrickCoord;
        private Vector3 StdBrickPos;

        // 그리드 경계 (min(0,0,0), max(gridCount))
        private Vector3[] gridBound;

        public Brick[,,] bricks;

        void Start()
        {
            StdBrickCoord = new XYZ(((gridCount[0] % 2 == 0) ? (gridCount[0] - 2) : (gridCount[0] - 1)) / 2, 0, 0);
            StdBrickPos = new Vector3((gridCount[0] % 2 == 0) ? gridCenterX - (brickBound.x / 2) : gridCenterX, brickBound[1]/2f, gridStartZ);
            gridBound = new Vector3[2] { getPosFromCoord(new XYZ(0, 0, 0)), getPosFromCoord(new XYZ(gridCount[0] - 1, gridCount[1] - 1, gridCount[2] - 1)) };

            bricks = new Brick[gridCount[0],gridCount[1],gridCount[2]];
            for(int i=0;i<gridCount[0];i++)
                for(int j=0;j<gridCount[1];j++)
                    for(int k=0;k<gridCount[2];k++)
                        bricks[i,j,k] = null;
            PlaceWalls();
        }

        public Vector3 getPosFromCoord(XYZ coord)
        {
            Vector3 pos = new Vector3();
            for (int i = 0; i < 3; i++)
                pos[i] = StdBrickPos[i] + ((coord[i] - StdBrickCoord[i]) * brickBound[i]);
            return pos;
        }
        public void PlaceWalls()
        {
            Vector3 wht = gridBound[1]-gridBound[0]+brickBound;
            Vector3 pos = (gridBound[1]+gridBound[0])/2f;
            // YWall
            Instantiate(wall, new Vector3(pos.x,gridBound[0].y-(brickBound.y+0.05f)/2, pos.z),Quaternion.identity, boundaries).transform.localScale = new Vector3(wht.x,0.05f,wht.z);
            Instantiate(wall, new Vector3(pos.x,gridBound[1].y+(brickBound.y+0.05f)/2, pos.z),Quaternion.identity, boundaries).transform.localScale = new Vector3(wht.x,0.05f,wht.z);

            //XWall
            Instantiate(wall, new Vector3(gridBound[0].x-(brickBound.x+0.05f)/2,pos.y, pos.z),Quaternion.identity, boundaries).transform.localScale = new Vector3(0.05f,wht.y,wht.z);
            Instantiate(wall, new Vector3(gridBound[1].x+(brickBound.x+0.05f)/2,pos.y, pos.z),Quaternion.identity, boundaries).transform.localScale = new Vector3(0.05f,wht.y,wht.z);

            //ZWall
            Instantiate(wall, new Vector3(pos.x, pos.y, gridBound[1].z+(brickBound.z+0.05f)/2),Quaternion.identity, boundaries).transform.localScale = new Vector3(wht.x,wht.y,0.05f);
        }
    }
}