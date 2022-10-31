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
        private GameObject bulletGrave;
        [SerializeField]
        private GameObject outerior;
        [SerializeField]
        private Transform boundaries;
        [SerializeField]
        private Transform outeriorParent;
        private Vector3 outeriorShape = new(1f,0.05f,1f);
        private float[] outeriorMargin = {3f, 1f, 0.7f, 1f}; // top, right, bottom, left
        private float outeriorRoomscale = 10;
        // X, Y, Z
        public readonly int[] gridCount = new int[3] { 5, 2, 10 };// x: 왼쪽에서 오른쪽, y: 아래에서 위, z: 앞에서 뒤
        [SerializeField]
        private float gridCenterX = 0f;
        [SerializeField]
        private float gridStartZ = 0f;

        [SerializeField]
        public readonly Vector3 brickBound = new Vector3(1.6f, 1f, 1f);

        // 기준 벽돌 좌표, 위치
        private XYZ StdBrickCoord;
        private Vector3 StdBrickPos;

        // 그리드 경계 (min(0,0,0), max(gridCount))
        [HideInInspector]
        public Vector3[] gridBound;
        private Vector3[] realGridBound;

        public Brick[,,] bricks;

        private void Awake()
        {
            StdBrickCoord = new XYZ(((gridCount[0] % 2 == 0) ? (gridCount[0] - 2) : (gridCount[0] - 1)) / 2, 0, 0);
            StdBrickPos = new Vector3((gridCount[0] % 2 == 0) ? gridCenterX - (brickBound.x / 2) : gridCenterX, brickBound[1]/2f, gridStartZ);
            gridBound = new Vector3[2] { getPosFromCoord(new XYZ(0, 0, 0)), getPosFromCoord(new XYZ(gridCount[0] - 1, gridCount[1] - 1, gridCount[2] - 1)) };
            realGridBound = new Vector3[2] {gridBound[0]-brickBound/2f, gridBound[1]+brickBound/2f};

            bricks = new Brick[gridCount[0],gridCount[1],gridCount[2]];
            for(int i=0;i<gridCount[0];i++)
                for(int j=0;j<gridCount[1];j++)
                    for(int k=0;k<gridCount[2];k++)
                        bricks[i,j,k] = null;
            PlaceWalls();
            PlaceOuterior();
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
            Instantiate(wall, new Vector3(gridBound[0].x-(brickBound.x+0.05f)/2,pos.y, pos.z),Quaternion.identity, boundaries).transform.localScale = new Vector3(0.05f,wht.y+0.1f,wht.z);
            Instantiate(wall, new Vector3(gridBound[1].x+(brickBound.x+0.05f)/2,pos.y, pos.z),Quaternion.identity, boundaries).transform.localScale = new Vector3(0.05f,wht.y+0.1f,wht.z);

            //ZWall
            Instantiate(bulletGrave, new Vector3(pos.x, pos.y, gridBound[0].z-(brickBound.z+0.05f)/2),Quaternion.identity, boundaries).transform.localScale = new Vector3(wht.x,wht.y,0.05f);
            Instantiate(wall, new Vector3(pos.x, pos.y, gridBound[1].z+(brickBound.z+0.05f)/2),Quaternion.identity, boundaries).transform.localScale = new Vector3(wht.x,wht.y,0.05f);
        }
        private void PlaceOuterior()
        {
            Vector3 wht = realGridBound[1] - realGridBound[0];
            Vector3 pos = (realGridBound[1] + realGridBound[0])/2f;
            var front = new Vector3(wht[0]+outeriorMargin[1]+outeriorMargin[3],wht[1]+outeriorMargin[0]+outeriorMargin[2],0f);
            var zvalue = realGridBound[0].z+outeriorShape.y/2;
            var quater = Quaternion.Euler(-90f,0f,0f);

            Instantiate(outerior, new Vector3(pos.x, realGridBound[1].y+outeriorMargin[0]/2f+0.025f,zvalue), quater,outeriorParent).transform.localScale 
                = new Vector3(front.x, outeriorShape.y,outeriorMargin[0]-0.05f); //top
            Instantiate(outerior, new Vector3(pos.x, realGridBound[0].y-outeriorMargin[2]/2f-0.025f,zvalue), quater,outeriorParent).transform.localScale
                = new Vector3(front.x, outeriorShape.y,outeriorMargin[2]-0.05f); //bot
            Instantiate(outerior, new Vector3(realGridBound[1].x+outeriorMargin[1]/2f+0.025f, pos.y, zvalue), quater, outeriorParent).transform.localScale
                = new Vector3(outeriorMargin[1]-0.05f,outeriorShape.y,wht.y+0.1f); //right
            Instantiate(outerior, new Vector3(realGridBound[0].x-outeriorMargin[3]/2f-0.025f, pos.y, zvalue), quater, outeriorParent).transform.localScale
                = new Vector3(outeriorMargin[3]-0.05f,outeriorShape.y,wht.y+0.1f); //left
            
            //LR
            quater = Quaternion.Euler(-90f,-90f,0f);
            zvalue = realGridBound[0].z;
            var yvalue = ((realGridBound[1].y+outeriorMargin[0]) + (realGridBound[0].y-outeriorMargin[2]))/2f;
            Instantiate(outerior, new Vector3(pos.x-front.x/2, yvalue, zvalue-outeriorRoomscale/2f), quater, outeriorParent).transform.localScale
                = new Vector3(outeriorRoomscale, outeriorShape.y, front.y);
            Instantiate(outerior, new Vector3(pos.x+front.x/2, yvalue, zvalue-outeriorRoomscale/2f), quater, outeriorParent).transform.localScale
                = new Vector3(outeriorRoomscale, outeriorShape.y, front.y);
            
            //TB
            quater = Quaternion.Euler(0f,0f,0f);
            Instantiate(outerior, new Vector3(pos.x, realGridBound[1].y+outeriorMargin[0],zvalue-outeriorRoomscale/2f), quater, outeriorParent).transform.localScale
                = new Vector3(front.x, outeriorShape.y, outeriorRoomscale);
            Instantiate(outerior, new Vector3(pos.x, realGridBound[0].y-outeriorMargin[2],zvalue-outeriorRoomscale/2f), quater, outeriorParent).transform.localScale
                = new Vector3(front.x, outeriorShape.y, outeriorRoomscale);
            
            //back
            Instantiate(outerior, new Vector3(pos.x, yvalue, zvalue-outeriorRoomscale-outeriorShape.y/2), Quaternion.Euler(-90f,0f,0f), outeriorParent).transform.localScale
                = new Vector3(front.x, outeriorShape.y, front.y);
        }
    }
}