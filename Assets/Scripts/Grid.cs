using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SBR
{
    public class Grid : MonoBehaviour
    {
        // X, Y, Z
        [SerializeField]
        private int[] gridCount = new int[3] { 5, 1, 10 };
        [SerializeField]
        private float gridCenterX = 0f;
        [SerializeField]
        private float gridStartZ = 0f;


        [SerializeField]
        private Vector3 brickBound = new Vector3(1.8f, 1f, 1f);

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
        }

        public Vector3 getPosFromCoord(XYZ coord)
        {
            Vector3 pos = new Vector3();
            for (int i = 0; i < 3; i++)
                pos[i] = StdBrickPos[i] + ((coord[i] - StdBrickCoord[i]) * brickBound[i]);
            return pos;
        }
    }
}