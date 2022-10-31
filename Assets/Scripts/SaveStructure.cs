using System;
using UnityEngine;

namespace SBR
{
    [Serializable]
    public class v3
    {
        public v3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public v3(Vector3 v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }
        public float x;
        public float y;
        public float z;
        public Vector3 ToVector3() => new Vector3(x,y,z);
    }
    [Serializable]
    public class BrickInfo
    {
        public BrickInfo(XYZ pos, int health)
        {
            this.pos = pos;
            this.health = health;
        }
        public XYZ pos;
        public int health;
    }
    [Serializable]
    public class BulletInfo
    {
        public BulletInfo(v3 pos, v3 dir)
        {
            this.pos = pos;
            this.dir = dir;
        }
        public v3 pos;
        public v3 dir;
    }
    [Serializable]
    public class SaveStructure
    {
        public SaveStructure(BrickInfo[] bricks, BulletInfo[] balls, int round, int maxBallCount, int remainingBallCount, int returnedBallCount)
        {
            this.bricks = (BrickInfo[]) bricks.Clone();
            this.balls = (BulletInfo[]) balls.Clone();
            this.round = round;
            this.maxBallCount = maxBallCount;
            this.remainingBallCount = remainingBallCount;
            this.returnedBallCount = returnedBallCount;
        }
        public BrickInfo[] bricks; // 위치, 체력
        public BulletInfo[] balls; // 위치, 방향;
        public int round;
        public int maxBallCount;
        public int remainingBallCount;
        public int returnedBallCount;
    }

}
